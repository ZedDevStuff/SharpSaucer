using System;

using SharpSaucer.Native;

namespace SharpSaucer;

public class Url : StructWrapper
{
    /// <summary>The scheme component (e.g. "https").</summary>
    public string Scheme
    {
        get
        {
            unsafe
            {
                NativeMethods.saucer_url_scheme((SaucerUrl*)Handle, 0, out nuint length);
                if (length == 0)
                    return string.Empty;
                var buf = stackalloc byte[(int)length];
                NativeMethods.saucer_url_scheme((SaucerUrl*)Handle, (nint)buf, out _);
                return System.Text.Encoding.UTF8.GetString(buf, (int)length);
            }
        }
    }

    /// <summary>The host component.</summary>
    public string Host
    {
        get
        {
            unsafe
            {
                NativeMethods.saucer_url_host((SaucerUrl*)Handle, 0, out nuint length);
                if (length == 0)
                    return string.Empty;
                var buf = stackalloc byte[(int)length];
                NativeMethods.saucer_url_host((SaucerUrl*)Handle, (nint)buf, out _);
                return System.Text.Encoding.UTF8.GetString(buf, (int)length);
            }
        }
    }

    /// <summary>The path component.</summary>
    public string Path
    {
        get
        {
            unsafe
            {
                char* ptr = null;
                NativeMethods.saucer_url_path((SaucerUrl*)Handle, (nint)ptr, out nuint length);
                if (length == 0)
                    return string.Empty;
                return System.Text.Encoding.UTF8.GetString((byte*)ptr, (int)length);
            }
        }
    }

    /// <summary>The user component (if present).</summary>
    public string User
    {
        get
        {
            unsafe
            {
                NativeMethods.saucer_url_user((SaucerUrl*)Handle, 0, out nuint length);
                if (length == 0)
                    return string.Empty;
                var buf = stackalloc byte[(int)length];
                NativeMethods.saucer_url_user((SaucerUrl*)Handle, (nint)buf, out _);
                return System.Text.Encoding.UTF8.GetString(buf, (int)length);
            }
        }
    }

    /// <summary>The password component (if present).</summary>
    public string Password
    {
        get
        {
            unsafe
            {
                NativeMethods.saucer_url_password((SaucerUrl*)Handle, 0, out nuint length);
                if (length == 0)
                    return string.Empty;
                var buf = stackalloc byte[(int)length];
                NativeMethods.saucer_url_password((SaucerUrl*)Handle, (nint)buf, out _);
                return System.Text.Encoding.UTF8.GetString(buf, (int)length);
            }
        }
    }

    /// <summary>The port, or null if not set.</summary>
    public nuint? Port
    {
        get
        {
            unsafe
            {
                return NativeMethods.saucer_url_port((SaucerUrl*)Handle, out var port) ? port : null;
            }
        }
    }

    internal Url(nint handle) : base(handle)
    {
    }
    internal unsafe Url(SaucerUrl* handle) : base((nint)handle)
    {
    }
    public Url(string url)
    {
        unsafe
        {
            Handle = (nint)NativeMethods.saucer_url_new_from(url, out int error);
            if (error != 0)
                throw new Exception($"Failed to create URL. Error code: {error}");
        }
    }

    public static Url Parse(string url)
    {
        unsafe
        {
            var handle = NativeMethods.saucer_url_new_parse(url, out int error);
            if (error != 0)
                throw new Exception($"Failed to parse URL. Error code: {error}");
            return new Url(handle);
        }
    }

    public override void Free()
    {
        unsafe
        {
            NativeMethods.saucer_url_free((SaucerUrl*)Handle);
        }
    }
}
