using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer.Native;


public struct SaucerUrl { }

internal static unsafe partial class NativeMethods
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_free(SaucerUrl* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerUrl* saucer_url_copy(SaucerUrl* arg0);

    /// <summary>
    ///  @note The pointer passed to @param {error} can be null
    /// </summary>
    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerUrl* saucer_url_new_parse(string arg0, out int error);

    /// <summary>
    ///  @note The pointer passed to @param {error} can be null
    /// </summary>
    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerUrl* saucer_url_new_from(string arg0, out int error);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerUrl* saucer_url_new_opts(string scheme, string host, out nuint port, string path);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_string(SaucerUrl* arg0, IntPtr arg1, out nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_path(SaucerUrl* arg0, IntPtr arg1, out nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_scheme(SaucerUrl* arg0, IntPtr arg1, out nuint arg2);

    /// <summary>
    ///  @note The url might not contain a host. If this is the case, @param {size} will be set to 0.
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_host(SaucerUrl* arg0, IntPtr arg1, out nuint arg2);

    /// <summary>
    ///  @note The url might not contain a port. If this is the case, @param {port} will be left unchanged and `false` will be returned.
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_url_port(SaucerUrl* arg0, out nuint arg1);

    /// <summary>
    ///  @note The url might not contain a user. If this is the case, @param {size} will be set to 0.
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_user(SaucerUrl* arg0, IntPtr arg1, out nuint arg2);

    /// <summary>
    ///  @note The url might not contain a password. If this is the case, @param {size} will be set to 0.
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_password(SaucerUrl* arg0, IntPtr arg1, out nuint arg2);

    /// <summary>
    ///  @note Please refer to the documentation in `application.h` on how to use this function.
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_native(SaucerUrl* arg0, nuint arg1, IntPtr arg2, out nuint arg3);

}
