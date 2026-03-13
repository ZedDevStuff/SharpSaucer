using System;
using System.Collections.Generic;

namespace SharpSaucer;


public partial class SaucerIcon : IDisposable
{
    internal unsafe saucer_icon* Handle;
    private bool _disposedValue;
    private static readonly Dictionary<nint, SaucerIcon> Cache = [];

    private unsafe SaucerIcon(saucer_icon* handle)
    {
        Handle = handle;
        Cache[(nint)handle] = this;
    }

    internal static unsafe SaucerIcon FromHandle(saucer_icon* handle)
    {
        if (handle == null || (nint)handle == nint.Zero)
            throw new ArgumentNullException(nameof(handle));
        return Cache.TryGetValue((nint)handle, out var cached) 
            ? cached 
            : new SaucerIcon(handle);
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
                saucer_icon_free(Handle);
            }
            _disposedValue = true;
        }
    }

    ~SaucerIcon()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
