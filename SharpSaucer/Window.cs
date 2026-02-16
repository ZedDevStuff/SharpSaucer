using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using SharpSaucer.Types;

namespace SharpSaucer;

/// <summary>
/// Managed wrapper around a native saucer window.
/// </summary>
public sealed class Window : IDisposable
{
    private nint _handle;
    private bool _disposedValue;

    // prevent GC of delegates passed to native code
    private readonly List<Delegate> _pinnedDelegates = [];
    private readonly NativeEventSubscription<Action<SaucerWindowDecoration>> _decoratedSubs = new();
    private readonly NativeEventSubscription<Action<bool>> _maximizeSubs = new();
    private readonly NativeEventSubscription<Action<bool>> _minimizeSubs = new();
    private readonly NativeEventSubscription<Action> _closedSubs = new();
    private readonly NativeEventSubscription<Action<int, int>> _resizeSubs = new();
    private readonly NativeEventSubscription<Action<bool>> _focusSubs = new();
    private readonly NativeEventSubscription<Func<SaucerPolicy>> _closeSubs = new();

    /// <summary>The underlying native handle.</summary>
    public nint Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposedValue, this);
            return _handle;
        }
    }

    private Window(nint handle)
    {
        if (handle == 0)
            throw new InvalidOperationException("Failed to create Window.");

        _handle = handle;
    }

    // ── Constructors ────────────────────────────

    /// <summary>Create a new window for the given application.</summary>
    public Window(Application application)
    {
        ArgumentNullException.ThrowIfNull(application);
        int error = 0;
        _handle = Bindings.saucer_window_new(application.Handle, ref error);

        if (error != 0 || _handle == 0)
            throw new InvalidOperationException($"Failed to create window (error={error}).");
    }

    // ── Factories ───────────────────────────────

    /// <summary>Create a new window for the given application.</summary>
    public static Window Create(Application application)
    {
        int error = 0;
        var handle = Bindings.saucer_window_new(application.Handle, ref error);
        if (error != 0 || handle == 0)
            throw new InvalidOperationException($"Failed to create window (error={error}).");

        return new Window(handle);
    }

    // ── Boolean Properties ──────────────────────

    /// <summary>Whether the window is currently visible.</summary>
    public bool Visible => Bindings.saucer_window_visible(Handle);

    /// <summary>Whether the window currently has keyboard focus.</summary>
    public bool Focused => Bindings.saucer_window_focused(Handle);

    /// <summary>Whether the window is minimized.</summary>
    public bool Minimized
    {
        get => Bindings.saucer_window_minimized(Handle);
        set => Bindings.saucer_window_set_minimized(Handle, value);
    }

    /// <summary>Whether the window is maximized.</summary>
    public bool Maximized
    {
        get => Bindings.saucer_window_maximized(Handle);
        set => Bindings.saucer_window_set_maximized(Handle, value);
    }

    /// <summary>Whether the window is resizable.</summary>
    public bool Resizable
    {
        get => Bindings.saucer_window_resizable(Handle);
        set => Bindings.saucer_window_set_resizable(Handle, value);
    }

    /// <summary>Whether the window is in fullscreen mode.</summary>
    public bool Fullscreen
    {
        get => Bindings.saucer_window_fullscreen(Handle);
        set => Bindings.saucer_window_set_fullscreen(Handle, value);
    }

    /// <summary>Whether the window is always on top of other windows.</summary>
    public bool AlwaysOnTop
    {
        get => Bindings.saucer_window_always_on_top(Handle);
        set => Bindings.saucer_window_set_always_on_top(Handle, value);
    }

    /// <summary>Whether mouse clicks pass through the window.</summary>
    public bool ClickThrough
    {
        get => Bindings.saucer_window_click_through(Handle);
        set => Bindings.saucer_window_set_click_through(Handle, value);
    }

    // ── String / Value Properties ───────────────

    /// <summary>The window title.</summary>
    public unsafe string Title
    {
        get
        {
            nuint size = 0;
            Bindings.saucer_window_title(Handle, 0, ref size);
            if (size == 0) return string.Empty;

            var buf = stackalloc byte[(int)size];
            Bindings.saucer_window_title(Handle, (nint)buf, ref size);
            return Encoding.UTF8.GetString(buf, (int)size);
        }
        set => Bindings.saucer_window_set_title(Handle, value);
    }

    /// <summary>The window background colour (R, G, B, A).</summary>
    public (byte R, byte G, byte B, byte A) Background
    {
        get
        {
            Bindings.saucer_window_background(Handle, out byte r, out byte g, out byte b, out byte a);
            return (r, g, b, a);
        }
        set => Bindings.saucer_window_set_background(Handle, value.R, value.G, value.B, value.A);
    }

    /// <summary>The window decoration style.</summary>
    public SaucerWindowDecoration Decorations
    {
        get => (SaucerWindowDecoration)Bindings.saucer_window_decorations(Handle);
        set => Bindings.saucer_window_set_decorations(Handle, value);
    }

    /// <summary>The window size in pixels (width, height).</summary>
    public (int Width, int Height) Size
    {
        get
        {
            Bindings.saucer_window_size(Handle, out int w, out int h);
            return (w, h);
        }
        set => Bindings.saucer_window_set_size(Handle, value.Width, value.Height);
    }

    /// <summary>The maximum window size in pixels (width, height).</summary>
    public (int Width, int Height) MaxSize
    {
        get
        {
            Bindings.saucer_window_max_size(Handle, out int w, out int h);
            return (w, h);
        }
        set => Bindings.saucer_window_set_max_size(Handle, value.Width, value.Height);
    }

    /// <summary>The minimum window size in pixels (width, height).</summary>
    public (int Width, int Height) MinSize
    {
        get
        {
            Bindings.saucer_window_min_size(Handle, out int w, out int h);
            return (w, h);
        }
        set => Bindings.saucer_window_set_min_size(Handle, value.Width, value.Height);
    }

    /// <summary>The window position (x, y).</summary>
    public (int X, int Y) Position
    {
        get
        {
            Bindings.saucer_window_position(Handle, out int x, out int y);
            return (x, y);
        }
        set => Bindings.saucer_window_set_position(Handle, value.X, value.Y);
    }

    /// <summary>The screen the window is currently on.</summary>
    public Screen CurrentScreen => Screen.FromHandle(Bindings.saucer_window_screen(Handle));

    // ── Methods ─────────────────────────────────

    /// <summary>Show the window.</summary>
    public void Show() => Bindings.saucer_window_show(Handle);

    /// <summary>Hide the window.</summary>
    public void Hide() => Bindings.saucer_window_hide(Handle);

    /// <summary>Close the window.</summary>
    public void Close() => Bindings.saucer_window_close(Handle);

    /// <summary>Give the window keyboard focus.</summary>
    public void Focus() => Bindings.saucer_window_focus(Handle);

    /// <summary>Begin an interactive drag operation on the window.</summary>
    public void StartDrag() => Bindings.saucer_window_start_drag(Handle);

    /// <summary>Begin an interactive resize from the given edge(s).</summary>
    public void StartResize(SaucerWindowEdge edge) => Bindings.saucer_window_start_resize(Handle, edge);

    /// <summary>Set the window icon.</summary>
    public void SetIcon(Icon icon) => Bindings.saucer_window_set_icon(Handle, icon.Handle);

    // ── Events ──────────────────────────────────

    /// <summary>Register a persistent event handler. Returns a subscription ID.</summary>
    public nuint On(SaucerWindowEvent @event, Delegate callback, bool clearable = true)
    {
        _pinnedDelegates.Add(callback);
        var ptr = Marshal.GetFunctionPointerForDelegate(callback);
        return Bindings.saucer_window_on(Handle, @event, ptr, clearable, 0);
    }

    /// <summary>Register a one-shot event handler.</summary>
    public void Once(SaucerWindowEvent @event, Delegate callback)
    {
        _pinnedDelegates.Add(callback);
        var ptr = Marshal.GetFunctionPointerForDelegate(callback);
        Bindings.saucer_window_once(Handle, @event, ptr, 0);
    }

    /// <summary>Remove a specific event handler by subscription ID.</summary>
    public void Off(SaucerWindowEvent @event, nuint id)
        => Bindings.saucer_window_off(Handle, @event, id);

    /// <summary>Remove all event handlers for the given event.</summary>
    public void OffAll(SaucerWindowEvent @event)
        => Bindings.saucer_window_off_all(Handle, @event);

    // ── C# Events ───────────────────────────────

    /// <summary>Raised when the window decoration style changes.</summary>
    public event Action<SaucerWindowDecoration>? DecorationChanged
    {
        add
        {
            ArgumentNullException.ThrowIfNull(value);
            SaucerWindowEventDecoratedCallback native = (_, decoration, _) => value(decoration);
            _pinnedDelegates.Add(native);
            var ptr = Marshal.GetFunctionPointerForDelegate(native);
            var id = Bindings.saucer_window_on(Handle, SaucerWindowEvent.Decorated, ptr, true, 0);
            _decoratedSubs.Add(value, id, native);
        }
        remove
        {
            ArgumentNullException.ThrowIfNull(value);
            if (_decoratedSubs.TryRemove(value, out var id))
                Bindings.saucer_window_off(Handle, SaucerWindowEvent.Decorated, id);
        }
    }

    /// <summary>Raised when the window is maximized or restored.</summary>
    public event Action<bool>? WindowMaximized
    {
        add
        {
            ArgumentNullException.ThrowIfNull(value);
            SaucerWindowEventMaximizeCallback native = (_, maximized, _) => value(maximized);
            _pinnedDelegates.Add(native);
            var ptr = Marshal.GetFunctionPointerForDelegate(native);
            var id = Bindings.saucer_window_on(Handle, SaucerWindowEvent.Maximize, ptr, true, 0);
            _maximizeSubs.Add(value, id, native);
        }
        remove
        {
            ArgumentNullException.ThrowIfNull(value);
            if (_maximizeSubs.TryRemove(value, out var id))
                Bindings.saucer_window_off(Handle, SaucerWindowEvent.Maximize, id);
        }
    }

    /// <summary>Raised when the window is minimized or restored.</summary>
    public event Action<bool>? WindowMinimized
    {
        add
        {
            ArgumentNullException.ThrowIfNull(value);
            SaucerWindowEventMinimizeCallback native = (_, minimized, _) => value(minimized);
            _pinnedDelegates.Add(native);
            var ptr = Marshal.GetFunctionPointerForDelegate(native);
            var id = Bindings.saucer_window_on(Handle, SaucerWindowEvent.Minimize, ptr, true, 0);
            _minimizeSubs.Add(value, id, native);
        }
        remove
        {
            ArgumentNullException.ThrowIfNull(value);
            if (_minimizeSubs.TryRemove(value, out var id))
                Bindings.saucer_window_off(Handle, SaucerWindowEvent.Minimize, id);
        }
    }

    /// <summary>Raised after the window has been closed.</summary>
    public event Action? Closed
    {
        add
        {
            ArgumentNullException.ThrowIfNull(value);
            SaucerWindowEventClosedCallback native = (_, _) => value();
            _pinnedDelegates.Add(native);
            var ptr = Marshal.GetFunctionPointerForDelegate(native);
            var id = Bindings.saucer_window_on(Handle, SaucerWindowEvent.Closed, ptr, true, 0);
            _closedSubs.Add(value, id, native);
        }
        remove
        {
            ArgumentNullException.ThrowIfNull(value);
            if (_closedSubs.TryRemove(value, out var id))
                Bindings.saucer_window_off(Handle, SaucerWindowEvent.Closed, id);
        }
    }

    /// <summary>Raised when the window is resized. Parameters are (width, height).</summary>
    public event Action<int, int>? Resized
    {
        add
        {
            ArgumentNullException.ThrowIfNull(value);
            SaucerWindowEventResizeCallback native = (_, w, h, _) => value(w, h);
            _pinnedDelegates.Add(native);
            var ptr = Marshal.GetFunctionPointerForDelegate(native);
            var id = Bindings.saucer_window_on(Handle, SaucerWindowEvent.Resize, ptr, true, 0);
            _resizeSubs.Add(value, id, native);
        }
        remove
        {
            ArgumentNullException.ThrowIfNull(value);
            if (_resizeSubs.TryRemove(value, out var id))
                Bindings.saucer_window_off(Handle, SaucerWindowEvent.Resize, id);
        }
    }

    /// <summary>Raised when the window gains or loses focus.</summary>
    public event Action<bool>? FocusChanged
    {
        add
        {
            ArgumentNullException.ThrowIfNull(value);
            SaucerWindowEventFocusCallback native = (_, focused, _) => value(focused);
            _pinnedDelegates.Add(native);
            var ptr = Marshal.GetFunctionPointerForDelegate(native);
            var id = Bindings.saucer_window_on(Handle, SaucerWindowEvent.Focus, ptr, true, 0);
            _focusSubs.Add(value, id, native);
        }
        remove
        {
            ArgumentNullException.ThrowIfNull(value);
            if (_focusSubs.TryRemove(value, out var id))
                Bindings.saucer_window_off(Handle, SaucerWindowEvent.Focus, id);
        }
    }

    /// <summary>Raised when the window is asked to close. Return <see cref="SaucerPolicy.Block"/> to prevent closing.</summary>
    public event Func<SaucerPolicy>? CloseRequested
    {
        add
        {
            ArgumentNullException.ThrowIfNull(value);
            SaucerWindowEventCloseCallback native = (_, _) => value();
            _pinnedDelegates.Add(native);
            var ptr = Marshal.GetFunctionPointerForDelegate(native);
            var id = Bindings.saucer_window_on(Handle, SaucerWindowEvent.Close, ptr, true, 0);
            _closeSubs.Add(value, id, native);
        }
        remove
        {
            ArgumentNullException.ThrowIfNull(value);
            if (_closeSubs.TryRemove(value, out var id))
                Bindings.saucer_window_off(Handle, SaucerWindowEvent.Close, id);
        }
    }

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _pinnedDelegates.Clear();
            }
            if (_handle != 0)
            {
                Bindings.saucer_window_free(_handle);
                _handle = 0;
            }
            _disposedValue = true;
        }
    }

    ~Window()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
