using System;

namespace SharpSaucer;

public partial class SaucerUrl : IDisposable
{
    internal unsafe saucer_url* Handle;
    private bool _disposedValue;

    internal unsafe SaucerUrl(saucer_url* handle)
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
                saucer_url_free(Handle);
            }
            _disposedValue = true;
        }
    }

    ~SaucerUrl()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
