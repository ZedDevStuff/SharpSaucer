using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerSchemeHandlerRaw(saucer_scheme_request* arg0, saucer_scheme_executor* arg1, IntPtr arg2);

public enum SaucerSchemeError
{
    NotFound = 404,
    Invalid = 400,
    Denied = 401,
    Failed = -1,
}

internal struct saucer_scheme_executor { }

public unsafe partial class SaucerSchemeExecutor
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_executor_free(saucer_scheme_executor* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_scheme_executor* saucer_scheme_executor_copy(saucer_scheme_executor* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_executor_reject(saucer_scheme_executor* arg0, SaucerSchemeError arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_executor_accept(saucer_scheme_executor* arg0, saucer_scheme_response* arg1);

}
