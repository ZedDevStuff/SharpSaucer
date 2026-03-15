using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerSchemeHandlerNative(saucer_scheme_request* arg0, saucer_scheme_executor* arg1, IntPtr arg2);

public enum SaucerSchemeError
{
   NotFound = 404,
   Invalid = 400,
   Denied = 401,
   Failed = -1,
}

internal struct saucer_scheme_executor { }


internal sealed class SaucerSchemeExecutorHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    public SaucerSchemeExecutorHandle(IntPtr handle) : base(handle, true)
    {
    }

    public SaucerSchemeExecutorHandle() : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        if (!IsInvalid)
        {
            SaucerSchemeExecutor.saucer_scheme_executor_free(this);
        }
        return true;
    }
}

public unsafe partial class SaucerSchemeExecutor
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_executor_free(SaucerSchemeExecutorHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerSchemeExecutorHandle saucer_scheme_executor_copy(SaucerSchemeExecutorHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_executor_reject(SaucerSchemeExecutorHandle arg0, SaucerSchemeError arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_executor_accept(SaucerSchemeExecutorHandle arg0, SaucerSchemeResponseHandle arg1);

}
