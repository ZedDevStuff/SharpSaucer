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

    private SchemeResponse(nint handle)
    {
        if (handle == 0)
            throw new InvalidOperationException("Failed to create SchemeResponse.");

        _handle = handle;
    }

    // ── Constructors ────────────────────────────

    /// <summary>Create a new scheme response from a stash and MIME type.</summary>
    public SchemeResponse(Stash content, string mime)
    {
        ArgumentNullException.ThrowIfNull(content);
        ArgumentNullException.ThrowIfNull(mime);
        _handle = Bindings.saucer_scheme_response_new(content.Handle, mime);

        if (_handle == 0)
            throw new InvalidOperationException("Failed to create SchemeResponse.");
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

    ~SchemeResponse()
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
