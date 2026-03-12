using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;



internal struct saucer_picker_options { }

public unsafe partial class SaucerPickerOptions
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_options_free(saucer_picker_options* arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_options_set_initial(saucer_picker_options* arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_options_set_filters(saucer_picker_options* arg0, string arg1, nuint arg2);

}
