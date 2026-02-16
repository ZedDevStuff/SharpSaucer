using System;
using System.Runtime.InteropServices;

using SharpSaucer.Types;

namespace SharpSaucer;

/// <summary>
/// Managed wrapper around a native saucer stash (an opaque binary blob).
/// </summary>
public sealed class Stash : IDisposable
{
    private nint _handle;
    private bool _disposed;

    /// <summary>The underlying native handle.</summary>
    public nint Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return _handle;
        }
    }

    private Stash(nint handle)
    {
        if (handle == 0)
            throw new InvalidOperationException("Failed to create Stash.");

        _handle = handle;
    }

    // ── Constructors ────────────────────────────

    /// <summary>Create a stash by copying the given byte array.</summary>
    public unsafe Stash(byte[] data)
    {
        ArgumentNullException.ThrowIfNull(data);
        fixed (byte* ptr = data)
            _handle = Bindings.saucer_stash_new_from((nint)ptr, (nuint)data.Length);

        if (_handle == 0)
            throw new InvalidOperationException("Failed to create Stash.");
    }

    /// <summary>Create a stash from a UTF-8 string (copies).</summary>
    public Stash(string str)
    {
        ArgumentNullException.ThrowIfNull(str);
        _handle = Bindings.saucer_stash_new_from_str(str);

        if (_handle == 0)
            throw new InvalidOperationException("Failed to create Stash.");
    }

    // ── Factories ───────────────────────────────

    /// <summary>Create an empty stash.</summary>
    public static Stash CreateEmpty()
        => new(Bindings.saucer_stash_new_empty());

    /// <summary>Create a stash that copies the given data.</summary>
    public static unsafe Stash FromBytes(ReadOnlySpan<byte> data)
    {
        fixed (byte* ptr = data)
            return new Stash(Bindings.saucer_stash_new_from((nint)ptr, (nuint)data.Length));
    }

    /// <summary>Create a stash that views (does not copy) the given data. The caller must keep the data alive.</summary>
    public static unsafe Stash ViewBytes(ReadOnlySpan<byte> data)
    {
        fixed (byte* ptr = data)
            return new Stash(Bindings.saucer_stash_new_view((nint)ptr, (nuint)data.Length));
    }

    /// <summary>Create a stash from a UTF-8 string (copies).</summary>
    public static Stash FromString(string str)
        => new(Bindings.saucer_stash_new_from_str(str));

    /// <summary>Create a stash that views a UTF-8 string (does not copy).</summary>
    public static Stash ViewString(string str)
        => new(Bindings.saucer_stash_new_view_str(str));

    /// <summary>Create a lazy stash that invokes a callback when the data is needed.</summary>
    public static Stash CreateLazy(SaucerStashLazyCallback callback, nint userdata = 0)
        => new(Bindings.saucer_stash_new_lazy(callback, userdata));

    // ── Properties ──────────────────────────────

    /// <summary>Pointer to the raw data.</summary>
    public nint Data => Bindings.saucer_stash_data(Handle);

    /// <summary>Size of the stash in bytes.</summary>
    public nuint Size => Bindings.saucer_stash_size(Handle);

    // ── Methods ─────────────────────────────────

    /// <summary>Create an independent copy of this stash.</summary>
    public Stash Copy() => new(Bindings.saucer_stash_copy(Handle));

    /// <summary>Copy the stash data into a managed byte array.</summary>
    public unsafe byte[] ToArray()
    {
        var size = (int)Size;
        var result = new byte[size];
        var src = Data;
        if (src != 0 && size > 0)
            new Span<byte>((void*)src, size).CopyTo(result);

        return result;
    }

    // ── IDisposable ─────────────────────────────

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        if (_handle != 0)
        {
            Bindings.saucer_stash_free(_handle);
            _handle = 0;
        }
    }

    ~Stash() => Dispose();
}
