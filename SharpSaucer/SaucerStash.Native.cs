using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate saucer_stash* SaucerStashLazyCallbackNative(IntPtr arg0);


internal struct saucer_stash { }


internal sealed class SaucerStashHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    public SaucerStashHandle(IntPtr handle) : base(handle, true)
    {
    }

    public SaucerStashHandle() : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        if (!IsInvalid)
        {
            SaucerStash.saucer_stash_free(this);
        }
        return true;
    }
}

public unsafe partial class SaucerStash
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial byte* saucer_stash_data(SaucerStashHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial nuint saucer_stash_size(SaucerStashHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_stash_free(SaucerStashHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerStashHandle saucer_stash_copy(SaucerStashHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerStashHandle saucer_stash_new_from(ref byte arg0, nuint arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerStashHandle saucer_stash_new_view(ref byte arg0, nuint arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerStashHandle saucer_stash_new_lazy(SaucerStashLazyCallbackNative arg0, IntPtr userdata);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerStashHandle saucer_stash_new_from_str(string arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerStashHandle saucer_stash_new_view_str(string arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerStashHandle saucer_stash_new_empty();

}
