using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SharpSaucer;

public partial class SaucerApplication : IDisposable
{
    internal SaucerApplicationHandle Handle;
    internal SaucerApplicationOptionsHandle OptionsHandle;

    private readonly Dictionary<Delegate, nuint> _events = [];

    public event Func<SaucerApplication, SaucerPolicy>? QuitRequested
    {
        add
        {
            if (value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerApplicationEventQuitNative callback = (_, _) => value.Invoke(this);
                    GC.KeepAlive(callback);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_application_on(Handle, SaucerApplicationEvent.Quit, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if (_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_application_off(Handle, SaucerApplicationEvent.Quit, id);
                }
                _events.Remove(value);
            }
        }
    }

    public SaucerScreen[] Screens
    {
        get
        {
            unsafe
            {
                nuint size = 0;
                saucer_application_screens(Handle, null, ref size);
                fixed (saucer_screen** buffer = new saucer_screen*[size])
                {
                    int length = (int)size;
                    saucer_application_screens(Handle, buffer, ref size);
                    SaucerScreen[] screens = new SaucerScreen[length];
                    for (int i = 0; i < length; i++)
                    {
                        screens[i] = new SaucerScreen(new((nint)buffer[i]));
                    }
                    return screens;
                }
            }
        }
    }

    public SaucerApplication(string id)
    {
        unsafe
        {
            OptionsHandle = SaucerApplicationOptions.saucer_application_options_new(id);
            Handle = saucer_application_new(OptionsHandle, out var error);
            if (error != 0)
                throw new Exception($"Failed to create application. Error code: {error}");
        }
    }

    public void Post(Action action)
    {
        unsafe
        {
            SaucerPostCallbackNative callback = _ => action();
            saucer_application_post(Handle, callback, nint.Zero);
        }
    }

    public int Run(Action<SaucerApplication> onRun, Action<SaucerApplication> onFinish)
    {
        unsafe
        {
            SaucerRunCallbackNative runCb = (_, _) => onRun(this);
            SaucerFinishCallbackNative finishCb = (_, _) => onFinish(this);
            return saucer_application_run(Handle, runCb, finishCb, nint.Zero);
        }
    }

    public void Dispose()
    {
        Handle.Dispose();
        OptionsHandle.Dispose();
    }

    public string Version
    {
        get
        {
            unsafe
            {
                return Marshal.PtrToStringAnsi(saucer_version()) ?? string.Empty;
            }
        }
    }
}
