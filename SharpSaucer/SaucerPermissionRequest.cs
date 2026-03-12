using System;

namespace SharpSaucer;

public partial class SaucerPermissionRequest : IDisposable
{
    internal unsafe saucer_permission_request* Handle;
    private bool _disposedValue;

    internal unsafe SaucerPermissionRequest(saucer_permission_request* handle)
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
                saucer_permission_request_free(Handle);
            }
            _disposedValue = true;
        }
    }

    ~SaucerPermissionRequest()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
