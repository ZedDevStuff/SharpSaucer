using System;

namespace SharpSaucer;

/// <summary>
/// Managed wrapper around a native saucer icon.
/// </summary>
public sealed class Icon : IDisposable
{
    private nint _handle;
    private bool _disposedValue;

    /// <summary>The underlying native handle.</summary>
    public nint Handle
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposedValue, this);
            return _handle;
        }
    }

    private Icon(nint handle)
    {
        if (handle == 0)
            throw new InvalidOperationException("Failed to create Icon.");

        _handle = handle;
    }

    // ── Constructors ────────────────────────────

    /// <summary>Load an icon from a file on disk.</summary>
    public Icon(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        int error = 0;
        _handle = Bindings.saucer_icon_new_from_file(path, ref error);

        if (error != 0 || _handle == 0)
            throw new InvalidOperationException($"Failed to load icon from file (error={error}).");
    }

    /// <summary>Load an icon from a stash (in-memory image data).</summary>
    public Icon(Stash stash)
    {
        ArgumentNullException.ThrowIfNull(stash);
        int error = 0;
        _handle = Bindings.saucer_icon_new_from_stash(stash.Handle, ref error);

        if (error != 0 || _handle == 0)
            throw new InvalidOperationException($"Failed to load icon from stash (error={error}).");
    }

    // ── Factories ───────────────────────────────

    /// <summary>Load an icon from a file on disk.</summary>
    public static Icon FromFile(string path)
    {
        int error = 0;
        var handle = Bindings.saucer_icon_new_from_file(path, ref error);
        if (error != 0 || handle == 0)
            throw new InvalidOperationException($"Failed to load icon from file (error={error}).");

        return new Icon(handle);
    }

    /// <summary>Load an icon from a stash (in-memory image data).</summary>
    public static Icon FromStash(Stash stash)
    {
        int error = 0;
        var handle = Bindings.saucer_icon_new_from_stash(stash.Handle, ref error);
        if (error != 0 || handle == 0)
            throw new InvalidOperationException($"Failed to load icon from stash (error={error}).");

        return new Icon(handle);
    }

    /// <summary>Wrap an existing native handle. Takes ownership.</summary>
    internal static Icon FromHandle(nint handle) => new(handle);

    // ── Properties ──────────────────────────────

    /// <summary>Whether the icon has no image data.</summary>
    public bool Empty => Bindings.saucer_icon_empty(Handle);

    /// <summary>Pointer to the underlying native icon data (platform-specific).</summary>
    public nint Data => Bindings.saucer_icon_data(Handle);

    // ── Methods ─────────────────────────────────

    /// <summary>Create an independent copy of this icon.</summary>
    public Icon Copy() => new(Bindings.saucer_icon_copy(Handle));

    /// <summary>Save the icon to a file on disk.</summary>
    public void Save(string path) => Bindings.saucer_icon_save(Handle, path);

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                
            }
            if (_handle != 0)
            {
                Bindings.saucer_window_free(_handle);
                _handle = 0;
            }
            _disposedValue = true;
        }
    }

    ~Icon()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
