using SharpSaucer.Native;

namespace SharpSaucer;

public class SchemeExecutor : StructWrapper
{
    internal SchemeExecutor(nint handle) : base(handle)
    {
    }
    internal unsafe SchemeExecutor(SaucerSchemeExecutor* handle) : base((nint)handle)
    {
    }

    public void Accept(SchemeResponse response)
    {
        unsafe
        {
            NativeMethods.saucer_scheme_executor_accept((SaucerSchemeExecutor*)Handle, (SaucerSchemeResponse*)response.Handle);
        }
    }
    public void Reject(SaucerSchemeError error)
    {
        unsafe
        {
            NativeMethods.saucer_scheme_executor_reject((SaucerSchemeExecutor*)Handle, error);
        }
    }
    public SchemeExecutor Copy()
    {
        unsafe
        {
            return new SchemeExecutor(NativeMethods.saucer_scheme_executor_copy((SaucerSchemeExecutor*)Handle));
        }
    }

    public override void Free()
    {
        unsafe
        {
            NativeMethods.saucer_scheme_executor_free((SaucerSchemeExecutor*)Handle);
        }
    }
}
