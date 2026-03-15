using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;



internal struct saucer_scheme_request { }


internal sealed class SaucerSchemeRequestHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    public SaucerSchemeRequestHandle(IntPtr handle) : base(handle, true)
    {
    }

    public SaucerSchemeRequestHandle() : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        if (!IsInvalid)
        {
            SaucerSchemeRequest.saucer_scheme_request_free(this);
        }
        return true;
    }
}

public unsafe partial class SaucerSchemeRequest
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_request_free(SaucerSchemeRequestHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerSchemeRequestHandle saucer_scheme_request_copy(SaucerSchemeRequestHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerUrlHandle saucer_scheme_request_url(SaucerSchemeRequestHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_request_method(SaucerSchemeRequestHandle arg0, sbyte* arg1, ref nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerStashHandle saucer_scheme_request_content(SaucerSchemeRequestHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_request_headers(SaucerSchemeRequestHandle arg0, sbyte* arg1, ref nuint arg2);

}
