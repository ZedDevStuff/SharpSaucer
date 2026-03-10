using System;

using SharpSaucer.Native;

namespace SharpSaucer;

public class Icon : StructWrapper
{
    public bool Empty
    {
        get
        {
            unsafe
            {
                return NativeMethods.saucer_icon_empty((SaucerIcon*)Handle);
            }
        }
    }
    public IntPtr Data
    {
        get
        {
            unsafe
            {
                return (IntPtr)NativeMethods.saucer_icon_data((SaucerIcon*)Handle);
            }
        }
    }
    internal Icon(nint handle) : base(handle)
    {
    }
    internal unsafe Icon(SaucerIcon* icon) : base((nint)icon)
    {
    }

    public Icon Copy()
    {
        unsafe
        {
            var copy = NativeMethods.saucer_icon_copy((SaucerIcon*)Handle);
            return new Icon((nint)copy);
        }
    }
    public void Save(string path)
    {
        unsafe
        {
            NativeMethods.saucer_icon_save((SaucerIcon*)Handle, path);
        }
    }

    public static Icon FromFile(string path)
    {
        unsafe
        {
            var icon = NativeMethods.saucer_icon_new_from_file(path, out int error);
            if (error != 0)
                throw new Exception($"Failed to load icon from file. Error code: {error}");
            return new Icon((nint)icon);
        }
    }
    public static Icon FromStash(object stash)
    {
        throw new NotImplementedException();
    }
    public override unsafe void Free()
    {
        NativeMethods.saucer_icon_free((SaucerIcon*)Handle);
    }
}
