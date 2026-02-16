using System;
using System.Text;

namespace SharpSaucer;

/// <summary>
/// Managed wrapper around a native saucer scheme request.
/// This is a read-only wrapper typically received in scheme handler callbacks.
/// </summary>
public sealed class SchemeRequest : IDisposable
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

    internal SchemeRequest(nint handle)
    {
        if (handle == 0)
            throw new InvalidOperationException("Invalid scheme request handle.");

        _handle = handle;
    }

    /// <summary>Wrap an existing native handle. Takes ownership.</summary>
    internal static SchemeRequest FromHandle(nint handle) => new(handle);

    // ── Properties ──────────────────────────────

    /// <summary>The request URL. The caller owns the returned Url and must dispose it.</summary>
    public Url Url => Url.FromHandle(Bindings.saucer_scheme_request_url(Handle));

    /// <summary>The HTTP method (e.g. "GET", "POST").</summary>
    public unsafe string Method
    {
        get
        {
            nuint size = 0;
            Bindings.saucer_scheme_request_method(Handle, 0, ref size);
            if (size == 0) return string.Empty;

            var buf = stackalloc byte[(int)size];
            Bindings.saucer_scheme_request_method(Handle, (nint)buf, ref size);
            return Encoding.UTF8.GetString(buf, (int)size);
        }
    }

    /// <summary>The request body content as a native stash pointer, or IntPtr.Zero if none.</summary>
    public nint ContentHandle => Bindings.saucer_scheme_request_content(Handle);

    /// <summary>The request headers as a raw string.</summary>
    public unsafe string Headers
    {
        get
        {
            nuint size = 0;
            Bindings.saucer_scheme_request_headers(Handle, 0, ref size);
            if (size == 0) return string.Empty;

            var buf = stackalloc byte[(int)size];
            Bindings.saucer_scheme_request_headers(Handle, (nint)buf, ref size);
            return Encoding.UTF8.GetString(buf, (int)size);
        }
    }

    // ── Methods ─────────────────────────────────

    /// <summary>Create an independent copy of this request.</summary>
    public SchemeRequest Copy() => new(Bindings.saucer_scheme_request_copy(Handle));

    // ── IDisposable ─────────────────────────────

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        if (_handle != 0)
        {
            Bindings.saucer_scheme_request_free(_handle);
            _handle = 0;
        }
    }

    ~SchemeRequest() => Dispose();
}
