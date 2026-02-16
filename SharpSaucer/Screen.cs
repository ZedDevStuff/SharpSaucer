using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpSaucer;

/// <summary>
/// Managed wrapper around a native saucer screen.
/// </summary>
public sealed class Screen : IDisposable
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

    internal Screen(nint handle)
    {
        if (handle == 0)
            throw new InvalidOperationException("Invalid screen handle.");

        _handle = handle;
    }

    /// <summary>Wrap an existing native handle. Takes ownership.</summary>
    internal static Screen FromHandle(nint handle) => new(handle);

    // ── Properties ──────────────────────────────

    /// <summary>The display name of the screen.</summary>
    public string Name
    {
        get
        {
            var ptr = Bindings.saucer_screen_name(Handle);
            return ptr == 0 ? string.Empty : Marshal.PtrToStringUTF8(ptr) ?? string.Empty;
        }
    }

    /// <summary>The screen size in pixels (width, height).</summary>
    public (int Width, int Height) Size
    {
        get
        {
            Bindings.saucer_screen_size(Handle, out int w, out int h);
            return (w, h);
        }
    }

    /// <summary>The screen position (x, y).</summary>
    public (int X, int Y) Position
    {
        get
        {
            Bindings.saucer_screen_position(Handle, out int x, out int y);
            return (x, y);
        }
    }

    // ── IDisposable ─────────────────────────────

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        if (_handle != 0)
        {
            Bindings.saucer_screen_free(_handle);
            _handle = 0;
        }
    }

    ~Screen() => Dispose();
}
