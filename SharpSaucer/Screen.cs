using System;

using SharpSaucer.Native;

namespace SharpSaucer;

public class Screen : StructWrapper
{
    internal unsafe Screen(nint handle) : base(handle)
    {
    }
    internal unsafe Screen(SaucerScreen* handle) : base((nint)handle)
    {
    }
    public string Name
    {
        get
        {
            unsafe
            {
                return new String((char*)NativeMethods.saucer_screen_name((SaucerScreen*)Handle));
            }
        }
    }
    public (int Width, int Height) Size
    {
        get
        {
            unsafe
            {
                NativeMethods.saucer_screen_size((SaucerScreen*)Handle, out int w, out int h);
                return (w, h);
            }
        }
    }
    public (int X, int Y) Position
    {
        get
        {
            unsafe
            {
                NativeMethods.saucer_screen_position((SaucerScreen*)Handle, out int x, out int y);
                return (x, y);
            }
        }
    }
    public override unsafe void Free()
    {
        NativeMethods.saucer_screen_free((SaucerScreen*)Handle);
    }
}
