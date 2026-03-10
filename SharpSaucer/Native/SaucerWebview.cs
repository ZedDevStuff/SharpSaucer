using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer.Native;

/// <summary>
///  @remark The passed permission requests lifetime ends when the event-callback finishes. To keep it around, it has to be explictly copied!
/// </summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate SaucerStatus SaucerWebviewEventPermission(SaucerWebview* arg0, SaucerPermissionRequest* arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate SaucerPolicy SaucerWebviewEventFullscreen(SaucerWebview* arg0, bool arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWebviewEventDomReady(SaucerWebview* arg0, IntPtr arg1);

/// <summary>
///  @remark The passed urls lifetime ends when the event-callback finishes. To keep it around, it has to be explictly copied!
/// </summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWebviewEventNavigated(SaucerWebview* arg0, SaucerUrl* arg1, IntPtr arg2);

/// <summary>
///  @remark The passed navigations lifetime ends when the event-callback finishes. It cannot be copied.
/// </summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate SaucerPolicy SaucerWebviewEventNavigate(SaucerWebview* arg0, SaucerNavigation* arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate SaucerStatus SaucerWebviewEventMessage(SaucerWebview* arg0, string arg1, nuint arg2, IntPtr arg3);

/// <summary>
///  @remark The passed urls lifetime ends when the event-callback finishes. To keep it around, it has to be explictly copied!
/// </summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWebviewEventRequest(SaucerWebview* arg0, SaucerUrl* arg1, IntPtr arg2);

/// <summary>
///  @remark The passed icons lifetime ends when the event-callback finishes. To keep it around, it has to be explictly copied!
/// </summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWebviewEventFavicon(SaucerWebview* arg0, SaucerIcon* arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWebviewEventTitle(SaucerWebview* arg0, string arg1, nuint arg2, IntPtr arg3);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWebviewEventLoad(SaucerWebview* arg0, SaucerState arg1, IntPtr arg2);

public struct SaucerWebview { }

public struct SaucerWebviewOptions { }

internal static unsafe partial class NativeMethods
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_free(SaucerWebviewOptions* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerWebviewOptions* saucer_webview_options_new(SaucerWindow* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_set_attributes(SaucerWebviewOptions* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_set_persistent_cookies(SaucerWebviewOptions* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_set_hardware_acceleration(SaucerWebviewOptions* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_set_storage_path(SaucerWebviewOptions* arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_set_user_agent(SaucerWebviewOptions* arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_append_browser_flag(SaucerWebviewOptions* arg0, string arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_free(SaucerWebview* arg0);

    /// <summary>
    ///  @note The pointer passed to @param {error} can be null
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerWebview* saucer_webview_new(SaucerWebviewOptions* arg0, out int error);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerUrl* saucer_webview_url(SaucerWebview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerIcon* saucer_webview_favicon(SaucerWebview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_page_title(SaucerWebview* arg0, IntPtr arg1, out nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_webview_dev_tools(SaucerWebview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_webview_context_menu(SaucerWebview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_webview_force_dark(SaucerWebview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_background(SaucerWebview* arg0, out byte r, out byte g, out byte b, out byte a);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_bounds(SaucerWebview* arg0, out int x, out int y, out int w, out int h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_url(SaucerWebview* arg0, SaucerUrl* arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_url_str(SaucerWebview* arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_html(SaucerWebview* arg0, string arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_dev_tools(SaucerWebview* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_context_menu(SaucerWebview* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_force_dark(SaucerWebview* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_background(SaucerWebview* arg0, byte r, byte g, byte b, byte a);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_reset_bounds(SaucerWebview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_bounds(SaucerWebview* arg0, int x, int y, int w, int h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_back(SaucerWebview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_forward(SaucerWebview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_reload(SaucerWebview* arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_serve(SaucerWebview* arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_embed(SaucerWebview* arg0, string path, SaucerStash* content, string mime);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_unembed_all(SaucerWebview* arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_unembed(SaucerWebview* arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_execute(SaucerWebview* arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint saucer_webview_inject(SaucerWebview* arg0, string code, SaucerScriptTime run_at, [MarshalAs(UnmanagedType.U1)] bool no_frames, [MarshalAs(UnmanagedType.U1)] bool clearable);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_uninject_all(SaucerWebview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_uninject(SaucerWebview* arg0, nuint arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_handle_scheme(SaucerWebview* arg0, string arg1, SaucerSchemeHandler arg2, IntPtr userdata);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_remove_scheme(SaucerWebview* arg0, string arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint saucer_webview_on(SaucerWebview* arg0, SaucerWebviewEvent arg1, IntPtr callback, [MarshalAs(UnmanagedType.U1)] bool clearable, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_once(SaucerWebview* arg0, SaucerWebviewEvent arg1, IntPtr callback, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_off(SaucerWebview* arg0, SaucerWebviewEvent arg1, nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_off_all(SaucerWebview* arg0, SaucerWebviewEvent arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_register_scheme(string arg0);

    /// <summary>
    ///  @note Please refer to the documentation in `application.h` on how to use this function.
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_native(SaucerWebview* arg0, nuint arg1, IntPtr arg2, out nuint arg3);

}
