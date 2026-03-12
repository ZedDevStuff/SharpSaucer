using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;


public enum SaucerPermissionType
{
    Unknown = 0,
    AudioMedia = 1,
    VideoMedia = 2,
    DesktopMedia = 4,
    MouseLock = 8,
    DeviceInfo = 16,
    Location = 32,
    Clipboard = 64,
    Notification = 128,
}

internal struct saucer_permission_request { }

public unsafe partial class SaucerPermissionRequest
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_permission_request_free(saucer_permission_request* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_permission_request* saucer_permission_request_copy(saucer_permission_request* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_url* saucer_permission_request_url(saucer_permission_request* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerPermissionType saucer_permission_request_type(saucer_permission_request* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_permission_request_accept(saucer_permission_request* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_permission_request_native(saucer_permission_request* arg0, nuint arg1, IntPtr arg2, ref nuint arg3);

}
