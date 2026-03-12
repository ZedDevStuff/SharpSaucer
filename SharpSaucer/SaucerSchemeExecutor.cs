using System;

namespace SharpSaucer;

public partial class SaucerSchemeExecutor : IDisposable
{
    internal unsafe saucer_scheme_executor* Handle;
    private bool _disposedValue;

    internal unsafe SaucerSchemeExecutor(saucer_scheme_executor* handle)
    {
        Handle = handle;
    }

    public void Accept(SaucerSchemeResponse response)
    {
        unsafe
        {
            saucer_scheme_executor_accept(Handle, response.Handle);
        }
    }
    public void Reject(SaucerSchemeError error)
    {
        unsafe
        {
            saucer_scheme_executor_reject(Handle, error);
        }
    }

    public SaucerSchemeExecutor Copy()
    {
        unsafe
        {
            return new SaucerSchemeExecutor(saucer_scheme_executor_copy(Handle));
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
                saucer_scheme_executor_free(Handle);
            }
            _disposedValue = true;
        }
    }

    ~SaucerSchemeExecutor()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}