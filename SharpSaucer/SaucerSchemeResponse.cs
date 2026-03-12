using System;

namespace SharpSaucer;

public partial class SaucerSchemeResponse : IDisposable
{
    internal unsafe saucer_scheme_response* Handle;
    private bool _disposedValue;

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

    internal unsafe SaucerSchemeResponse(saucer_scheme_response* handle)
    {
        Handle = handle;
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
