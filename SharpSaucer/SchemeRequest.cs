using System.Text;

using SharpSaucer.Native;

namespace SharpSaucer;

public class SchemeRequest : StructWrapper
{
    public Url Url
    {
        get
        {
            unsafe
            {
                return new Url(NativeMethods.saucer_scheme_request_url((SaucerSchemeRequest*)Handle));
            }
        }
    }
    public string Method
    {
        get
        {
            unsafe
            {
                NativeMethods.saucer_scheme_request_method((SaucerSchemeRequest*)Handle, 0, out nuint length);
                if (length == 0)
                    return string.Empty;
                var buf = stackalloc byte[(int)length];
                NativeMethods.saucer_scheme_request_method((SaucerSchemeRequest*)Handle, (nint)buf, out _);
                return Encoding.UTF8.GetString(buf, (int)length);
            }
        }
    }
    public Stash Content
    {
        get
        {
            unsafe
            {
                return new Stash(NativeMethods.saucer_scheme_request_content((SaucerSchemeRequest*)Handle));
            }
        }
    }
    public string Headers
    {
        get
        {
            unsafe
            {
                NativeMethods.saucer_scheme_request_headers((SaucerSchemeRequest*)Handle, 0, out nuint length);
                if (length == 0)
                    return string.Empty;
                var buf = stackalloc byte[(int)length];
                NativeMethods.saucer_scheme_request_headers((SaucerSchemeRequest*)Handle, (nint)buf, out _);
                return Encoding.UTF8.GetString(buf, (int)length);
            }
        }
    }

    internal SchemeRequest(nint handle) : base(handle)
    {
    }
    internal unsafe SchemeRequest(SaucerSchemeRequest* handle) : base((nint)handle)
    {
    }


    public override void Free()
    {
        unsafe
        {
            NativeMethods.saucer_scheme_request_free((SaucerSchemeRequest*)Handle);
        }
    }
}
