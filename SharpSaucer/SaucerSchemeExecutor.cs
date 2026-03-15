using System;

namespace SharpSaucer;

public partial class SaucerSchemeExecutor : IDisposable
{
    internal SaucerSchemeExecutorHandle Handle;

    internal SaucerSchemeExecutor(SaucerSchemeExecutorHandle handle)
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

    public void Dispose()
    {
        Handle.Dispose();
    }
}