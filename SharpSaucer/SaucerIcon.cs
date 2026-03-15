using System;

namespace SharpSaucer;


public partial class SaucerIcon : IDisposable
{
    internal SaucerIconHandle Handle;

    internal SaucerIcon(SaucerIconHandle handle)
    {
        Handle = handle;
    }

    public void Dispose()
    {
        Handle.Dispose();
    }
}
