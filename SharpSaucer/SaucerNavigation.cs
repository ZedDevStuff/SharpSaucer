using System;

namespace SharpSaucer;

public partial class SaucerNavigation : IDisposable
{
    internal unsafe saucer_navigation* Handle;
    private bool _disposedValue;

    public SaucerUrl Url
    {
        get
        {
            unsafe
            {
                return new SaucerUrl(saucer_navigation_url(Handle));
            }
        }
    }
    public bool NewWindow
    {
        get
        {
            unsafe
            {
                return saucer_navigation_new_window(Handle);
            }
        }
    }
    public bool Redirection
    {
        get
        {
            unsafe
            {
                return saucer_navigation_redirection(Handle);
            }
        }
    }
    public bool UserInitiated
    {
        get
        {
            unsafe
            {
                return saucer_navigation_user_initiated(Handle);
            }
        }
    }

    internal unsafe SaucerNavigation(saucer_navigation* handle)
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
                // Apparently there is no free function for this struct
            }
            _disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~SaucerNavigation()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
