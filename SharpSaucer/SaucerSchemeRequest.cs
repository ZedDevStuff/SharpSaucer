using System;
using System.Text;

namespace SharpSaucer;

public partial class SaucerSchemeRequest : IDisposable
{
    internal SaucerSchemeRequestHandle Handle;

    public SaucerUrl Url
    {
        get
        {
            unsafe
            {
                return new SaucerUrl(saucer_scheme_request_url(Handle));
            }
        }
    }
    public string Headers
    {
        get
        {
            unsafe
            {
                nuint size = 0;
                saucer_scheme_request_headers(Handle, null, ref size);
                fixed (sbyte* buffer = new sbyte[size])
                {
                    int length = (int)size;
                    saucer_scheme_request_headers(Handle, buffer, ref size);
                    return new string(buffer, 0, length, Encoding.UTF8);
                }
            }
        }
    }
    public string Method
    {
        get
        {
            unsafe
            {
                nuint size = 0;
                saucer_scheme_request_method(Handle, null, ref size);
                fixed (sbyte* buffer = new sbyte[size])
                {
                    int length = (int)size;
                    saucer_scheme_request_method(Handle, buffer, ref size);
                    return new string(buffer, 0, length, Encoding.UTF8);
                }
            }
        }
    }
    public SaucerStash Content
    {
        get
        {
            unsafe
            {
                return new SaucerStash(saucer_scheme_request_content(Handle));
            }
        }
    }

    internal SaucerSchemeRequest(SaucerSchemeRequestHandle handle)
    {
        Handle = handle;
    }

    public SaucerSchemeRequest Copy()
    {
        unsafe
        {
            return new SaucerSchemeRequest(saucer_scheme_request_copy(Handle));
        }
    }

    public void Dispose()
    {
        Handle.Dispose();
    }
}
