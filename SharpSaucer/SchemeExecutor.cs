using System;

using SharpSaucer.Types;

namespace SharpSaucer;

/// <summary>
/// Managed wrapper around a native saucer scheme executor.
/// Used in scheme handler callbacks to accept or reject a request.
/// </summary>
public sealed class SchemeExecutor : IDisposable
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

    internal SchemeExecutor(nint handle)
    {
        if (handle == 0)
            throw new InvalidOperationException("Invalid scheme executor handle.");

        _handle = handle;
    }

    /// <summary>Wrap an existing native handle. Takes ownership.</summary>
    internal static SchemeExecutor FromHandle(nint handle) => new(handle);

    // ── Methods ─────────────────────────────────

    /// <summary>Accept the scheme request with the given response.</summary>
    public void Accept(SchemeResponse response)
        => Bindings.saucer_scheme_executor_accept(Handle, response.Handle);

    /// <summary>Reject the scheme request with an error.</summary>
    public void Reject(SaucerSchemeError error)
        => Bindings.saucer_scheme_executor_reject(Handle, error);

    /// <summary>Create an independent copy of this executor.</summary>
    public SchemeExecutor Copy() => new(Bindings.saucer_scheme_executor_copy(Handle));

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

    ~SchemeExecutor()
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
