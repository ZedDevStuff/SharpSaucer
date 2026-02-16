using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpSaucer;

/// <summary>
/// Managed wrapper around a native saucer URL.
/// </summary>
public sealed class Url : IDisposable
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

    private Url(nint handle)
    {
        if (handle == 0)
            throw new InvalidOperationException("Failed to create Url.");

        _handle = handle;
    }

    // ── Factories ───────────────────────────────

    /// <summary>Parse a URL string (percent-encoded).</summary>
    public static Url Parse(string str)
    {
        int error = 0;
        var handle = Bindings.saucer_url_new_parse(str, ref error);
        if (error != 0 || handle == 0)
            throw new InvalidOperationException($"Failed to parse URL (error={error}).");

        return new Url(handle);
    }

    /// <summary>Create a URL from a plain string.</summary>
    public static Url From(string str)
    {
        int error = 0;
        var handle = Bindings.saucer_url_new_from(str, ref error);
        if (error != 0 || handle == 0)
            throw new InvalidOperationException($"Failed to create URL from string (error={error}).");

        return new Url(handle);
    }

    /// <summary>Create a URL from individual components.</summary>
    public static Url FromComponents(string scheme, string host, nuint? port = null, string path = "/")
    {
        if (port.HasValue)
        {
            var p = port.Value;
            return new Url(Bindings.saucer_url_new_opts(scheme, host, ref p, path));
        }
        else
        {
            nuint zero = 0;
            return new Url(Bindings.saucer_url_new_opts(scheme, host, ref zero, path));
        }
    }

    /// <summary>Wrap an existing native handle. Takes ownership.</summary>
    internal static Url FromHandle(nint handle) => new(handle);

    // ── Properties ──────────────────────────────

    /// <summary>The scheme component (e.g. "https").</summary>
    public string Scheme => ReadStringProperty(Bindings.saucer_url_scheme);

    /// <summary>The host component.</summary>
    public string Host => ReadStringProperty(Bindings.saucer_url_host);

    /// <summary>The path component.</summary>
    public string Path => ReadStringProperty(Bindings.saucer_url_path);

    /// <summary>The user component (if present).</summary>
    public string User => ReadStringProperty(Bindings.saucer_url_user);

    /// <summary>The password component (if present).</summary>
    public string Password => ReadStringProperty(Bindings.saucer_url_password);

    /// <summary>The port, or null if not set.</summary>
    public nuint? Port
    {
        get
        {
            nuint port = 0;
            return Bindings.saucer_url_port(Handle, ref port) ? port : null;
        }
    }

    // ── Methods ─────────────────────────────────

    /// <summary>Create an independent copy of this URL.</summary>
    public Url Copy() => new(Bindings.saucer_url_copy(Handle));

    /// <summary>Return the full URL as a string.</summary>
    public override string ToString() => ReadStringProperty(Bindings.saucer_url_string);

    // ── Helpers ─────────────────────────────────

    private delegate void StringGetter(nint handle, nint buffer, ref nuint size);

    private unsafe string ReadStringProperty(StringGetter getter)
    {
        nuint size = 0;
        getter(Handle, 0, ref size);

        if (size == 0)
            return string.Empty;

        var buf = stackalloc byte[(int)size];
        getter(Handle, (nint)buf, ref size);

        return Encoding.UTF8.GetString(buf, (int)size);
    }

    // ── IDisposable ─────────────────────────────

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        if (_handle != 0)
        {
            Bindings.saucer_url_free(_handle);
            _handle = 0;
        }
    }

    ~Url() => Dispose();
}
