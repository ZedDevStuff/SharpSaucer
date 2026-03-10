using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate SaucerStash* SaucerStashLazyCallback(IntPtr arg0);

public struct SaucerStash { }

internal static unsafe partial class NativeMethods
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* saucer_stash_data(SaucerStash* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint saucer_stash_size(SaucerStash* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_stash_free(SaucerStash* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerStash* saucer_stash_copy(SaucerStash* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerStash* saucer_stash_new_from(out byte arg0, nuint arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerStash* saucer_stash_new_view(out byte arg0, nuint arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerStash* saucer_stash_new_lazy(SaucerStashLazyCallback arg0, IntPtr userdata);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerStash* saucer_stash_new_from_str(string arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerStash* saucer_stash_new_view_str(string arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerStash* saucer_stash_new_empty();

}
