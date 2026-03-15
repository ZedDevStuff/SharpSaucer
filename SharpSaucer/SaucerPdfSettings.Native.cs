using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;



internal struct saucer_pdf_settings { }


internal sealed class SaucerPdfSettingsHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    public SaucerPdfSettingsHandle(IntPtr handle) : base(handle, true)
    {
    }

    public SaucerPdfSettingsHandle() : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        if (!IsInvalid)
        {
            SaucerPdfSettings.saucer_pdf_settings_free(this);
        }
        return true;
    }
}

public unsafe partial class SaucerPdfSettings
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_pdf_settings_free(SaucerPdfSettingsHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_pdf_settings_set_size(SaucerPdfSettingsHandle arg0, double w, double h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_pdf_settings_set_orientation(SaucerPdfSettingsHandle arg0, SaucerPdfLayout arg1);

}
