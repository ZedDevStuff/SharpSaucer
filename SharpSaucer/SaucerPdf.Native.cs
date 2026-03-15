using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;


public enum SaucerPdfLayout
{
   Portrait = 0,
   Landscape = 1,
}

internal struct saucer_pdf { }


internal sealed class SaucerPdfHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    public SaucerPdfHandle(IntPtr handle) : base(handle, true)
    {
    }

    public SaucerPdfHandle() : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        if (!IsInvalid)
        {
            SaucerPdf.saucer_pdf_free(this);
        }
        return true;
    }
}

public unsafe partial class SaucerPdf
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_pdf_free(SaucerPdfHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_pdf_save(SaucerPdfHandle arg0, SaucerPdfSettingsHandle arg1);

}
