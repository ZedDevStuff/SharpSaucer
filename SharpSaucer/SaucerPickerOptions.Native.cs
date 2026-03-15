using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;



internal struct saucer_picker_options { }


internal sealed class SaucerPickerOptionsHandle : SafeHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    public SaucerPickerOptionsHandle(IntPtr handle) : base(handle, true)
    {
    }

    public SaucerPickerOptionsHandle() : base(IntPtr.Zero, true)
    {
    }

    protected override bool ReleaseHandle()
    {
        if (!IsInvalid)
        {
            SaucerPickerOptions.saucer_picker_options_free(this);
        }
        return true;
    }
}

public unsafe partial class SaucerPickerOptions
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_options_free(SaucerPickerOptionsHandle arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_options_set_initial(SaucerPickerOptionsHandle arg0, string arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_picker_options_set_filters(SaucerPickerOptionsHandle arg0, string arg1, nuint arg2);

}
