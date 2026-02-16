using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using SharpSaucer.Types;

namespace SharpSaucer;

/// <summary>
/// Managed wrapper around a native saucer application.
/// </summary>
public sealed class Application : IDisposable
{
    private nint _handle;
    private bool _disposedValue;

    // prevent GC of delegates passed to native code
    private readonly List<Delegate> _pinnedDelegates = [];
    private readonly NativeEventSubscription<Func<SaucerPolicy>> _quitSubs = new();

    /// <summary>The underlying native handle.</summary>
    public nint Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposedValue, this);
            return _handle;
        }
    }

    private Application(nint handle)
    {
        if (handle == 0)
            throw new InvalidOperationException("Failed to create Application.");

        _handle = handle;
    }

    // ── Constructors ────────────────────────────

    /// <summary>Create a new saucer application with the given ID.</summary>
    public Application(string id, bool quitOnLastWindowClosed = true)
    {
        ArgumentNullException.ThrowIfNull(id);

        var opts = Bindings.saucer_application_options_new(id);
        if (opts == 0)
            throw new InvalidOperationException("Failed to create application options.");

        Bindings.saucer_application_options_set_quit_on_last_window_closed(opts, quitOnLastWindowClosed);

        int error = 0;
        _handle = Bindings.saucer_application_new(opts, ref error);
        Bindings.saucer_application_options_free(opts);

        if (error != 0 || _handle == 0)
            throw new InvalidOperationException($"Failed to create application (error={error}).");
    }

    // ── Factories ───────────────────────────────

    /// <summary>Create a new saucer application with the given ID.</summary>
    public static Application Create(string id, bool quitOnLastWindowClosed = true)
    {
        var opts = Bindings.saucer_application_options_new(id);
        if (opts == 0)
            throw new InvalidOperationException("Failed to create application options.");

        Bindings.saucer_application_options_set_quit_on_last_window_closed(opts, quitOnLastWindowClosed);

        int error = 0;
        var handle = Bindings.saucer_application_new(opts, ref error);
        Bindings.saucer_application_options_free(opts);

        if (error != 0 || handle == 0)
            throw new InvalidOperationException($"Failed to create application (error={error}).");

        return new Application(handle);
    }

    // ── Properties ──────────────────────────────

    /// <summary>Whether the current call is on the application's main thread.</summary>
    public bool ThreadSafe => Bindings.saucer_application_thread_safe(Handle);

    /// <summary>The saucer library version string.</summary>
    public static string Version
    {
        get
        {
            var ptr = Bindings.saucer_version();
            return ptr == 0 ? string.Empty : Marshal.PtrToStringUTF8(ptr) ?? string.Empty;
        }
    }

    // ── Methods ─────────────────────────────────

    /// <summary>Get the list of available screens.</summary>
    public unsafe Screen[] GetScreens()
    {
        Bindings.saucer_application_screens(Handle, out var ptr, out var count);
        if (ptr == 0 || count == 0)
            return [];

        var screens = new Screen[(int)count];
        var ptrSpan = new ReadOnlySpan<nint>((void*)ptr, (int)count);
        for (int i = 0; i < (int)count; i++)
            screens[i] = Screen.FromHandle(ptrSpan[i]);

        // free the array allocated by native code (not the individual screens)
        Marshal.FreeHGlobal(ptr);
        return screens;
    }

    /// <summary>Post a callback to be executed on the application's main thread.</summary>
    public void Post(Action callback)
    {
        SaucerPostCallback native = _ => callback();
        _pinnedDelegates.Add(native);
        Bindings.saucer_application_post(Handle, native, 0);
    }

    /// <summary>Quit the application.</summary>
    public void Quit() => Bindings.saucer_application_quit(Handle);

    /// <summary>Run the application event loop.</summary>
    public int Run(Action<Application>? onRun = null, Action<Application>? onFinish = null)
    {
        SaucerRunCallback? runCb = null;
        SaucerFinishCallback? finishCb = null;

        if (onRun != null)
        {
            runCb = (_, _) => onRun(this);
            _pinnedDelegates.Add(runCb);
        }

        if (onFinish != null)
        {
            finishCb = (_, _) => onFinish(this);
            _pinnedDelegates.Add(finishCb);
        }

        return Bindings.saucer_application_run(
            Handle,
            runCb!,
            finishCb!,
            0);
    }

    /// <summary>Register a persistent event handler. Returns a subscription ID.</summary>
    public nuint On(SaucerApplicationEvent @event, SaucerApplicationEventQuitCallback callback, bool clearable = true)
    {
        _pinnedDelegates.Add(callback);
        var ptr = Marshal.GetFunctionPointerForDelegate(callback);
        return Bindings.saucer_application_on(Handle, @event, ptr, clearable, 0);
    }

    /// <summary>Register a one-shot event handler.</summary>
    public void Once(SaucerApplicationEvent @event, SaucerApplicationEventQuitCallback callback)
    {
        _pinnedDelegates.Add(callback);
        var ptr = Marshal.GetFunctionPointerForDelegate(callback);
        Bindings.saucer_application_once(Handle, @event, ptr, 0);
    }

    /// <summary>Remove a specific event handler by subscription ID.</summary>
    public void Off(SaucerApplicationEvent @event, nuint id)
        => Bindings.saucer_application_off(Handle, @event, id);

    /// <summary>Remove all event handlers for the given event.</summary>
    public void OffAll(SaucerApplicationEvent @event)
        => Bindings.saucer_application_off_all(Handle, @event);

    // ── C# Events ───────────────────────────────

    /// <summary>Raised when the application is asked to quit. Return <see cref="SaucerPolicy.Block"/> to prevent quitting.</summary>
    public event Func<SaucerPolicy>? QuitRequested
    {
        add
        {
            ArgumentNullException.ThrowIfNull(value);
            SaucerApplicationEventQuitCallback native = (_, _) => value();
            _pinnedDelegates.Add(native);
            var ptr = Marshal.GetFunctionPointerForDelegate(native);
            var id = Bindings.saucer_application_on(Handle, SaucerApplicationEvent.Quit, ptr, true, 0);
            _quitSubs.Add(value, id, native);
        }
        remove
        {
            ArgumentNullException.ThrowIfNull(value);
            if (_quitSubs.TryRemove(value, out var id))
                Bindings.saucer_application_off(Handle, SaucerApplicationEvent.Quit, id);
        }
    }

    // ── IDisposable ─────────────────────────────

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

    ~Application()
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
