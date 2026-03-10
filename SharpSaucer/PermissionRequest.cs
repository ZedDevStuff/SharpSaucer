using SharpSaucer.Native;

namespace SharpSaucer;

public unsafe class PermissionRequest : StructWrapper
{
    public Url Url
    {
        get
        {
            unsafe
            {
                return new Url(NativeMethods.saucer_permission_request_url((SaucerPermissionRequest*)Handle));
            }
        }
    }
    public SaucerPermissionType PermissionType => NativeMethods.saucer_permission_request_type((SaucerPermissionRequest*)Handle);
    internal PermissionRequest(nint handle) : base(handle)
    {
    }
    internal PermissionRequest(SaucerPermissionRequest* handle) : base((nint)handle)
    {
    }

    public void Accept()
    {
        unsafe
        {
            NativeMethods.saucer_permission_request_accept((SaucerPermissionRequest*)Handle, true);
        }
    }
    public void Deny()
    {
        unsafe
        {
            NativeMethods.saucer_permission_request_accept((SaucerPermissionRequest*)Handle, false);
        }
    }

    public PermissionRequest Copy()
    {
        unsafe
        {
            return new PermissionRequest(NativeMethods.saucer_permission_request_copy((SaucerPermissionRequest*)Handle));
        }
    }

    public override void Free()
    {
        NativeMethods.saucer_permission_request_free((SaucerPermissionRequest*)Handle);
    }
}
