using System;

namespace SharpSaucer;

public abstract class StructWrapper : IDisposable
{
    public nint Handle { get; init; }
    protected StructWrapper()
    {

    }
    protected StructWrapper(nint handle)
    {
        Handle = handle;
    }
    public abstract void Free();
    public void Dispose() => Free();

    public static implicit operator nint(StructWrapper wrapper) => wrapper.Handle;
}
