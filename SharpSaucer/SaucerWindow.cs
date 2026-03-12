using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpSaucer;

public enum SaucerWindowEdge
{
   Top = 1,
   Bottom = 2,
   Left = 4,
   Right = 8,
   BottomLeft = 6,
   BottomRight = 10,
   TopLeft = 5,
   TopRight = 9,
}

public enum SaucerWindowDecoration
{
   None = 0,
   Partial = 1,
   Full = 2,
}

public enum SaucerWindowEvent
{
   Decorated = 0,
   Maximize = 1,
   Minimize = 2,
   Closed = 3,
   Resize = 4,
   Focus = 5,
   Close = 6,
}

public partial class SaucerWindow : IDisposable
{
    internal unsafe saucer_window* Handle;
    private bool _disposedValue;

    public string Title
    {
        get
        {
            unsafe
            {
                nuint size = 0;
                saucer_window_title(Handle, null, ref size);
                fixed (sbyte* buffer = new sbyte[size])
                {
                    int length = (int)size;
                    saucer_window_title(Handle, buffer, ref size);
                    return new(buffer, 0, length, Encoding.UTF8);
                }
            }
        }
        set
        {
            unsafe
            {
                saucer_window_set_title(Handle, value);
            }
        }
    }
    public SaucerIcon Icon
    {
        set 
        {
            unsafe
            {
                saucer_window_set_icon(Handle, value.Handle);
            }
        }
    }
    public (int Width, int Height) MinSize
    {
        get
        {
            unsafe
            {
                saucer_window_min_size(Handle, out var width, out var height);
                return (width, height);
            }
        }
        set
        {
            unsafe
            {
                saucer_window_set_min_size(Handle, value.Width, value.Height);
            }
        }
    }
    public (int Width, int Height) Size
    {
        get
        {
            unsafe
            {
                saucer_window_size(Handle, out var width, out var height);
                return (width, height);
            }
        }
        set
        {
            unsafe
            {
                saucer_window_set_size(Handle, value.Width, value.Height);
            }
        }
    }
    public (int Width, int Height) MaxSize
    {
        get
        {
            unsafe
            {
                saucer_window_max_size(Handle, out var width, out var height);
                return (width, height);
            }
        }
        set
        {
            unsafe
            {
                saucer_window_set_max_size(Handle, value.Width, value.Height);
            }
        }
    }
    public (int X, int Y) Position
    {
        get
        {
            unsafe
            {
                saucer_window_position(Handle, out var x, out var y);
                return (x, y);
            }
        }
        set
        {
            unsafe
            {
                saucer_window_set_position(Handle, value.X, value.Y);
            }
        }
    }
    public bool Resizable
    {
        get
        {
            unsafe
            {
                return saucer_window_resizable(Handle);
            }
        }
        set
        {
            unsafe
            {
                saucer_window_set_resizable(Handle, value);
            }
        }
    }
    public bool Visible
    {
        get
        {
            unsafe
            {
                return saucer_window_visible(Handle);
            }
        }
    }
    public bool Focused
    {
        get
        {
            unsafe
            {
                return saucer_window_focused(Handle);
            }
        }
    }
    public bool AlwaysOnTop
    {
        get
        {
            unsafe
            {
                return saucer_window_always_on_top(Handle);
            }
        }
        set
        {
            unsafe
            {
                saucer_window_set_always_on_top(Handle, value);
            }
        }
    }
    public bool ClickThrough
    {
        get
        {
            unsafe
            {
                return saucer_window_click_through(Handle);
            }
        }
        set
        {
            unsafe
            {
                saucer_window_set_click_through(Handle, value);
            }
        }
    }
    public SaucerWindowDecoration Decoration
    {
        get
        {
            unsafe
            {
                return (SaucerWindowDecoration)saucer_window_decorations(Handle);
            }
        }
        set
        {
            unsafe
            {
                saucer_window_set_decorations(Handle, value);
            }
        }
    }
    public Color Background
    {
        get
        {
            unsafe
            {
                (byte r, byte g, byte b, byte a) = (0, 0, 0, 0);
                saucer_window_background(Handle, ref r, ref g, ref b, ref a);
                return Color.FromArgb(a, r, g, b);
            }
        }
        set
        {
            unsafe
            {
                saucer_window_set_background(Handle, value.R, value.G, value.B, value.A);
            }
        }
    }
    public SaucerScreen Screen
    {
        get
        {
            unsafe
            {
                return new SaucerScreen(saucer_window_screen(Handle));
            }
        }
    }

    private readonly Dictionary<Delegate, nuint> _events = [];
    public event Action<SaucerWindow, SaucerWindowDecoration>? Decorated
    {
        add
        {
            if(value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWindowEventDecorated callback = (_, decoration, _) => value.Invoke(this, decoration);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_window_on(Handle, SaucerWindowEvent.Decorated, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if(_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_window_off(Handle, SaucerWindowEvent.Decorated, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Action<SaucerWindow, bool>? Maximized
    {
        add
        {
            if(value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWindowEventMaximize callback = (_, maximized, _) => value.Invoke(this, maximized);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_window_on(Handle, SaucerWindowEvent.Maximize, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if(_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_window_off(Handle, SaucerWindowEvent.Maximize, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Action<SaucerWindow, bool>? Minimized
    {
        add
        {
            if(value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWindowEventMinimize callback = (_, minimized, _) => value.Invoke(this, minimized);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_window_on(Handle, SaucerWindowEvent.Minimize, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if(_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_window_off(Handle, SaucerWindowEvent.Minimize, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Action<SaucerWindow>? Closed
    {
        add
        {
            if(value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWindowEventClosed callback = (_, _) => value.Invoke(this);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_window_on(Handle, SaucerWindowEvent.Closed, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if(_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_window_off(Handle, SaucerWindowEvent.Closed, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Action<SaucerWindow, int, int>? Resize
    {
        add
        {
            if(value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWindowEventResize callback = (_, width, height, _) => value.Invoke(this, width, height);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_window_on(Handle, SaucerWindowEvent.Resize, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if(_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_window_off(Handle, SaucerWindowEvent.Resize, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Action<SaucerWindow, bool>? Focus
    {
        add
        {
            if(value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWindowEventFocus callback = (_, focused, _) => value.Invoke(this, focused);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_window_on(Handle, SaucerWindowEvent.Focus, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if(_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_window_off(Handle, SaucerWindowEvent.Focus, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Func<SaucerWindow, SaucerPolicy>? CloseRequested
    {
        add
        {
            if(value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWindowEventClose callback = (_, _) => value.Invoke(this);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_window_on(Handle, SaucerWindowEvent.Close, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if(_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_window_off(Handle, SaucerWindowEvent.Close, id);
                }
                _events.Remove(value);
            }
        }
    }

    public SaucerWindow(SaucerApplication application)
    {
        unsafe
        {
            Handle = saucer_window_new(application.Handle, out var error);
            if(error != 0)
                throw new Exception($"Failed to create window. Error code: {error}");
        }
    }

    public void Minimize()
    {
        unsafe
        {
            saucer_window_set_minimized(Handle, true);
        }
    }
    public void Maximize()
    {
        unsafe
        {
            saucer_window_set_maximized(Handle, true);
        }
    }


    public void Show()
    {
        unsafe
        {
            saucer_window_show(Handle);
        }
    }
    public void Close()
    {
        unsafe
        {
            saucer_window_close(Handle);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }
            unsafe
            {
                saucer_window_free(Handle);
            }
            _disposedValue = true;
        }
    }

    ~SaucerWindow()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
