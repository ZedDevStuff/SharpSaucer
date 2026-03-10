using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using SharpSaucer.Native;

namespace SharpSaucer;

public unsafe class WebView : StructWrapper
{
    /// <summary>The current URL. The caller owns the returned Url and must dispose it.</summary>
    public Url CurrentUrl => new Url(NativeMethods.saucer_webview_url((SaucerWebview*)Handle));

    /// <summary>The current page favicon. The caller owns the returned Icon and must dispose it.</summary>
    public Icon Favicon => new Icon(NativeMethods.saucer_webview_favicon((SaucerWebview*)Handle));

    /// <summary>The current page title.</summary>
    public string PageTitle
    {
        get
        {
            NativeMethods.saucer_webview_page_title((SaucerWebview*)Handle, 0, out var size);
            if (size == 0) return string.Empty;

            var buf = stackalloc byte[(int)size];
            NativeMethods.saucer_webview_page_title((SaucerWebview*)Handle, (nint)buf, out size);
            return Encoding.UTF8.GetString(buf, (int)size);
        }
    }

    /// <summary>Whether the developer tools are open.</summary>
    public bool DevTools
    {
        get => NativeMethods.saucer_webview_dev_tools((SaucerWebview*)Handle);
        set => NativeMethods.saucer_webview_set_dev_tools((SaucerWebview*)Handle, value);
    }

    /// <summary>Whether the context menu is enabled.</summary>
    public bool ContextMenu
    {
        get => NativeMethods.saucer_webview_context_menu((SaucerWebview*)Handle);
        set => NativeMethods.saucer_webview_set_context_menu((SaucerWebview*)Handle, value);
    }

    /// <summary>Whether dark mode is forced.</summary>
    public bool ForceDark
    {
        get => NativeMethods.saucer_webview_force_dark((SaucerWebview*)Handle);
        set => NativeMethods.saucer_webview_set_force_dark((SaucerWebview*)Handle, value);
    }

    /// <summary>The webview background colour (R, G, B, A).</summary>
    public (byte R, byte G, byte B, byte A) Background
    {
        get
        {
            NativeMethods.saucer_webview_background((SaucerWebview*)Handle, out byte r, out byte g, out byte b, out byte a);
            return (r, g, b, a);
        }
        set => NativeMethods.saucer_webview_set_background((SaucerWebview*)Handle, value.R, value.G, value.B, value.A);
    }

    /// <summary>The webview bounds (x, y, width, height).</summary>
    public (int X, int Y, int Width, int Height) Bounds
    {
        get
        {
            NativeMethods.saucer_webview_bounds((SaucerWebview*)Handle, out int x, out int y, out int w, out int h);
            return (x, y, w, h);
        }
        set => NativeMethods.saucer_webview_set_bounds((SaucerWebview*)Handle, value.X, value.Y, value.Width, value.Height);
    }
    private readonly Dictionary<Delegate, nuint> _eventCallbacks = [];
    /// <summary>Raised when a permission is requested. Return <see cref="SaucerStatus.Handled"/> to indicate the request was handled.</summary>
    public event Func<WebView, PermissionRequest, SaucerStatus>? PermissionRequested
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWebviewEventPermission callback = (_, request, _) => value.Invoke(this, new PermissionRequest((nint)request));
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_webview_on((SaucerWebview*)Handle, SaucerWebviewEvent.Permission, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_webview_off((SaucerWebview*)Handle, SaucerWebviewEvent.Permission, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised when the page requests fullscreen. Return <see cref="SaucerPolicy.Block"/> to deny.</summary>
    public event Func<WebView, bool, SaucerPolicy>? FullscreenRequested
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWebviewEventFullscreen callback = (_, fullscreen, _) => value.Invoke(this, fullscreen);
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_webview_on((SaucerWebview*)Handle, SaucerWebviewEvent.Fullscreen, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_webview_off((SaucerWebview*)Handle, SaucerWebviewEvent.Fullscreen, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised when the DOM is ready.</summary>
    public event Action<WebView>? DomReady
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWebviewEventDomReady callback = (_, _) => value.Invoke(this);
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_webview_on((SaucerWebview*)Handle, SaucerWebviewEvent.DomReady, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_webview_off((SaucerWebview*)Handle, SaucerWebviewEvent.DomReady, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised after the webview has navigated to a new URL.</summary>
    public event Action<WebView, Url>? Navigated
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWebviewEventNavigated callback = (_, url, _) => value.Invoke(this, new Url((nint)url));
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_webview_on((SaucerWebview*)Handle, SaucerWebviewEvent.Navigated, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_webview_off((SaucerWebview*)Handle, SaucerWebviewEvent.Navigated, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised before the webview navigates. Return <see cref="SaucerPolicy.Block"/> to prevent navigation.</summary>
    public event Func<WebView, Navigation, SaucerPolicy>? Navigate
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWebviewEventNavigate callback = (_, nav, _) => value.Invoke(this, new Navigation((nint)nav));
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_webview_on((SaucerWebview*)Handle, SaucerWebviewEvent.Navigate, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_webview_off((SaucerWebview*)Handle, SaucerWebviewEvent.Navigate, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised when a message is received from the page. Return <see cref="SaucerStatus.Handled"/> if consumed.</summary>
    public event Func<WebView, string, SaucerStatus>? MessageReceived
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWebviewEventMessage callback = (_, message, _, _) => value.Invoke(this, message ?? string.Empty);
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_webview_on((SaucerWebview*)Handle, SaucerWebviewEvent.Message, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_webview_off((SaucerWebview*)Handle, SaucerWebviewEvent.Message, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised when a resource is requested.</summary>
    public event Action<WebView, Url>? ResourceRequested
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWebviewEventRequest callback = (_, url, _) => value.Invoke(this, new Url((nint)url));
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_webview_on((SaucerWebview*)Handle, SaucerWebviewEvent.Request, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_webview_off((SaucerWebview*)Handle, SaucerWebviewEvent.Request, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised when the page favicon changes.</summary>
    public event Action<WebView, Icon>? FaviconChanged
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWebviewEventFavicon callback = (_, icon, _) => value.Invoke(this, new Icon((nint)icon));
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_webview_on((SaucerWebview*)Handle, SaucerWebviewEvent.Favicon, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_webview_off((SaucerWebview*)Handle, SaucerWebviewEvent.Favicon, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised when the page title changes.</summary>
    public event Action<WebView, string>? TitleChanged
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWebviewEventTitle callback = (_, title, _, _) => value.Invoke(this, title ?? string.Empty);
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_webview_on((SaucerWebview*)Handle, SaucerWebviewEvent.Title, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_webview_off((SaucerWebview*)Handle, SaucerWebviewEvent.Title, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    /// <summary>Raised when the page load state changes.</summary>
    public event Action<WebView, SaucerState>? LoadingStateChanged
    {
        add
        {
            if (value == null || _eventCallbacks.ContainsKey(value))
                return;
            unsafe
            {
                SaucerWebviewEventLoad callback = (_, state, _) => value.Invoke(this, state);
                var ptr = Marshal.GetFunctionPointerForDelegate(callback);
                _eventCallbacks[value] = NativeMethods.saucer_webview_on((SaucerWebview*)Handle, SaucerWebviewEvent.Load, ptr, true, nint.Zero);
            }
        }
        remove
        {
            if (_eventCallbacks.TryGetValue(value, out var id))
            {
                unsafe
                {
                    NativeMethods.saucer_webview_off((SaucerWebview*)Handle, SaucerWebviewEvent.Load, id);
                    _eventCallbacks.Remove(value);
                }
            }
        }
    }

    internal WebView(nint handle) : base(handle)
    {
    }
    internal unsafe WebView(SaucerWebview* handle) : base((nint)handle)
    {
    }

    public WebView(Window window)
    {
        unsafe
        {
            Handle = (nint)NativeMethods.saucer_webview_new(NativeMethods.saucer_webview_options_new((SaucerWindow*)window.Handle), out int error);
            if (error != 0)
                throw new Exception($"Failed to create webview. Error code: {error}");
        }
    }

    public void SetUrl(Url url) => NativeMethods.saucer_webview_set_url((SaucerWebview*)Handle, (SaucerUrl*)url.Handle);
    public void SetUrl(string url) => NativeMethods.saucer_webview_set_url_str((SaucerWebview*)Handle, url);
    public void SetHtml(string html) => NativeMethods.saucer_webview_set_html((SaucerWebview*)Handle, html);

    public void Back() => NativeMethods.saucer_webview_back((SaucerWebview*)Handle);
    public void Forward() => NativeMethods.saucer_webview_forward((SaucerWebview*)Handle);
    public void Reload() => NativeMethods.saucer_webview_reload((SaucerWebview*)Handle);

    public void ResetBounds() => NativeMethods.saucer_webview_reset_bounds((SaucerWebview*)Handle);

    public void Serve(string url) => NativeMethods.saucer_webview_serve((SaucerWebview*)Handle, url);

    public void Embed(string path, Stash content, string mime)
        => NativeMethods.saucer_webview_embed((SaucerWebview*)Handle, path, (SaucerStash*)content.Handle, mime);

    public void Unembed(string path) => NativeMethods.saucer_webview_unembed((SaucerWebview*)Handle, path);

    public void UnembedAll() => NativeMethods.saucer_webview_unembed_all((SaucerWebview*)Handle);
    public void Execute(string script) => NativeMethods.saucer_webview_execute((SaucerWebview*)Handle, script);

    public nuint Inject(string code, SaucerScriptTime runAt = SaucerScriptTime.Ready, bool noFrames = false, bool clearable = true)
        => NativeMethods.saucer_webview_inject((SaucerWebview*)Handle, code, runAt, noFrames, clearable);

    public void Uninject(nuint id) => NativeMethods.saucer_webview_uninject((SaucerWebview*)Handle, id);

    public void UninjectAll() => NativeMethods.saucer_webview_uninject_all((SaucerWebview*)Handle);

    public static void RegisterScheme(string scheme)
        => NativeMethods.saucer_webview_register_scheme(scheme);

    /// <summary>Register a handler for a custom scheme on this webview.</summary>
    public void HandleScheme(string scheme, Action<SchemeRequest, SchemeExecutor> handler, nint userdata = 0)
    {
        SaucerSchemeHandler callback = (request, executor, _) => handler(new(request), new(executor));
        NativeMethods.saucer_webview_handle_scheme((SaucerWebview*)Handle, scheme, callback, userdata);
    }

    /// <summary>Remove the handler for a custom scheme.</summary>
    public void RemoveScheme(string scheme)
        => NativeMethods.saucer_webview_remove_scheme((SaucerWebview*)Handle, scheme);

    public override void Free()
    {
        unsafe
        {
            NativeMethods.saucer_webview_free((SaucerWebview*)Handle);
        }
    }
}
