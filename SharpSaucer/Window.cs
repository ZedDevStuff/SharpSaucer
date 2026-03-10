using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using SharpSaucer.Native;

namespace SharpSaucer;

public unsafe class Window : StructWrapper
{
    public bool Visible => NativeMethods.saucer_window_visible((SaucerWindow*)Handle);

    /// <summary>Whether the window currently has keyboard focus.</summary>
    public bool Focused => NativeMethods.saucer_window_focused((SaucerWindow*)Handle);

    /// <summary>Whether the window is minimized.</summary>
    public bool Minimized
    {
        get => NativeMethods.saucer_window_minimized((SaucerWindow*)Handle);
        set => NativeMethods.saucer_window_set_minimized((SaucerWindow*)Handle, value);
    }

    /// <summary>Whether the window is maximized.</summary>
    public bool Maximized
    {
        get => NativeMethods.saucer_window_maximized((SaucerWindow*)Handle);
        set => NativeMethods.saucer_window_set_maximized((SaucerWindow*)Handle, value);
    }

    /// <summary>Whether the window is resizable.</summary>
    public bool Resizable
    {
        get => NativeMethods.saucer_window_resizable((SaucerWindow*)Handle);
        set => NativeMethods.saucer_window_set_resizable((SaucerWindow*)Handle, value);
    }

    /// <summary>Whether the window is in fullscreen mode.</summary>
    public bool Fullscreen
    {
        get => NativeMethods.saucer_window_fullscreen((SaucerWindow*)Handle);
        set => NativeMethods.saucer_window_set_fullscreen((SaucerWindow*)Handle, value);
    }

    /// <summary>Whether the window is always on top of other windows.</summary>
    public bool AlwaysOnTop
    {
        get => NativeMethods.saucer_window_always_on_top((SaucerWindow*)Handle);
        set => NativeMethods.saucer_window_set_always_on_top((SaucerWindow*)Handle, value);
    }

    /// <summary>Whether mouse clicks pass through the window.</summary>
    public bool ClickThrough
    {
        get => NativeMethods.saucer_window_click_through((SaucerWindow*)Handle);
        set => NativeMethods.saucer_window_set_click_through((SaucerWindow*)Handle, value);
    }

    public unsafe string Title
    {
        get
        {
            NativeMethods.saucer_window_title((SaucerWindow*)Handle, 0, out var size);
            if (size == 0) return string.Empty;

            var buf = stackalloc byte[(int)size];
            NativeMethods.saucer_window_title((SaucerWindow*)Handle, (nint)buf, out size);
            return Encoding.UTF8.GetString(buf, (int)size);
        }
        set => NativeMethods.saucer_window_set_title((SaucerWindow*)Handle, value);
    }

    public (byte R, byte G, byte B, byte A) Background
    {
        get
        {
            NativeMethods.saucer_window_background((SaucerWindow*)Handle, out byte r, out byte g, out byte b, out byte a);
            return (r, g, b, a);
        }
        set => NativeMethods.saucer_window_set_background((SaucerWindow*)Handle, value.R, value.G, value.B, value.A);
    }

    public SaucerWindowDecoration Decorations
    {
        get => (SaucerWindowDecoration)NativeMethods.saucer_window_decorations((SaucerWindow*)Handle);
        set => NativeMethods.saucer_window_set_decorations((SaucerWindow*)Handle, value);
    }

    public (int Width, int Height) Size
    {
        get
        {
            NativeMethods.saucer_window_size((SaucerWindow*)Handle, out int w, out int h);
            return (w, h);
        }
        set => NativeMethods.saucer_window_set_size((SaucerWindow*)Handle, value.Width, value.Height);
    }

    public (int Width, int Height) MaxSize
    {
        get
        {
            NativeMethods.saucer_window_max_size((SaucerWindow*)Handle, out int w, out int h);
            return (w, h);
        }
        set => NativeMethods.saucer_window_set_max_size((SaucerWindow*)Handle, value.Width, value.Height);
    }

    public (int Width, int Height) MinSize
    {
        get
        {
            NativeMethods.saucer_window_min_size((SaucerWindow*)Handle, out int w, out int h);
            return (w, h);
        }
        set => NativeMethods.saucer_window_set_min_size((SaucerWindow*)Handle, value.Width, value.Height);
    }

    public (int X, int Y) Position
    {
        get
        {
            NativeMethods.saucer_window_position((SaucerWindow*)Handle, out int x, out int y);
            return (x, y);
        }
        set => NativeMethods.saucer_window_set_position((SaucerWindow*)Handle, value.X, value.Y);
    }

    /// <summary>The screen the window is currently on.</summary>
    public Screen CurrentScreen => new Screen(NativeMethods.saucer_window_screen((SaucerWindow*)Handle));

    private readonly Dictionary<Delegate, nuint> _eventCallbacks = [];

    public event Action<Window, SaucerWindowDecoration>? Decorated
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWindowEventDecorated callback = (_, decoration, _) => value.Invoke(this, decoration);
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_window_on((SaucerWindow*)Handle, SaucerWindowEvent.Decorated, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_window_off((SaucerWindow*)Handle, SaucerWindowEvent.Decorated, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised when the window is maximized or restored.</summary>
    public event Action<Window, bool>? WindowMaximized
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWindowEventMaximize callback = (_, maximized, _) => value.Invoke(this, maximized);
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_window_on((SaucerWindow*)Handle, SaucerWindowEvent.Maximize, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_window_off((SaucerWindow*)Handle, SaucerWindowEvent.Maximize, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised when the window is minimized or restored.</summary>
    public event Action<Window, bool>? WindowMinimized
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWindowEventMinimize callback = (_, minimized, _) => value.Invoke(this, minimized);
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_window_on((SaucerWindow*)Handle, SaucerWindowEvent.Minimize, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_window_off((SaucerWindow*)Handle, SaucerWindowEvent.Minimize, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised after the window has been closed.</summary>
    public event Action<Window>? Closed
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWindowEventClosed callback = (_, _) => value.Invoke(this);
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_window_on((SaucerWindow*)Handle, SaucerWindowEvent.Closed, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_window_off((SaucerWindow*)Handle, SaucerWindowEvent.Closed, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised when the window is resized. Parameters are (width, height).</summary>
    public event Action<Window, (int Width, int Height)>? Resized
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWindowEventResize callback = (_, width, height, _) => value.Invoke(this, (width, height));
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_window_on((SaucerWindow*)Handle, SaucerWindowEvent.Resize, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_window_off((SaucerWindow*)Handle, SaucerWindowEvent.Resize, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised when the window gains or loses focus.</summary>
    public event Action<Window, bool>? FocusChanged
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWindowEventFocus callback = (_, focused, _) => value.Invoke(this, focused);
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_window_on((SaucerWindow*)Handle, SaucerWindowEvent.Focus, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_window_off((SaucerWindow*)Handle, SaucerWindowEvent.Focus, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised when the window is asked to close. Return <see cref="SaucerPolicy.Block"/> to prevent closing.</summary>
    public event Func<Window, SaucerPolicy>? CloseRequested
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWindowEventClose callback = (_, _) => value.Invoke(this);
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_window_on((SaucerWindow*)Handle, SaucerWindowEvent.Close, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_window_off((SaucerWindow*)Handle, SaucerWindowEvent.Close, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    public Window(Application app) : base((nint)NativeMethods.saucer_window_new((SaucerApplication*)app.Handle, out _))
    {

    }

    public void Show() => NativeMethods.saucer_window_show((SaucerWindow*)Handle);
    public void Hide() => NativeMethods.saucer_window_hide((SaucerWindow*)Handle);
    public void Close() => NativeMethods.saucer_window_close((SaucerWindow*)Handle);
    public void Focus() => NativeMethods.saucer_window_focus((SaucerWindow*)Handle);
    public void StartDrag() => NativeMethods.saucer_window_start_drag((SaucerWindow*)Handle);
    public void StartResize(SaucerWindowEdge edge) => NativeMethods.saucer_window_start_resize((SaucerWindow*)Handle, edge);

    /// <summary>Set the window icon.</summary>
    public void SetIcon(Icon icon) => NativeMethods.saucer_window_set_icon((SaucerWindow*)Handle, (SaucerIcon*)icon.Handle);

    public override void Free()
    {
        unsafe
        {
            NativeMethods.saucer_window_free((SaucerWindow*)Handle);
        }
    }
}
