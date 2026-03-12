using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;



internal struct saucer_desktop { }

public unsafe partial class SaucerDesktop
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_desktop_free(saucer_desktop* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_desktop_mouse_position(saucer_desktop* arg0, out int x, out int y);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_pick_file(saucer_desktop* arg0, saucer_picker_options* arg1, sbyte* arg2, ref nuint arg3, out int error);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_pick_folder(saucer_desktop* arg0, saucer_picker_options* arg1, sbyte* arg2, ref nuint arg3, out int error);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_pick_files(saucer_desktop* arg0, saucer_picker_options* arg1, sbyte* arg2, ref nuint arg3, out int error);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_save(saucer_desktop* arg0, saucer_picker_options* arg1, sbyte* arg2, ref nuint arg3, out int error);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_desktop_open(saucer_desktop* arg0, string arg1);

}
