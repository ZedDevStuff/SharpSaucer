using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpSaucer;

public partial class SaucerWebview : IDisposable
{
    internal unsafe saucer_webview* Handle;
    private bool _disposedValue;
    private static readonly Dictionary<nint, SaucerWebview> Cache = [];

    public Color Background
    {
        get
        {
            unsafe
            {
                (byte r, byte g, byte b, byte a) = (0, 0, 0, 0);
                saucer_webview_background(Handle, ref r, ref g, ref b, ref a);
                return Color.FromArgb(a, r, g, b);
            }
        }
        set
        {
            unsafe
            {
                saucer_webview_set_background(Handle, value.R, value.G, value.B, value.A);
            }
        }
    }
    public (int X, int Y, int Width, int Height) Bounds
    {
        get
        {
            unsafe
            {
                saucer_webview_bounds(Handle, out int x, out int y, out int width, out int height);
                return (x, y, width, height);
            }
        }
        set
        {
            unsafe
            {
                saucer_webview_set_bounds(Handle, value.X, value.Y, value.Width, value.Height);
            }
        }
    }
    public bool ContextMenu
    {
        get
        {
            unsafe
            {
                return saucer_webview_context_menu(Handle);
            }
        }
        set
        {
            unsafe
            {
                saucer_webview_set_context_menu(Handle, value);
            }
        }
    }
    public bool DevTools
    {
        get
        {
            unsafe
            {
                return saucer_webview_dev_tools(Handle);
            }
        }
        set
        {
            unsafe
            {
                saucer_webview_set_dev_tools(Handle, value);
            }
        }
    }
    public bool ForceDark
    {
        get
        {
            unsafe
            {
                return saucer_webview_force_dark(Handle);
            }
        }
        set
        {
            unsafe
            {
                saucer_webview_set_force_dark(Handle, value);
            }
        }
    }
    public string PageTitle
    {
        get
        {
            unsafe
            {
                nuint size = 0;
                saucer_webview_page_title(Handle, null, ref size);
                fixed (sbyte* buffer = new sbyte[size])
                {
                    int length = (int)size;
                    saucer_webview_page_title(Handle, buffer, ref size);
                    return new string(buffer, 0, length, Encoding.UTF8);
                }
            }
        }
    }

    private readonly Dictionary<Delegate, nuint> _events = [];
    public event Func<SaucerWebview, SaucerPermissionRequest, SaucerStatus> PermissionRequested
    {
        add
        {
            if (value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWebviewEventPermission callback = (_, request, _) => value.Invoke(this, SaucerPermissionRequest.FromHandle(request));
                    GC.KeepAlive(callback);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_webview_on(Handle, SaucerWebviewEvent.Permission, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if (_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_webview_off(Handle, SaucerWebviewEvent.Permission, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Func<SaucerWebview, bool, SaucerPolicy> FullscreenRequested
    {
        add
        {
            if (value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWebviewEventFullscreen callback = (_, fullscreen, _) => value.Invoke(this, fullscreen);
                    GC.KeepAlive(callback); 
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_webview_on(Handle, SaucerWebviewEvent.Fullscreen, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if (_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_webview_off(Handle, SaucerWebviewEvent.Fullscreen, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Action<SaucerWebview> DomReady
    {
        add
        {
            if (value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWebviewEventDomReady callback = (_, _) => value.Invoke(this);
                    GC.KeepAlive(callback);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_webview_on(Handle, SaucerWebviewEvent.DomReady, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if (_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_webview_off(Handle, SaucerWebviewEvent.DomReady, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Action<SaucerWebview, SaucerUrl> Navigated
    {
        add
        {
            if (value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWebviewEventNavigated callback = (_, url, _) => value.Invoke(this, SaucerUrl.FromHandle(url));
                    GC.KeepAlive(callback); 
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_webview_on(Handle, SaucerWebviewEvent.Navigated, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if (_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_webview_off(Handle, SaucerWebviewEvent.Navigated, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Func<SaucerWebview, SaucerNavigation, SaucerPolicy> NavigationRequested
    {
        add
        {
            if (value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWebviewEventNavigate callback = (_, navigation, _) => value.Invoke(this, new SaucerNavigation(navigation));
                    GC.KeepAlive(callback);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_webview_on(Handle, SaucerWebviewEvent.Navigate, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if (_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_webview_off(Handle, SaucerWebviewEvent.Navigate, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Func<SaucerWebview, string, SaucerStatus> MessageReceived
    {
        add
        {
            if (value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWebviewEventMessage callback = (_, message, _, _) => value.Invoke(this, message);
                    GC.KeepAlive(callback);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_webview_on(Handle, SaucerWebviewEvent.Message, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if (_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_webview_off(Handle, SaucerWebviewEvent.Message, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Action<SaucerWebview, SaucerUrl> ResourceRequested
    {
        add
        {
            if (value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWebviewEventRequest callback = (_, url, _) => value.Invoke(this, SaucerUrl.FromHandle(url));
                    GC.KeepAlive(callback);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_webview_on(Handle, SaucerWebviewEvent.Request, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if (_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_webview_off(Handle, SaucerWebviewEvent.Request, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Action<SaucerWebview, SaucerIcon> FaviconLoaded
    {
        add
        {
            if (value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWebviewEventFavicon callback = (_, icon, _) => value.Invoke(this, SaucerIcon.FromHandle(icon));
                    GC.KeepAlive(callback);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_webview_on(Handle, SaucerWebviewEvent.Favicon, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if (_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_webview_off(Handle, SaucerWebviewEvent.Favicon, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Action<SaucerWebview, string> TitleChanged
    {
        add
        {
            if (value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWebviewEventTitle callback = (_, title, _, _) => value.Invoke(this, title);
                    GC.KeepAlive(callback);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_webview_on(Handle, SaucerWebviewEvent.Title, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if (_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_webview_off(Handle, SaucerWebviewEvent.Title, id);
                }
                _events.Remove(value);
            }
        }
    }
    public event Action<SaucerWebview, SaucerState> PageStateChanged
    {
        add
        {
            if (value != null && !_events.ContainsKey(value))
            {
                unsafe
                {
                    SaucerWebviewEventLoad callback = (_, state, _) => value.Invoke(this, state);
                    GC.KeepAlive(callback);
                    var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                    _events[value] = saucer_webview_on(Handle, SaucerWebviewEvent.Load, ptr, true, nint.Zero);
                }
            }
        }
        remove
        {
            if (_events.TryGetValue(value, out var id))
            {
                unsafe
                {
                    saucer_webview_off(Handle, SaucerWebviewEvent.Load, id);
                }
                _events.Remove(value);
            }
        }
    }

    public SaucerWebview(SaucerWindow window)
    {
        unsafe
        {
            Handle = saucer_webview_new(SaucerWebviewOptions.saucer_webview_options_new(window.Handle), out int error);
            if (error != 0)
                throw new Exception($"Failed to create webview. Error code: {error}");
        }
    }
    public SaucerWebview(SaucerWebviewOptions options)
    {
        unsafe
        {
            //Handle = saucer_webview_new(options.Handle, out int error);
            //if (error != 0)
            //    throw new Exception($"Failed to create webview. Error code: {error}");
        }
    }
    public void ResetBounds()
    {
        unsafe
        {
            saucer_webview_reset_bounds(Handle);
        }
    }
    public void Back()
    {
        unsafe
        {
            saucer_webview_back(Handle);
        }
    }
    public void Forward()
    {
        unsafe
        {
            saucer_webview_forward(Handle);
        }
    }
    public void Reload()
    {
        unsafe
        {
            saucer_webview_reload(Handle);
        }
    }

    public void HandleScheme(string scheme, SaucerSchemeHandler handler)
    {
        unsafe
        {
            SaucerSchemeHandlerRaw callback = (request, executor, _) => handler(SaucerSchemeRequest.FromHandle(request), SaucerSchemeExecutor.FromHandle(executor));
            GC.KeepAlive(callback);
            saucer_webview_handle_scheme(Handle, scheme, callback, nint.Zero);
        }
    }
    public void RemoveScheme(string scheme)
    {
        unsafe
        {
            saucer_webview_remove_scheme(Handle, scheme);
        }
    }

    public void SetUrl(string url)
    {
        unsafe
        {
            saucer_webview_set_url_str(Handle, url);
        }
    }
    public void SetUrl(SaucerUrl url)
    {
        throw new NotImplementedException();
    }

    public void SetHtml(string html)
    {
        unsafe
        {
            saucer_webview_set_html(Handle, html);
        }
    }
    public void Execute(string js)
    {
        unsafe
        {
            saucer_webview_execute(Handle, js);
        }
    }
    public nuint Inject(string js, SaucerScriptTime runAt, bool noFrames)
    {
        unsafe
        {
            return saucer_webview_inject(Handle, js, runAt, noFrames, true);
        }
    }
    public void Uninject(nuint id)
    {
        unsafe
        {
            saucer_webview_uninject(Handle, id);
        }
    }
    public void UninjectAll()
    {
        unsafe
        {
            saucer_webview_uninject_all(Handle);
        }
    }

    public static void RegisterScheme(string scheme)
    {
        unsafe
        {
            saucer_webview_register_scheme(scheme);
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
                saucer_webview_free(Handle);
            }
            _disposedValue = true;
        }
    }

    ~SaucerWebview()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
