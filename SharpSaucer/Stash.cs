using System;

using SharpSaucer.Native;

namespace SharpSaucer;

public class Stash : StructWrapper
{
    public IntPtr Data
    {
        get
        {
            unsafe
            {
                return (IntPtr)NativeMethods.saucer_stash_data((SaucerStash*)Handle);
            }
        }
    }
    public nuint Length
    {
        get
        {
            unsafe
            {
                return NativeMethods.saucer_stash_size((SaucerStash*)Handle);
            }
        }
    }
    internal Stash(nint handle) : base(handle)
    {
    }
    internal unsafe Stash(SaucerStash* handle) : base((nint)handle)
    {
    }
    public Stash(byte[] data)
    {
        unsafe
        {
            Handle = (nint)NativeMethods.saucer_stash_new_from(out data[0], (nuint)data.Length);
        }
    }
    public Stash(string data)
    {
        unsafe
        {
            Handle = (nint)NativeMethods.saucer_stash_new_from_str(data);
        }
    }

    public Stash Copy()
    {
        unsafe
        {
            return new Stash(NativeMethods.saucer_stash_copy((SaucerStash*)Handle));
        }
    }

    public override void Free()
    {
        unsafe
        {
            NativeMethods.saucer_stash_free((SaucerStash*)Handle);
        }
    }

    public static unsafe Stash Empty => new Stash(NativeMethods.saucer_stash_new_empty());
}
