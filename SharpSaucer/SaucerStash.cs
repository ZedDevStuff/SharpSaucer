using System;

namespace SharpSaucer;

public partial class SaucerStash : IDisposable
{
    internal SaucerStashHandle Handle;

    public nuint Size
    {
        get
        {
            unsafe
            {
                return saucer_stash_size(Handle);
            }
        }
    }
    public byte[] Data
    {
        get
        {
            unsafe
            {
                var size = saucer_stash_size(Handle);
                var data = new byte[size];
                var ptr = saucer_stash_data(Handle);
                ReadOnlySpan<byte> source = new(ptr, (int)size);
                return source.ToArray();
            }
        }
    }

    internal SaucerStash(SaucerStashHandle handle)
    {
        Handle = handle;
    }
    public SaucerStash Copy()
    {
        unsafe
        {
            return new SaucerStash(saucer_stash_copy(Handle));
        }
    }

    public static SaucerStash FromBytes(byte[] data)
    {
        unsafe
        {
            return new SaucerStash(saucer_stash_new_from(ref data[0], (nuint)data.Length));
        }
    }
    public static SaucerStash FromString(string data)
    {
        unsafe
        {
            return new SaucerStash(saucer_stash_new_from_str(data));
        }
    }

    public void Dispose()
    {
        Handle.Dispose();
    }
}