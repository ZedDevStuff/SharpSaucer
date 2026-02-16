using System;

using SharpSaucer.Types;

namespace SharpSaucer;

/// <summary>
/// Managed wrapper around a native saucer scheme response.
/// Used to construct responses for custom scheme handlers.
/// </summary>
public sealed class SchemeResponse : IDisposable
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

    private SchemeResponse(nint handle)
    {
        if (handle == 0)
            throw new InvalidOperationException("Failed to create SchemeResponse.");

        _handle = handle;
    }

    // ── Factories ───────────────────────────────

    /// <summary>Create a new scheme response from a stash and MIME type.</summary>
    public static SchemeResponse Create(Stash content, string mime)
        => new(Bindings.saucer_scheme_response_new(content.Handle, mime));

    // ── Methods ─────────────────────────────────

    /// <summary>Append a response header.</summary>
    public void AppendHeader(string name, string value)
        => Bindings.saucer_scheme_response_append_header(Handle, name, value);

    /// <summary>Set the HTTP status code of the response.</summary>
    public void SetStatus(int status)
        => Bindings.saucer_scheme_response_set_status(Handle, status);

    // ── IDisposable ─────────────────────────────

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        if (_handle != 0)
        {
            Bindings.saucer_scheme_response_free(_handle);
            _handle = 0;
        }
    }

    ~SchemeResponse() => Dispose();
}
