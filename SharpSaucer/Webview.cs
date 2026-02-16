using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using SharpSaucer.Types;

namespace SharpSaucer;

/// <summary>
/// Managed wrapper around a native saucer webview.
/// </summary>
public sealed class Webview : IDisposable
{
    private nint _handle;
    private bool _disposed;

    // prevent GC of delegates passed to native code
    private readonly List<Delegate> _pinnedDelegates = [];

    /// <summary>The underlying native handle.</summary>
    public nint Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return _handle;
        }
    }

    private Webview(nint handle)
    {
        if (handle == 0)
            throw new InvalidOperationException("Failed to create Webview.");

        _handle = handle;
    }

    // ── Constructors ────────────────────────────

    /// <summary>Create a new webview for the given window with default options.</summary>
    public Webview(Window window)
    {
        ArgumentNullException.ThrowIfNull(window);

        var opts = Bindings.saucer_webview_options_new(window.Handle);
        if (opts == 0)
            throw new InvalidOperationException("Failed to create webview options.");

        int error = 0;
        _handle = Bindings.saucer_webview_new(opts, ref error);
        Bindings.saucer_webview_options_free(opts);

        if (error != 0 || _handle == 0)
            throw new InvalidOperationException($"Failed to create webview (error={error}).");
    }

    // ── Factories ───────────────────────────────

    /// <summary>Create a new webview for the given window.</summary>
    public static Webview Create(
        Window window,
        bool hardwareAcceleration = true,
        bool persistentCookies = false,
        bool attributes = true,
        string? storagePath = null,
        string? userAgent = null,
        string[]? browserFlags = null)
    {
        var opts = Bindings.saucer_webview_options_new(window.Handle);
        if (opts == 0)
            throw new InvalidOperationException("Failed to create webview options.");

        Bindings.saucer_webview_options_set_hardware_acceleration(opts, hardwareAcceleration);
        Bindings.saucer_webview_options_set_persistent_cookies(opts, persistentCookies);
        Bindings.saucer_webview_options_set_attributes(opts, attributes);

        if (storagePath != null)
            Bindings.saucer_webview_options_set_storage_path(opts, storagePath);

        if (userAgent != null)
            Bindings.saucer_webview_options_set_user_agent(opts, userAgent);

        if (browserFlags != null)
        {
            foreach (var flag in browserFlags)
                Bindings.saucer_webview_options_append_browser_flag(opts, flag);
        }

        int error = 0;
        var handle = Bindings.saucer_webview_new(opts, ref error);
        Bindings.saucer_webview_options_free(opts);

        if (error != 0 || handle == 0)
            throw new InvalidOperationException($"Failed to create webview (error={error}).");

        return new Webview(handle);
    }

    // ── Properties ──────────────────────────────

    /// <summary>The current URL. The caller owns the returned Url and must dispose it.</summary>
    public Url CurrentUrl => Url.FromHandle(Bindings.saucer_webview_url(Handle));

    /// <summary>The current page favicon. The caller owns the returned Icon and must dispose it.</summary>
    public Icon Favicon => Icon.FromHandle(Bindings.saucer_webview_favicon(Handle));

    /// <summary>The current page title.</summary>
    public unsafe string PageTitle
    {
        get
        {
            nuint size = 0;
            Bindings.saucer_webview_page_title(Handle, 0, ref size);
            if (size == 0) return string.Empty;

            var buf = stackalloc byte[(int)size];
            Bindings.saucer_webview_page_title(Handle, (nint)buf, ref size);
            return Encoding.UTF8.GetString(buf, (int)size);
        }
    }

    /// <summary>Whether the developer tools are open.</summary>
    public bool DevTools
    {
        get => Bindings.saucer_webview_dev_tools(Handle);
        set => Bindings.saucer_webview_set_dev_tools(Handle, value);
    }

    /// <summary>Whether the context menu is enabled.</summary>
    public bool ContextMenu
    {
        get => Bindings.saucer_webview_context_menu(Handle);
        set => Bindings.saucer_webview_set_context_menu(Handle, value);
    }

    /// <summary>Whether dark mode is forced.</summary>
    public bool ForceDark
    {
        get => Bindings.saucer_webview_force_dark(Handle);
        set => Bindings.saucer_webview_set_force_dark(Handle, value);
    }

    /// <summary>The webview background colour (R, G, B, A).</summary>
    public (byte R, byte G, byte B, byte A) Background
    {
        get
        {
            Bindings.saucer_webview_background(Handle, out byte r, out byte g, out byte b, out byte a);
            return (r, g, b, a);
        }
        set => Bindings.saucer_webview_set_background(Handle, value.R, value.G, value.B, value.A);
    }

    /// <summary>The webview bounds (x, y, width, height).</summary>
    public (int X, int Y, int Width, int Height) Bounds
    {
        get
        {
            Bindings.saucer_webview_bounds(Handle, out int x, out int y, out int w, out int h);
            return (x, y, w, h);
        }
        set => Bindings.saucer_webview_set_bounds(Handle, value.X, value.Y, value.Width, value.Height);
    }

    // ── Navigation Methods ──────────────────────

    /// <summary>Navigate to a URL object.</summary>
    public void SetUrl(Url url) => Bindings.saucer_webview_set_url(Handle, url.Handle);

    /// <summary>Navigate to a URL string.</summary>
    public void SetUrl(string url) => Bindings.saucer_webview_set_url_str(Handle, url);

    /// <summary>Set the webview content to raw HTML.</summary>
    public void SetHtml(string html) => Bindings.saucer_webview_set_html(Handle, html);

    /// <summary>Navigate back.</summary>
    public void Back() => Bindings.saucer_webview_back(Handle);

    /// <summary>Navigate forward.</summary>
    public void Forward() => Bindings.saucer_webview_forward(Handle);

    /// <summary>Reload the current page.</summary>
    public void Reload() => Bindings.saucer_webview_reload(Handle);

    /// <summary>Reset the webview bounds to default.</summary>
    public void ResetBounds() => Bindings.saucer_webview_reset_bounds(Handle);

    // ── Serving / Embedding ─────────────────────

    /// <summary>Serve content from the given scheme URL.</summary>
    public void Serve(string url) => Bindings.saucer_webview_serve(Handle, url);

    /// <summary>Embed a resource at the given path with content and MIME type.</summary>
    public void Embed(string path, Stash content, string mime)
        => Bindings.saucer_webview_embed(Handle, path, content.Handle, mime);

    /// <summary>Remove an embedded resource at the given path.</summary>
    public void Unembed(string path) => Bindings.saucer_webview_unembed(Handle, path);

    /// <summary>Remove all embedded resources.</summary>
    public void UnembedAll() => Bindings.saucer_webview_unembed_all(Handle);

    // ── Script Injection ────────────────────────

    /// <summary>Execute a JavaScript snippet immediately.</summary>
    public void Execute(string script) => Bindings.saucer_webview_execute(Handle, script);

    /// <summary>Inject a script to run at the given time. Returns a script ID.</summary>
    public nuint Inject(string code, SaucerScriptTime runAt = SaucerScriptTime.Ready, bool noFrames = false, bool clearable = true)
        => Bindings.saucer_webview_inject(Handle, code, runAt, noFrames, clearable);

    /// <summary>Remove an injected script by its ID.</summary>
    public void Uninject(nuint id) => Bindings.saucer_webview_uninject(Handle, id);

    /// <summary>Remove all injected scripts.</summary>
    public void UninjectAll() => Bindings.saucer_webview_uninject_all(Handle);

    // ── Custom Scheme Handling ──────────────────

    /// <summary>Register a custom scheme globally (must be called before any webview uses it).</summary>
    public static void RegisterScheme(string scheme)
        => Bindings.saucer_webview_register_scheme(scheme);

    /// <summary>Register a handler for a custom scheme on this webview.</summary>
    public void HandleScheme(string scheme, SaucerSchemeHandler handler, nint userdata = 0)
    {
        _pinnedDelegates.Add(handler);
        Bindings.saucer_webview_handle_scheme(Handle, scheme, handler, userdata);
    }

    /// <summary>Remove the handler for a custom scheme.</summary>
    public void RemoveScheme(string scheme)
        => Bindings.saucer_webview_remove_scheme(Handle, scheme);

    // ── Events ──────────────────────────────────

    /// <summary>Register a persistent event handler. Returns a subscription ID.</summary>
    public nuint On(SaucerWebviewEvent @event, Delegate callback, bool clearable = true)
    {
        _pinnedDelegates.Add(callback);
        var ptr = Marshal.GetFunctionPointerForDelegate(callback);
        return Bindings.saucer_webview_on(Handle, @event, ptr, clearable, 0);
    }

    /// <summary>Register a one-shot event handler.</summary>
    public void Once(SaucerWebviewEvent @event, Delegate callback)
    {
        _pinnedDelegates.Add(callback);
        var ptr = Marshal.GetFunctionPointerForDelegate(callback);
        Bindings.saucer_webview_once(Handle, @event, ptr, 0);
    }

    /// <summary>Remove a specific event handler by subscription ID.</summary>
    public void Off(SaucerWebviewEvent @event, nuint id)
        => Bindings.saucer_webview_off(Handle, @event, id);

    /// <summary>Remove all event handlers for the given event.</summary>
    public void OffAll(SaucerWebviewEvent @event)
        => Bindings.saucer_webview_off_all(Handle, @event);

    // ── IDisposable ─────────────────────────────

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        if (_handle != 0)
        {
            Bindings.saucer_webview_free(_handle);
            _handle = 0;
        }
        _pinnedDelegates.Clear();
    }

    ~Webview() => Dispose();
}
