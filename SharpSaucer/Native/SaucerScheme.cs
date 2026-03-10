using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerSchemeHandler(SaucerSchemeRequest* arg0, SaucerSchemeExecutor* arg1, IntPtr arg2);

public struct SaucerSchemeExecutor { }

public struct SaucerSchemeRequest { }

public struct SaucerSchemeResponse { }

internal static unsafe partial class NativeMethods
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_response_free(SaucerSchemeResponse* arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerSchemeResponse* saucer_scheme_response_new(SaucerStash* arg0, string mime);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_response_append_header(SaucerSchemeResponse* arg0, string arg1, string arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_response_set_status(SaucerSchemeResponse* arg0, int arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_request_free(SaucerSchemeRequest* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerSchemeRequest* saucer_scheme_request_copy(SaucerSchemeRequest* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerUrl* saucer_scheme_request_url(SaucerSchemeRequest* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_request_method(SaucerSchemeRequest* arg0, IntPtr arg1, out nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerStash* saucer_scheme_request_content(SaucerSchemeRequest* arg0);

    /// <summary>
    ///  @remark Headers are returned null delimited, e.g. as "Header: Value\0Another Header: Value"
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_request_headers(SaucerSchemeRequest* arg0, IntPtr arg1, out nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_executor_free(SaucerSchemeExecutor* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerSchemeExecutor* saucer_scheme_executor_copy(SaucerSchemeExecutor* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_executor_reject(SaucerSchemeExecutor* arg0, SaucerSchemeError arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_executor_accept(SaucerSchemeExecutor* arg0, SaucerSchemeResponse* arg1);

}
