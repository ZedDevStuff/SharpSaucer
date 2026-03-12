using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate SaucerStatus SaucerWebviewEventPermission(saucer_webview* arg0, saucer_permission_request* arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate SaucerPolicy SaucerWebviewEventFullscreen(saucer_webview* arg0, bool arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWebviewEventDomReady(saucer_webview* arg0, IntPtr arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWebviewEventNavigated(saucer_webview* arg0, saucer_url* arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate SaucerPolicy SaucerWebviewEventNavigate(saucer_webview* arg0, saucer_navigation* arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate SaucerStatus SaucerWebviewEventMessage(saucer_webview* arg0, string arg1, nuint arg2, IntPtr arg3);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWebviewEventRequest(saucer_webview* arg0, saucer_url* arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWebviewEventFavicon(saucer_webview* arg0, saucer_icon* arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWebviewEventTitle(saucer_webview* arg0, string arg1, nuint arg2, IntPtr arg3);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWebviewEventLoad(saucer_webview* arg0, SaucerState arg1, IntPtr arg2);

public enum SaucerState
{
    Started = 0,
    Finished = 1,
}

public enum SaucerStatus
{
    Handled = 0,
    Unhandled = 1,
}

public enum SaucerScriptTime
{
    Creation = 0,
    Ready = 1,
}

public enum SaucerWebviewEvent
{
    Permission = 0,
    Fullscreen = 1,
    DomReady = 2,
    Navigated = 3,
    Navigate = 4,
    Message = 5,
    Request = 6,
    Favicon = 7,
    Title = 8,
    Load = 9,
}

internal struct saucer_webview { }

public unsafe partial class SaucerWebview
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_free(saucer_webview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_webview* saucer_webview_new(saucer_webview_options* arg0, out int error);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_url* saucer_webview_url(saucer_webview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_icon* saucer_webview_favicon(saucer_webview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_page_title(saucer_webview* arg0, sbyte* arg1, ref nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool saucer_webview_dev_tools(saucer_webview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool saucer_webview_context_menu(saucer_webview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool saucer_webview_force_dark(saucer_webview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_background(saucer_webview* arg0, ref byte r, ref byte g, ref byte b, ref byte a);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_bounds(saucer_webview* arg0, out int x, out int y, out int w, out int h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_set_url(saucer_webview* arg0, saucer_url* arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_set_url_str(saucer_webview* arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_set_html(saucer_webview* arg0, string arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_set_dev_tools(saucer_webview* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_set_context_menu(saucer_webview* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_set_force_dark(saucer_webview* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_set_background(saucer_webview* arg0, byte r, byte g, byte b, byte a);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_reset_bounds(saucer_webview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_set_bounds(saucer_webview* arg0, int x, int y, int w, int h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_back(saucer_webview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_forward(saucer_webview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_reload(saucer_webview* arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_serve(saucer_webview* arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_embed(saucer_webview* arg0, string path, saucer_stash* content, string mime);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_unembed_all(saucer_webview* arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_unembed(saucer_webview* arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_execute(saucer_webview* arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial nuint saucer_webview_inject(saucer_webview* arg0, string code, SaucerScriptTime run_at, [MarshalAs(UnmanagedType.U1)] bool no_frames, [MarshalAs(UnmanagedType.U1)] bool clearable);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_uninject_all(saucer_webview* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_uninject(saucer_webview* arg0, nuint arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_handle_scheme(saucer_webview* arg0, string arg1, SaucerSchemeHandlerRaw arg2, IntPtr userdata);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_remove_scheme(saucer_webview* arg0, string arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial nuint saucer_webview_on(saucer_webview* arg0, SaucerWebviewEvent arg1, IntPtr callback, [MarshalAs(UnmanagedType.U1)] bool clearable, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_once(saucer_webview* arg0, SaucerWebviewEvent arg1, IntPtr callback, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_off(saucer_webview* arg0, SaucerWebviewEvent arg1, nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_off_all(saucer_webview* arg0, SaucerWebviewEvent arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_register_scheme(string arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_native(saucer_webview* arg0, nuint arg1, IntPtr arg2, ref nuint arg3);

}
