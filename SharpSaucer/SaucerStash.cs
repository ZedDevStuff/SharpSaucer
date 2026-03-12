using System;

namespace SharpSaucer;

public partial class SaucerStash : IDisposable
{
    internal unsafe saucer_stash* Handle;
    private bool _disposedValue;

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

    internal unsafe SaucerStash(saucer_stash* handle)
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
                saucer_stash_free(Handle);
            }
            _disposedValue = true;
        }
    }

    // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~SaucerStash()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}