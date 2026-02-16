using System;

namespace SharpSaucer;

/// <summary>
/// Managed wrapper around a native saucer navigation.
/// This is a read-only, non-owning wrapper — the native side owns the lifetime.
/// </summary>
public readonly struct Navigation
{
    private readonly nint _handle;

    /// <summary>The underlying native handle.</summary>
    public nint Handle => _handle;

    internal Navigation(nint handle)
    {
        _handle = handle;
    }

    // ── Properties ──────────────────────────────

    /// <summary>The URL being navigated to. The caller owns the returned Url and must dispose it.</summary>
    public Url Url => Url.FromHandle(Bindings.saucer_navigation_url(_handle));

    /// <summary>Whether the navigation targets a new window.</summary>
    public bool NewWindow => Bindings.saucer_navigation_new_window(_handle);

    /// <summary>Whether this navigation is a redirection.</summary>
    public bool Redirection => Bindings.saucer_navigation_redirection(_handle);

    /// <summary>Whether the navigation was initiated by the user.</summary>
    public bool UserInitiated => Bindings.saucer_navigation_user_initiated(_handle);
}
