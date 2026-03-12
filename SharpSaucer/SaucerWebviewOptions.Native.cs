using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;



internal struct saucer_webview_options { }

public unsafe partial class SaucerWebviewOptions
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_options_free(saucer_webview_options* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_webview_options* saucer_webview_options_new(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_options_set_attributes(saucer_webview_options* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_options_set_persistent_cookies(saucer_webview_options* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_options_set_hardware_acceleration(saucer_webview_options* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_options_set_storage_path(saucer_webview_options* arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_options_set_user_agent(saucer_webview_options* arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_webview_options_append_browser_flag(saucer_webview_options* arg0, string arg1);

}
