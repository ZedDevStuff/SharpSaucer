using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SharpSaucer.Types;

namespace SharpSaucer;

/// <summary>
/// Raw P/Invoke bindings for the saucer C bindings library.
/// Maps to the functions declared in the include/saucer/*.h headers.
/// </summary>
public static partial class Bindings
{
    private const string LibName = "saucer-bindings";

    // ══════════════════════════════════════════════
    //  stash.h
    // ══════════════════════════════════════════════

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_stash_data(IntPtr stash);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint saucer_stash_size(IntPtr stash);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_stash_free(IntPtr stash);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_stash_copy(IntPtr stash);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_stash_new_from(IntPtr data, nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_stash_new_view(IntPtr data, nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_stash_new_lazy(SaucerStashLazyCallback callback, IntPtr userdata);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_stash_new_from_str([MarshalAs(UnmanagedType.LPUTF8Str)] string str);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_stash_new_view_str([MarshalAs(UnmanagedType.LPUTF8Str)] string str);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_stash_new_empty();

    // ══════════════════════════════════════════════
    //  url.h
    // ══════════════════════════════════════════════

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_free(IntPtr url);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_url_copy(IntPtr url);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_url_new_parse([MarshalAs(UnmanagedType.LPUTF8Str)] string str, ref int error);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_url_new_from([MarshalAs(UnmanagedType.LPUTF8Str)] string str, ref int error);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_url_new_opts(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string scheme,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string host,
        ref nuint port,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string path);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_string(IntPtr url, IntPtr buffer, ref nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_path(IntPtr url, IntPtr buffer, ref nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_scheme(IntPtr url, IntPtr buffer, ref nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_host(IntPtr url, IntPtr buffer, ref nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_url_port(IntPtr url, ref nuint port);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_user(IntPtr url, IntPtr buffer, ref nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_password(IntPtr url, IntPtr buffer, ref nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_url_native(IntPtr url, nuint idx, IntPtr result, ref nuint size);

    // ══════════════════════════════════════════════
    //  icon.h
    // ══════════════════════════════════════════════

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_icon_empty(IntPtr icon);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_icon_data(IntPtr icon);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_icon_save(IntPtr icon, [MarshalAs(UnmanagedType.LPUTF8Str)] string path);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_icon_free(IntPtr icon);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_icon_copy(IntPtr icon);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_icon_new_from_file([MarshalAs(UnmanagedType.LPUTF8Str)] string path, ref int error);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_icon_new_from_stash(IntPtr stash, ref int error);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_icon_native(IntPtr icon, nuint idx, IntPtr result, ref nuint size);

    // ══════════════════════════════════════════════
    //  navigation.h
    // ══════════════════════════════════════════════

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_navigation_url(IntPtr navigation);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_navigation_new_window(IntPtr navigation);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_navigation_redirection(IntPtr navigation);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_navigation_user_initiated(IntPtr navigation);

    // ══════════════════════════════════════════════
    //  permission.h
    // ══════════════════════════════════════════════

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_permission_request_free(IntPtr request);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_permission_request_copy(IntPtr request);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_permission_request_url(IntPtr request);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SaucerPermissionType saucer_permission_request_type(IntPtr request);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_permission_request_accept(IntPtr request, [MarshalAs(UnmanagedType.U1)] bool accept);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_permission_request_native(IntPtr request, nuint idx, IntPtr result, ref nuint size);

    // ══════════════════════════════════════════════
    //  scheme.h
    // ══════════════════════════════════════════════

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_response_free(IntPtr response);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_scheme_response_new(IntPtr stash, [MarshalAs(UnmanagedType.LPUTF8Str)] string mime);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_response_append_header(
        IntPtr response,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string name,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string value);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_response_set_status(IntPtr response, int status);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_request_free(IntPtr request);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_scheme_request_copy(IntPtr request);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_scheme_request_url(IntPtr request);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_request_method(IntPtr request, IntPtr buffer, ref nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_scheme_request_content(IntPtr request);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_request_headers(IntPtr request, IntPtr buffer, ref nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_executor_free(IntPtr executor);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_scheme_executor_copy(IntPtr executor);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_executor_reject(IntPtr executor, SaucerSchemeError error);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_scheme_executor_accept(IntPtr executor, IntPtr response);

    // ══════════════════════════════════════════════
    //  app.h
    // ══════════════════════════════════════════════

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_screen_free(IntPtr screen);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_screen_name(IntPtr screen);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_screen_size(IntPtr screen, out int w, out int h);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_screen_position(IntPtr screen, out int x, out int y);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_options_free(IntPtr options);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_application_options_new([MarshalAs(UnmanagedType.LPUTF8Str)] string id);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_options_set_argc(IntPtr options, int argc);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_options_set_argv(IntPtr options, IntPtr argv);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_options_set_quit_on_last_window_closed(IntPtr options, [MarshalAs(UnmanagedType.U1)] bool quit);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_free(IntPtr application);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_application_new(IntPtr options, ref int error);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_application_thread_safe(IntPtr application);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_screens(IntPtr application, out IntPtr screens, out nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_post(IntPtr application, SaucerPostCallback callback, IntPtr userdata);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_quit(IntPtr application);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_application_run(
        IntPtr application,
        SaucerRunCallback runCallback,
        SaucerFinishCallback finishCallback,
        IntPtr userdata);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint saucer_application_on(
        IntPtr application,
        SaucerApplicationEvent @event,
        IntPtr callback,
        [MarshalAs(UnmanagedType.U1)] bool clearable,
        IntPtr userdata);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_once(
        IntPtr application,
        SaucerApplicationEvent @event,
        IntPtr callback,
        IntPtr userdata);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_off(IntPtr application, SaucerApplicationEvent @event, nuint id);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_off_all(IntPtr application, SaucerApplicationEvent @event);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_application_native(IntPtr application, nuint idx, IntPtr result, ref nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_version();

    // ══════════════════════════════════════════════
    //  window.h
    // ══════════════════════════════════════════════

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_free(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_window_new(IntPtr application, ref int error);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_window_visible(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_window_focused(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_window_minimized(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_window_maximized(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_window_resizable(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_window_fullscreen(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_window_always_on_top(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_window_click_through(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_title(IntPtr window, IntPtr buffer, ref nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_background(IntPtr window, out byte r, out byte g, out byte b, out byte a);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int saucer_window_decorations(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_size(IntPtr window, out int w, out int h);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_max_size(IntPtr window, out int w, out int h);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_min_size(IntPtr window, out int w, out int h);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_position(IntPtr window, out int x, out int y);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_window_screen(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_hide(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_show(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_close(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_focus(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_start_drag(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_start_resize(IntPtr window, SaucerWindowEdge edge);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_minimized(IntPtr window, [MarshalAs(UnmanagedType.U1)] bool minimized);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_maximized(IntPtr window, [MarshalAs(UnmanagedType.U1)] bool maximized);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_resizable(IntPtr window, [MarshalAs(UnmanagedType.U1)] bool resizable);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_fullscreen(IntPtr window, [MarshalAs(UnmanagedType.U1)] bool fullscreen);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_always_on_top(IntPtr window, [MarshalAs(UnmanagedType.U1)] bool alwaysOnTop);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_click_through(IntPtr window, [MarshalAs(UnmanagedType.U1)] bool clickThrough);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_icon(IntPtr window, IntPtr icon);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_title(IntPtr window, [MarshalAs(UnmanagedType.LPUTF8Str)] string title);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_background(IntPtr window, byte r, byte g, byte b, byte a);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_decorations(IntPtr window, SaucerWindowDecoration decoration);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_size(IntPtr window, int w, int h);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_max_size(IntPtr window, int w, int h);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_min_size(IntPtr window, int w, int h);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_set_position(IntPtr window, int x, int y);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint saucer_window_on(
        IntPtr window,
        SaucerWindowEvent @event,
        IntPtr callback,
        [MarshalAs(UnmanagedType.U1)] bool clearable,
        IntPtr userdata);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_once(
        IntPtr window,
        SaucerWindowEvent @event,
        IntPtr callback,
        IntPtr userdata);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_off(IntPtr window, SaucerWindowEvent @event, nuint id);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_off_all(IntPtr window, SaucerWindowEvent @event);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_window_native(IntPtr window, nuint idx, IntPtr result, ref nuint size);

    // ══════════════════════════════════════════════
    //  webview.h
    // ══════════════════════════════════════════════

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_free(IntPtr options);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_webview_options_new(IntPtr window);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_set_attributes(IntPtr options, [MarshalAs(UnmanagedType.U1)] bool attributes);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_set_persistent_cookies(IntPtr options, [MarshalAs(UnmanagedType.U1)] bool persistent);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_set_hardware_acceleration(IntPtr options, [MarshalAs(UnmanagedType.U1)] bool enabled);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_set_storage_path(IntPtr options, [MarshalAs(UnmanagedType.LPUTF8Str)] string path);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_set_user_agent(IntPtr options, [MarshalAs(UnmanagedType.LPUTF8Str)] string userAgent);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_options_append_browser_flag(IntPtr options, [MarshalAs(UnmanagedType.LPUTF8Str)] string flag);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_free(IntPtr webview);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_webview_new(IntPtr options, ref int error);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_webview_url(IntPtr webview);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr saucer_webview_favicon(IntPtr webview);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_page_title(IntPtr webview, IntPtr buffer, ref nuint size);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_webview_dev_tools(IntPtr webview);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_webview_context_menu(IntPtr webview);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    public static partial bool saucer_webview_force_dark(IntPtr webview);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_background(IntPtr webview, out byte r, out byte g, out byte b, out byte a);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_bounds(IntPtr webview, out int x, out int y, out int w, out int h);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_url(IntPtr webview, IntPtr url);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_url_str(IntPtr webview, [MarshalAs(UnmanagedType.LPUTF8Str)] string url);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_html(IntPtr webview, [MarshalAs(UnmanagedType.LPUTF8Str)] string html);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_dev_tools(IntPtr webview, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_context_menu(IntPtr webview, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_force_dark(IntPtr webview, [MarshalAs(UnmanagedType.U1)] bool forceDark);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_background(IntPtr webview, byte r, byte g, byte b, byte a);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_reset_bounds(IntPtr webview);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_set_bounds(IntPtr webview, int x, int y, int w, int h);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_back(IntPtr webview);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_forward(IntPtr webview);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_reload(IntPtr webview);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_serve(IntPtr webview, [MarshalAs(UnmanagedType.LPUTF8Str)] string url);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_embed(
        IntPtr webview,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string path,
        IntPtr content,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string mime);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_unembed_all(IntPtr webview);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_unembed(IntPtr webview, [MarshalAs(UnmanagedType.LPUTF8Str)] string path);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_execute(IntPtr webview, [MarshalAs(UnmanagedType.LPUTF8Str)] string script);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint saucer_webview_inject(
        IntPtr webview,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string code,
        SaucerScriptTime runAt,
        [MarshalAs(UnmanagedType.U1)] bool noFrames,
        [MarshalAs(UnmanagedType.U1)] bool clearable);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_uninject_all(IntPtr webview);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_uninject(IntPtr webview, nuint id);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_handle_scheme(
        IntPtr webview,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string scheme,
        SaucerSchemeHandler handler,
        IntPtr userdata);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_remove_scheme(IntPtr webview, [MarshalAs(UnmanagedType.LPUTF8Str)] string scheme);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint saucer_webview_on(
        IntPtr webview,
        SaucerWebviewEvent @event,
        IntPtr callback,
        [MarshalAs(UnmanagedType.U1)] bool clearable,
        IntPtr userdata);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_once(
        IntPtr webview,
        SaucerWebviewEvent @event,
        IntPtr callback,
        IntPtr userdata);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_off(IntPtr webview, SaucerWebviewEvent @event, nuint id);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_off_all(IntPtr webview, SaucerWebviewEvent @event);


    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_register_scheme([MarshalAs(UnmanagedType.LPUTF8Str)] string scheme);

    [LibraryImport(LibName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void saucer_webview_native(IntPtr webview, nuint idx, IntPtr result, ref nuint size);
}
