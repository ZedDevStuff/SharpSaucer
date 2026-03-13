using System;
using System.Collections.Generic;

namespace SharpSaucer;

public partial class SaucerPermissionRequest : IDisposable
{
    internal unsafe saucer_permission_request* Handle;
    private bool _disposedValue;
    private static readonly Dictionary<nint, SaucerPermissionRequest> Cache = [];

    private unsafe SaucerPermissionRequest(saucer_permission_request* handle)
    {
        Handle = handle;
        Cache[(nint)handle] = this;
    }
    internal static unsafe SaucerPermissionRequest FromHandle(saucer_permission_request* handle)
    {
        if (handle == null || (nint)handle == nint.Zero) 
            throw new ArgumentNullException(nameof(handle));
        return Cache.TryGetValue((nint)handle, out var cached) 
            ? cached 
            : new SaucerPermissionRequest(handle);
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
