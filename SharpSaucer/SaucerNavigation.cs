using System;

namespace SharpSaucer;

public partial class SaucerNavigation
{
    internal unsafe saucer_navigation* Handle;

    public SaucerUrl Url
    {
        get
        {
            unsafe
            {
                return SaucerUrl.FromHandle(saucer_navigation_url(Handle));
            }
        }
    }
    public bool NewWindow
    {
        get
        {
            unsafe
            {
                return saucer_navigation_new_window(Handle);
            }
        }
    }
    public bool Redirection
    {
        get
        {
            unsafe
            {
                return saucer_navigation_redirection(Handle);
            }
        }
    }
    public bool UserInitiated
    {
        get
        {
            unsafe
            {
                return saucer_navigation_user_initiated(Handle);
            }
        }
    }

    internal unsafe SaucerNavigation(saucer_navigation* handle)
    {
        Handle = handle;
    }
}
