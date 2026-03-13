using System;
using System.Collections.Generic;

namespace SharpSaucer;

public partial class SaucerSchemeResponse : IDisposable
{
    internal unsafe saucer_scheme_response* Handle;
    private bool _disposedValue;
    private static readonly Dictionary<nint, SaucerSchemeResponse> Cache = [];

    public int Status
    {
        set
        {
            unsafe
            {
                saucer_scheme_response_set_status(Handle, value);
            }
        }
    }

    private unsafe SaucerSchemeResponse(saucer_scheme_response* handle)
    {
        Handle = handle;
        Cache[(nint)handle] = this;
    }
    internal static unsafe SaucerSchemeResponse FromHandle(saucer_scheme_response* handle)
    {
        if(handle == null || (nint)handle == nint.Zero)
            throw new ArgumentNullException(nameof(handle));
        return Cache.TryGetValue((nint)handle, out var cached) 
            ? cached 
            : new SaucerSchemeResponse(handle);
    }
    public SaucerSchemeResponse(SaucerStash stash, string mime)
    {
        unsafe
        {
            Handle = saucer_scheme_response_new(stash.Handle, mime);
        }
    }

    public void AppendHeader(string name, string value)
    {
        unsafe
        {
            saucer_scheme_response_append_header(Handle, name, value);
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
                saucer_scheme_response_free(Handle);
            }
            _disposedValue = true;
        }
    }

    ~SaucerSchemeResponse()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
