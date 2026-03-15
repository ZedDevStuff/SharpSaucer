using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;



internal struct saucer_desktop { }


internal sealed class SaucerDesktopHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    public SaucerDesktopHandle(IntPtr handle) : base(handle, true)
    {
    }

    public SaucerDesktopHandle() : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        if (!IsInvalid)
        {
            SaucerDesktop.saucer_desktop_free(this);
        }
        return true;
    }
}

public unsafe partial class SaucerDesktop
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_desktop_free(SaucerDesktopHandle arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_desktop_mouse_position(SaucerDesktopHandle arg0, out int x, out int y);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_pick_file(SaucerDesktopHandle arg0, SaucerPickerOptionsHandle arg1, sbyte* arg2, ref nuint arg3, out int error);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_pick_folder(SaucerDesktopHandle arg0, SaucerPickerOptionsHandle arg1, sbyte* arg2, ref nuint arg3, out int error);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_pick_files(SaucerDesktopHandle arg0, SaucerPickerOptionsHandle arg1, sbyte* arg2, ref nuint arg3, out int error);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_save(SaucerDesktopHandle arg0, SaucerPickerOptionsHandle arg1, sbyte* arg2, ref nuint arg3, out int error);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_desktop_open(SaucerDesktopHandle arg0, string arg1);

}
