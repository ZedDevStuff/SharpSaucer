using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate SaucerPolicy SaucerApplicationEventQuitNative(saucer_application* arg0, IntPtr arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerPostCallbackNative(IntPtr arg0);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerRunCallbackNative(saucer_application* arg0, IntPtr arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerFinishCallbackNative(saucer_application* arg0, IntPtr arg1);

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


internal sealed class SaucerApplicationHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    public SaucerApplicationHandle(IntPtr handle) : base(handle, true)
    {
    }

    public SaucerApplicationHandle() : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        if (!IsInvalid)
        {
            SaucerApplication.saucer_application_free(this);
        }
        return true;
    }
}

public unsafe partial class SaucerApplication
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_free(SaucerApplicationHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerApplicationHandle saucer_application_new(SaucerApplicationOptionsHandle arg0, out int error);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool saucer_application_thread_safe(SaucerApplicationHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_screens(SaucerApplicationHandle arg0, saucer_screen** arg1, ref nuint size);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_post(SaucerApplicationHandle arg0, SaucerPostCallbackNative arg1, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_quit(SaucerApplicationHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_application_run(SaucerApplicationHandle arg0, SaucerRunCallbackNative arg1, SaucerFinishCallbackNative arg2, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial nuint saucer_application_on(SaucerApplicationHandle arg0, SaucerApplicationEvent arg1, IntPtr callback, [MarshalAs(UnmanagedType.U1)] bool clearable, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_once(SaucerApplicationHandle arg0, SaucerApplicationEvent arg1, IntPtr callback, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_off(SaucerApplicationHandle arg0, SaucerApplicationEvent arg1, nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_off_all(SaucerApplicationHandle arg0, SaucerApplicationEvent arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_native(SaucerApplicationHandle arg0, nuint idx, IntPtr result, ref nuint size);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial IntPtr saucer_version();

}
