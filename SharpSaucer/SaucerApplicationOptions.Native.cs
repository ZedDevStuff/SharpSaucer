using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;



internal struct saucer_application_options { }


internal sealed class SaucerApplicationOptionsHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    public SaucerApplicationOptionsHandle(IntPtr handle) : base(handle, true)
    {
    }

    public SaucerApplicationOptionsHandle() : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        if (!IsInvalid)
        {
            SaucerApplicationOptions.saucer_application_options_free(this);
        }
        return true;
    }
}

public unsafe partial class SaucerApplicationOptions
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_options_free(SaucerApplicationOptionsHandle arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial SaucerApplicationOptionsHandle saucer_application_options_new(string id);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_options_set_argc(SaucerApplicationOptionsHandle arg0, int arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_options_set_argv(SaucerApplicationOptionsHandle arg0, IntPtr[] arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_application_options_set_quit_on_last_window_closed(SaucerApplicationOptionsHandle arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

}
