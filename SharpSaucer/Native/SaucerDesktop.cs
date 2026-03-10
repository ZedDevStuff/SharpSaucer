using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer.Native;


public struct SaucerDesktop { }

public struct SaucerPickerOptions { }

internal static unsafe partial class NativeMethods
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_picker_options_free(SaucerPickerOptions* arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_picker_options_set_initial(SaucerPickerOptions* arg0, string arg1);

    /// <summary>
    ///  @remark Expects the filters in the format of: "filter1\0filter2\0filter3\0"
    /// </summary>
    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_picker_options_set_filters(SaucerPickerOptions* arg0, string arg1, nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_desktop_free(SaucerDesktop* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_desktop_mouse_position(SaucerDesktop* arg0, out int x, out int y);

    /// <summary>
    ///  @note The pointer passed to @param {error} can be null
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_picker_pick_file(SaucerDesktop* arg0, SaucerPickerOptions* arg1, IntPtr arg2, out nuint arg3, out int error);

    /// <summary>
    ///  @note The pointer passed to @param {error} can be null
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_picker_pick_folder(SaucerDesktop* arg0, SaucerPickerOptions* arg1, IntPtr arg2, out nuint arg3, out int error);

    /// <summary>
    ///  @note The pointer passed to @param {error} can be null
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_picker_pick_files(SaucerDesktop* arg0, SaucerPickerOptions* arg1, IntPtr arg2, out nuint arg3, out int error);

    /// <summary>
    ///  @note The pointer passed to @param {error} can be null
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_picker_save(SaucerDesktop* arg0, SaucerPickerOptions* arg1, IntPtr arg2, out nuint arg3, out int error);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_desktop_open(SaucerDesktop* arg0, string arg1);

}
