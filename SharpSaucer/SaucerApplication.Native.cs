using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate SaucerPolicy SaucerApplicationEventQuit(saucer_application* arg0, IntPtr arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerPostCallback(IntPtr arg0);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerRunCallback(saucer_application* arg0, IntPtr arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerFinishCallback(saucer_application* arg0, IntPtr arg1);

public enum SaucerPolicy
{
    Allow = 0,
    Block = 1,
}

public enum SaucerApplicationEvent
{
    Quit = 0,
}

internal struct saucer_application { }

public unsafe partial class SaucerApplication
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_free(saucer_application* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_application* saucer_application_new(saucer_application_options* arg0, out int error);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool saucer_application_thread_safe(saucer_application* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_screens(saucer_application* arg0, saucer_screen** arg1, ref nuint size);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_post(saucer_application* arg0, SaucerPostCallback arg1, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_quit(saucer_application* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_application_run(saucer_application* arg0, SaucerRunCallback arg1, SaucerFinishCallback arg2, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial nuint saucer_application_on(saucer_application* arg0, SaucerApplicationEvent arg1, IntPtr callback, [MarshalAs(UnmanagedType.U1)] bool clearable, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_once(saucer_application* arg0, SaucerApplicationEvent arg1, IntPtr callback, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_off(saucer_application* arg0, SaucerApplicationEvent arg1, nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_off_all(saucer_application* arg0, SaucerApplicationEvent arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_native(saucer_application* arg0, nuint idx, IntPtr result, ref nuint size);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr saucer_version();

}
