using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SharpSaucer;

public partial class SaucerUrl : IDisposable
{
    internal unsafe saucer_url* Handle;
    private bool _disposedValue;
    private static readonly Dictionary<nint, SaucerUrl> Cache = [];

    public string Scheme
    {
        get
        {
            unsafe
            {
                nuint size = 0;
                saucer_url_scheme(Handle, null, ref size);
                fixed (sbyte* buffer = new sbyte[size])
                {
                    int length = (int)size;
                    saucer_url_scheme(Handle, buffer, ref size);
                    return new string(buffer, 0, length, Encoding.UTF8);
                }
            }
        }
    }
    public string Host
    {
        get
        {
            unsafe
            {
                nuint size = 0;
                saucer_url_host(Handle, null, ref size);
                fixed (sbyte* buffer = new sbyte[size])
                {
                    int length = (int)size;
                    saucer_url_host(Handle, buffer, ref size);
                    return new string(buffer, 0, length, Encoding.UTF8);
                }
            }
        }
    }
    public nuint? Port
    {
        get
        {
            unsafe
            {
                nuint port = 0;
                if (saucer_url_port(Handle, ref port))
                    return port;
                return null;
            }
        }
    }
    public string Path
    {
        get
        {
            unsafe
            {
                nuint size = 0;
                saucer_url_path(Handle, null, ref size);
                fixed(sbyte* buffer = new sbyte[size])
                {
                    int length = (int)size;
                    saucer_url_path(Handle, buffer, ref size);
                    return new string(buffer, 0, length, Encoding.UTF8);
                }
            }
        }
    }
    

    private unsafe SaucerUrl(saucer_url* handle)
    {
        Handle = handle;
        Cache[(nint)handle] = this;
    }
    internal static unsafe SaucerUrl FromHandle(saucer_url* handle)
    {
        if(handle == null || (nint)handle == nint.Zero)
            throw new ArgumentNullException(nameof(handle));
        return Cache.TryGetValue((nint)handle, out var cached) 
            ? cached 
            : new SaucerUrl(handle);
    }

    public override string ToString()
    {
        unsafe
        {
            nuint size = 0;
            saucer_url_string(Handle, null, ref size);
            fixed(sbyte* buffer = new sbyte[size])
            {
                int length = (int)size;
                saucer_url_string(Handle, buffer, ref size);
                return new string(buffer, 0, length, Encoding.UTF8);
            }
        }
    }

    public SaucerUrl Copy()
    {
        unsafe
        {
            return new SaucerUrl(saucer_url_copy(Handle));
        }
    }

    public static SaucerUrl Parse(string url)
    {
        unsafe
        {
            var handle = saucer_url_new_parse(url, out int error);
            if(error != 0)
                throw new ArgumentException($"Failed to parse URL: {url}");
            return new SaucerUrl(handle);
        }
    }
    public static SaucerUrl FromFile(string path)
    {
        if(!File.Exists(path) || !Directory.Exists(path))
            throw new ArgumentException($"Given path does not exist: {path}");
        unsafe
        {
            var handle = saucer_url_new_from(path, out int error);
            if (error != 0)
                throw new ArgumentException($"Failed to create URL from path: {path}");
            return new SaucerUrl(handle);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }
            unsafe
            {
                Cache.Remove((nint)Handle);
                saucer_url_free(Handle);
            }
            _disposedValue = true;
        }
    }

    ~SaucerUrl()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
