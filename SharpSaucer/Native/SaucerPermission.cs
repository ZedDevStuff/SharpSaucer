using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer.Native;


/// <summary>
///  @remark Permission-Requests are reference counted. Please make sure to free all copies to properly release it!
/// </summary>
public struct SaucerPermissionRequest { }

internal static unsafe partial class NativeMethods
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_permission_request_free(SaucerPermissionRequest* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerPermissionRequest* saucer_permission_request_copy(SaucerPermissionRequest* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerUrl* saucer_permission_request_url(SaucerPermissionRequest* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerPermissionType saucer_permission_request_type(SaucerPermissionRequest* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_permission_request_accept(SaucerPermissionRequest* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    /// <summary>
    ///  @note Please refer to the documentation in `application.h` on how to use this function.
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_permission_request_native(SaucerPermissionRequest* arg0, nuint arg1, IntPtr arg2, out nuint arg3);

}
