using SharpSaucer.Native;

namespace SharpSaucer;

public class SchemeResponse : StructWrapper
{
    public SchemeResponse(Stash content, string contentType)
    {
        unsafe
        {
            Handle = (nint)NativeMethods.saucer_scheme_response_new((SaucerStash*)content.Handle, contentType);
        }
    }

    public void AppendHeader(string header, string value)
    {
        unsafe
        {
            NativeMethods.saucer_scheme_response_append_header((SaucerSchemeResponse*)Handle, header, value);
        }
    }
    public void SetStatus(int status)
    {
        unsafe
        {
            NativeMethods.saucer_scheme_response_set_status((SaucerSchemeResponse*)Handle, status);
        }
    }

    public override void Free()
    {
        unsafe
        {
            NativeMethods.saucer_scheme_response_free((SaucerSchemeResponse*)Handle);
        }
    }
}
