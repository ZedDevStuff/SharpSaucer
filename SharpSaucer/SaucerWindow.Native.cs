using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWindowEventDecorated(saucer_window* arg0, SaucerWindowDecoration arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWindowEventMaximize(saucer_window* arg0, bool arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWindowEventMinimize(saucer_window* arg0, bool arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWindowEventClosed(saucer_window* arg0, IntPtr arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWindowEventResize(saucer_window* arg0, int arg1, int arg2, IntPtr arg3);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerWindowEventFocus(saucer_window* arg0, bool arg1, IntPtr arg2);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate SaucerPolicy SaucerWindowEventClose(saucer_window* arg0, IntPtr arg1);

public enum SaucerWindowEdge
{
    Top = 1,
    Bottom = 2,
    Left = 4,
    Right = 8,
    BottomLeft = 6,
    BottomRight = 10,
    TopLeft = 5,
    TopRight = 9,
}

public enum SaucerWindowDecoration
{
    None = 0,
    Partial = 1,
    Full = 2,
}

public enum SaucerWindowEvent
{
    Decorated = 0,
    Maximize = 1,
    Minimize = 2,
    Closed = 3,
    Resize = 4,
    Focus = 5,
    Close = 6,
}

internal struct saucer_window { }

public unsafe partial class SaucerWindow
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_free(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_window* saucer_window_new(saucer_application* arg0, out int error);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool saucer_window_visible(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool saucer_window_focused(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool saucer_window_minimized(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool saucer_window_maximized(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool saucer_window_resizable(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool saucer_window_fullscreen(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool saucer_window_always_on_top(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool saucer_window_click_through(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_title(saucer_window* arg0, sbyte* arg1, ref nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_background(saucer_window* arg0, ref byte r, ref byte g, ref byte b, ref byte a);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int saucer_window_decorations(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_size(saucer_window* arg0, out int w, out int h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_max_size(saucer_window* arg0, out int w, out int h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_min_size(saucer_window* arg0, out int w, out int h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_position(saucer_window* arg0, out int x, out int y);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_screen* saucer_window_screen(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_hide(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_show(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_close(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_focus(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_start_drag(saucer_window* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_start_resize(saucer_window* arg0, SaucerWindowEdge arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_minimized(saucer_window* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_maximized(saucer_window* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_resizable(saucer_window* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_fullscreen(saucer_window* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_always_on_top(saucer_window* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_click_through(saucer_window* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_icon(saucer_window* arg0, saucer_icon* arg1);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_title(saucer_window* arg0, string arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_background(saucer_window* arg0, byte r, byte g, byte b, byte a);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_decorations(saucer_window* arg0, SaucerWindowDecoration arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_size(saucer_window* arg0, int w, int h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_max_size(saucer_window* arg0, int w, int h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_min_size(saucer_window* arg0, int w, int h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_set_position(saucer_window* arg0, int x, int y);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial nuint saucer_window_on(saucer_window* arg0, SaucerWindowEvent arg1, IntPtr callback, [MarshalAs(UnmanagedType.U1)] bool clearable, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_once(saucer_window* arg0, SaucerWindowEvent arg1, IntPtr callback, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_off(saucer_window* arg0, SaucerWindowEvent arg1, nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_off_all(saucer_window* arg0, SaucerWindowEvent arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_window_native(saucer_window* arg0, nuint arg1, IntPtr arg2, ref nuint arg3);

}
