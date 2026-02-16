using System;

using SharpSaucer.Types;

namespace SharpSaucer;

/// <summary>
/// Managed wrapper around a native saucer permission request.
/// </summary>
public sealed class PermissionRequest : IDisposable
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

    internal PermissionRequest(nint handle)
    {
        if (handle == 0)
            throw new InvalidOperationException("Invalid permission request handle.");

        _handle = handle;
    }

    /// <summary>Wrap an existing native handle. Takes ownership.</summary>
    internal static PermissionRequest FromHandle(nint handle) => new(handle);

    // ── Properties ──────────────────────────────

    /// <summary>The URL associated with the permission request. The caller owns the returned Url and must dispose it.</summary>
    public Url Url => Url.FromHandle(Bindings.saucer_permission_request_url(Handle));

    /// <summary>The type of permission being requested.</summary>
    public SaucerPermissionType PermissionType => Bindings.saucer_permission_request_type(Handle);

    // ── Methods ─────────────────────────────────

    /// <summary>Accept or deny the permission request.</summary>
    public void Accept(bool accept = true) => Bindings.saucer_permission_request_accept(Handle, accept);

    /// <summary>Deny the permission request.</summary>
    public void Deny() => Accept(false);

    /// <summary>Create an independent copy of this request.</summary>
    public PermissionRequest Copy() => new(Bindings.saucer_permission_request_copy(Handle));

    // ── IDisposable ─────────────────────────────

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        if (_handle != 0)
        {
            Bindings.saucer_permission_request_free(_handle);
            _handle = 0;
        }
    }

    ~PermissionRequest() => Dispose();
}
