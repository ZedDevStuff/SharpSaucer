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
    }

    internal static unsafe SaucerIcon FromHandle(nint handle)
    {
        if (handle == 0)
            return null;
        if (Cache.TryGetValue(handle, out var icon))
            return icon;
        icon = new SaucerIcon((saucer_icon*)handle);
        Cache[handle] = icon;
        return icon;
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
