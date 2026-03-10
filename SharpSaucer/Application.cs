using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

using SharpSaucer.Native;

namespace SharpSaucer;

public class Application : StructWrapper
{
    private readonly Dictionary<Func<Application, SaucerPolicy>, nuint> _quitCallbacks = [];
    public event Func<Application, SaucerPolicy>? QuitRequested
    {
        add
        {
            if (value == null || _quitCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerApplicationEventQuit quitCb = (_, _) => value.Invoke(this);
                var ptr = Marshal.GetFunctionPointerForDelegate(quitCb);
                _quitCallbacks[value] = NativeMethods.saucer_application_on((SaucerApplication*)Handle, (int)SaucerApplicationEvent.Quit, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_quitCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_application_off((SaucerApplication*)Handle, (int)SaucerApplicationEvent.Quit, id);
                    _quitCallbacks.Remove(value);
                }
            }
        }
    }

    public Application(string id) : base()
    {
        unsafe
        {
            Handle = (nint)NativeMethods.saucer_application_new(NativeMethods.saucer_application_options_new(id), out int error);
            if (error != 0)
                throw new Exception($"Failed to create application. Error code: {error}");
        }
    }

    public Screen[] GetScreens()
    {
        unsafe
        {
            NativeMethods.saucer_application_screens((SaucerApplication*)Handle, out SaucerScreen* screens, out nuint count);
            var span = new ReadOnlySpan<SaucerScreen>(screens, (int)count);
            return [.. span.ToArray().Select(s => new Screen((nint)(&s)))];
        }
    }
    /// <summary>
    /// Run the application's main loop.
    /// </summary>
    /// <param name="onRun"></param>
    /// <param name="onFinish"></param>
    /// <returns></returns>
    public int Run(Action<Application>? onRun = null, Action<Application>? onFinish = null)
    {
        unsafe
        {
            SaucerRunCallback? runCb = null;
            SaucerFinishCallback? finishCb = null;

            if (onRun != null)
                runCb = (_, _) => onRun(this);
            if (onFinish != null)
                finishCb = (_, _) => onFinish(this);

            return NativeMethods.saucer_application_run((SaucerApplication*)Handle, runCb, finishCb, nint.Zero);
        }
    }

    public void Once(SaucerApplicationEvent eventType, Func<Application, SaucerPolicy> callback)
    {
        unsafe
        {
            SaucerApplicationEventQuit cb = (_, _) => callback(this);
            var ptr = Marshal.GetFunctionPointerForDelegate(cb);
            NativeMethods.saucer_application_on((SaucerApplication*)Handle, eventType, ptr, false, nint.Zero);
        }
    }

    /// <summary>
    /// Executes the given action on the <see cref="Application"/>'s main thread.
    /// </summary>
    /// <param name="action"></param>
    public void Post(Action action)
    {
        unsafe
        {
            SaucerPostCallback cb = (_) => action();
            var ptr = Marshal.GetFunctionPointerForDelegate(cb);
            NativeMethods.saucer_application_post((SaucerApplication*)Handle, cb, nint.Zero);
        }
    }

    public void Quit()
    {
        unsafe
        {
            NativeMethods.saucer_application_quit((SaucerApplication*)Handle);
        }
    }

    public override void Free()
    {
        unsafe
        {
            NativeMethods.saucer_application_free((SaucerApplication*)Handle);
        }
    }
}
