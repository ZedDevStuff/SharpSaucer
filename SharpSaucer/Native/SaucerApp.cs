using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate SaucerPolicy SaucerApplicationEventQuit(SaucerApplication* arg0, IntPtr arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerPostCallback(IntPtr arg0);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerRunCallback(SaucerApplication* arg0, IntPtr arg1);

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal unsafe delegate void SaucerFinishCallback(SaucerApplication* arg0, IntPtr arg1);

public struct SaucerScreen { }

public struct SaucerApplication { }

public struct SaucerApplicationOptions { }

internal static unsafe partial class NativeMethods
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_screen_free(SaucerScreen* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_screen_name(SaucerScreen* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_screen_size(SaucerScreen* arg0, out int w, out int h);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_screen_position(SaucerScreen* arg0, out int x, out int y);

    /// <summary>
    ///  @note The application options can be safely free'd after creating an application instance.
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_options_free(SaucerApplicationOptions* arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerApplicationOptions* saucer_application_options_new(string id);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_options_set_argc(SaucerApplicationOptions* arg0, int arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_options_set_argv(SaucerApplicationOptions* arg0, IntPtr[] arg1);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_options_set_quit_on_last_window_closed(SaucerApplicationOptions* arg0, [MarshalAs(UnmanagedType.U1)] bool arg1);

    /// <summary>
    ///  @attention Please call this after @see{saucer_application_run} returned and not in the finish callback or similar.
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_free(SaucerApplication* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerApplication* saucer_application_new(SaucerApplicationOptions* arg0, out int error);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_application_thread_safe(SaucerApplication* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_screens(SaucerApplication* arg0, out SaucerScreen* arg1, out nuint size);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_post(SaucerApplication* arg0, SaucerPostCallback arg1, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_quit(SaucerApplication* arg0);

    /// <summary>
    ///  @note This approximates the run function that uses coroutines. The run callback is called once the application is ready, then `co_await app->finish()` is called internally, afterwards, the finish callback is invoked. @attention You might want to use the loop module instead.
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_application_run(SaucerApplication* arg0, SaucerRunCallback arg1, SaucerFinishCallback arg2, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint saucer_application_on(SaucerApplication* arg0, SaucerApplicationEvent arg1, IntPtr callback, [MarshalAs(UnmanagedType.U1)] bool clearable, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_once(SaucerApplication* arg0, SaucerApplicationEvent arg1, IntPtr callback, IntPtr userdata);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_off(SaucerApplication* arg0, SaucerApplicationEvent arg1, nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_off_all(SaucerApplication* arg0, SaucerApplicationEvent arg1);

    /// <summary>
    ///  @brief Allows to access the stable natives of saucer::application. @param idx The index of the member to return, e.g. `0` to access the `AdwApplication *` of the webkitgtk natives. @param result A pointer to a buffer into which the member will be extracted. @param size The size of the buffer. @note To use this function, call it first with @param {result} being `nullptr`, and @param {size} pointing to a variable that will receive the required buffer size. Then call it again with @param {result} pointing to a buffer with sufficient size. Leave @param {size} unchanged in the second invocation.
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_native(SaucerApplication* arg0, nuint idx, IntPtr result, out nuint size);

    /// <summary>
    ///  @note The returned string does not need to be free'd.
    /// </summary>
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_version();

}
