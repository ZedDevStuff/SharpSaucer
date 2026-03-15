using System;

namespace SharpSaucer;

public partial class SaucerPermissionRequest : IDisposable
{
    internal SaucerPermissionRequestHandle Handle;

    internal SaucerPermissionRequest(SaucerPermissionRequestHandle handle)
    {
        Handle = handle;
    }

    public void Dispose()
    {
        Handle.Dispose();
    }
}
