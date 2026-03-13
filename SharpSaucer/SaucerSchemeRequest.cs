using System;
using System.Collections.Generic;
using System.Text;

namespace SharpSaucer;

public partial class SaucerSchemeRequest : IDisposable
{
    internal unsafe saucer_scheme_request* Handle;
    private bool _disposedValue;
    private static readonly Dictionary<nint, SaucerSchemeRequest> Cache = [];

    public SaucerUrl Url
    {
        get
        {
            unsafe
            {
                return SaucerUrl.FromHandle(saucer_scheme_request_url(Handle));
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
                fixed(sbyte* buffer = new sbyte[size])
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
                fixed(sbyte* buffer = new sbyte[size])
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
                return SaucerStash.FromHandle(saucer_scheme_request_content(Handle));
            }
        }
    }

    private unsafe SaucerSchemeRequest(saucer_scheme_request* handle)
    {
        Handle = handle;
        Cache[(nint)handle] = this;
    }
    internal static unsafe SaucerSchemeRequest FromHandle(saucer_scheme_request* handle)
    {
        if(handle == null || (nint)handle == nint.Zero)
            throw new ArgumentNullException(nameof(handle));
        return Cache.TryGetValue((nint)handle, out var cached) 
            ? cached 
            : new SaucerSchemeRequest(handle);
    }

    public SaucerSchemeRequest Copy()
    {
        unsafe
        {
            return new SaucerSchemeRequest(saucer_scheme_request_copy(Handle));
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }
            unsafe
            {
                Cache.Remove((nint)Handle);
                saucer_scheme_request_free(Handle);
            }
            _disposedValue = true;
        }
    }

    ~SaucerSchemeRequest()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
