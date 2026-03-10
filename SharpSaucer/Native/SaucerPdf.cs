using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer.Native;


public struct SaucerPdf { }

public struct SaucerPdfSettings { }

internal static unsafe partial class NativeMethods
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_pdf_settings_free(SaucerPdfSettings* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_pdf_settings_set_size(SaucerPdfSettings* arg0, double w, double h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_pdf_settings_set_orientation(SaucerPdfSettings* arg0, SaucerPdfLayout arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_pdf_free(SaucerPdf* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_pdf_save(SaucerPdf* arg0, SaucerPdfSettings* arg1);

}
