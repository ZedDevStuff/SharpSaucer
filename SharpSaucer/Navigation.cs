using SharpSaucer.Native;

namespace SharpSaucer;

public unsafe class Navigation : StructWrapper
{
    internal Navigation(nint handle) : base(handle)
    {
    }
    internal Navigation(SaucerNavigation* handle) : base((nint)handle)
    {
    }

    /// <summary>The URL being navigated to. The caller owns the returned Url and must dispose it.</summary>
    public Url Url => new Url(NativeMethods.saucer_navigation_url((SaucerNavigation*)Handle));

    /// <summary>Whether the navigation targets a new window.</summary>
    public bool NewWindow => NativeMethods.saucer_navigation_new_window((SaucerNavigation*)Handle);

    /// <summary>Whether this navigation is a redirection.</summary>
    public bool Redirection => NativeMethods.saucer_navigation_redirection((SaucerNavigation*)Handle);

    /// <summary>Whether the navigation was initiated by the user.</summary>
    public bool UserInitiated => NativeMethods.saucer_navigation_user_initiated((SaucerNavigation*)Handle);

    public override void Free()
    {
    }
}
