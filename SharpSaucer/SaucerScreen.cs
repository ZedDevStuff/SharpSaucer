using System;
using System.Runtime.InteropServices;

namespace SharpSaucer;


public partial class SaucerScreen : IDisposable
{
    internal unsafe saucer_screen* Handle;
    private bool _disposedValue;

    /// <summary>
    /// WARNING: This does not work currently
    /// </summary>
    public string Name
    {
        get
        {
            unsafe
            {
                return Marshal.PtrToStringAnsi(saucer_screen_name(Handle)) ?? "UNKNOWN";
            }
        }
    }
    /// <summary>
    /// WARNING: This does not work currently
    /// </summary>
    public (int Width, int Height) Size
    {
        get
        {
            unsafe
            {
                saucer_screen_size(Handle, out int width, out int height);
                return (width, height);
            }
        }
    }
    public (int X, int Y) Position
    {
        get
        {
            unsafe
            {
                saucer_screen_position(Handle, out int x, out int y);
                return (x, y);
            }
        }
    }

    internal unsafe SaucerScreen(saucer_screen* handle)
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
                saucer_screen_free(Handle);
            }
            _disposedValue = true;
        }
    }

    ~SaucerScreen()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
