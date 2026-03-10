using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer.Native;


public struct SaucerIcon { }

internal static unsafe partial class NativeMethods
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_icon_empty(SaucerIcon* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerStash* saucer_icon_data(SaucerIcon* arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_icon_save(SaucerIcon* arg0, string arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_icon_free(SaucerIcon* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerIcon* saucer_icon_copy(SaucerIcon* arg0);

    /// <summary>
    ///  @note The pointer passed to @param {error} can be null
    /// </summary>
    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerIcon* saucer_icon_new_from_file(string arg0, out int error);

    /// <summary>
    ///  @note The pointer passed to @param {error} can be null
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerIcon* saucer_icon_new_from_stash(SaucerStash* arg0, out int error);

    /// <summary>
    ///  @note Please refer to the documentation in `application.h` on how to use this function.
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_icon_native(SaucerIcon* arg0, nuint arg1, IntPtr arg2, out nuint arg3);

}
