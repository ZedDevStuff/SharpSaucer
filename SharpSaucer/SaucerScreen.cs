using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SharpSaucer;


public partial class SaucerScreen : IDisposable
{
    internal SaucerScreenHandle Handle;

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

    internal SaucerScreen(SaucerScreenHandle handle)
    {
        Handle = handle;
    }

    public void Dispose()
    {
        Handle.Dispose();
    }
}
