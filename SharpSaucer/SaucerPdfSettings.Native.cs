using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;



internal struct saucer_pdf_settings { }

public unsafe partial class SaucerPdfSettings
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_pdf_settings_free(saucer_pdf_settings* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_pdf_settings_set_size(saucer_pdf_settings* arg0, double w, double h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_pdf_settings_set_orientation(saucer_pdf_settings* arg0, SaucerPdfLayout arg1);

}
