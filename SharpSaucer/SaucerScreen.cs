using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SharpSaucer;


public partial class SaucerScreen : IDisposable
{
    internal unsafe saucer_screen* Handle;
    private bool _disposedValue;
    private static readonly Dictionary<nint, SaucerScreen> Cache = [];

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

    private unsafe SaucerScreen(saucer_screen* handle)
    {
        Handle = handle;
        Cache[(nint)handle] = this;
    }
    internal static unsafe SaucerScreen FromHandle(saucer_screen* handle)
    {
        if(handle == null || (nint)handle == nint.Zero)
            throw new ArgumentNullException(nameof(handle));
        return Cache.TryGetValue((nint)handle, out var cached) 
            ? cached 
            : new SaucerScreen(handle);
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
