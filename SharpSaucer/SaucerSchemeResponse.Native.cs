using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;



internal struct saucer_scheme_response { }


internal sealed class SaucerSchemeResponseHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    public SaucerSchemeResponseHandle(IntPtr handle) : base(handle, true)
    {
    }

    public SaucerSchemeResponseHandle() : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        if (!IsInvalid)
        {
            SaucerSchemeResponse.saucer_scheme_response_free(this);
        }
        return true;
    }
}

public unsafe partial class SaucerSchemeResponse
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_response_free(SaucerSchemeResponseHandle arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerSchemeResponseHandle saucer_scheme_response_new(SaucerStashHandle arg0, string mime);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_response_append_header(SaucerSchemeResponseHandle arg0, string arg1, string arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_response_set_status(SaucerSchemeResponseHandle arg0, int arg1);

}
