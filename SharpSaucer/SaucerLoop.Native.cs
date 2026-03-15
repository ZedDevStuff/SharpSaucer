using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;



internal struct saucer_loop { }


internal sealed class SaucerLoopHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    public SaucerLoopHandle(IntPtr handle) : base(handle, true)
    {
    }

    public SaucerLoopHandle() : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        if (!IsInvalid)
        {
            SaucerLoop.saucer_loop_free(this);
        }
        return true;
    }
}

public unsafe partial class SaucerLoop
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_loop_free(SaucerLoopHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_loop_run(SaucerLoopHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_loop_iteration(SaucerLoopHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_loop_quit(SaucerLoopHandle arg0);

}
