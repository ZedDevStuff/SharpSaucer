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


internal sealed class SaucerPermissionRequestHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    public SaucerPermissionRequestHandle(IntPtr handle) : base(handle, true)
    {
    }

    public SaucerPermissionRequestHandle() : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        if (!IsInvalid)
        {
            SaucerPermissionRequest.saucer_permission_request_free(this);
        }
        return true;
    }
}

public unsafe partial class SaucerPermissionRequest
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_permission_request_free(SaucerPermissionRequestHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerPermissionRequestHandle saucer_permission_request_copy(SaucerPermissionRequestHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerUrlHandle saucer_permission_request_url(SaucerPermissionRequestHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerPermissionType saucer_permission_request_type(SaucerPermissionRequestHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_permission_request_accept(SaucerPermissionRequestHandle arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_permission_request_native(SaucerPermissionRequestHandle arg0, nuint arg1, IntPtr arg2, ref nuint arg3);

}
