using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SharpSaucer;

public partial class SaucerSchemeResponse : IDisposable
{
    internal SaucerSchemeResponseHandle Handle;

    public int Status
    {
        set
        {
            unsafe
            {
                saucer_scheme_response_set_status(Handle, value);
            }
        }
    }

    internal SaucerSchemeResponse(SaucerSchemeResponseHandle handle)
    {
        Handle = handle;
    }

    public SaucerSchemeResponse(SaucerStash stash, string mime)
    {
        unsafe
        {
            Handle = saucer_scheme_response_new(stash.Handle, mime);
        }
    }

    public void AppendHeader(string name, string value)
    {
        unsafe
        {
            saucer_scheme_response_append_header(Handle, name, value);
        }
    }

    public void Dispose()
    {
        Handle.Dispose();
    }
}
