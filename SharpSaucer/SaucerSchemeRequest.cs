using System;

namespace SharpSaucer;

public partial class SaucerSchemeRequest : IDisposable
{
    internal unsafe saucer_scheme_request* Handle;
    private bool _disposedValue;

    internal unsafe SaucerSchemeRequest(saucer_scheme_request* handle)
    {
        Handle = handle;
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
