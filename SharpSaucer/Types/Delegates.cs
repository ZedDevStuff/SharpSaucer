using System;
using System.Runtime.InteropServices;

namespace SharpSaucer.Types;

// ──────────────────────────────────────────────
// app.h callbacks
// ──────────────────────────────────────────────

/// <summary>typedef saucer_policy (*saucer_application_event_quit)(saucer_application *, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate SaucerPolicy SaucerApplicationEventQuitCallback(IntPtr application, IntPtr userdata);

/// <summary>typedef void (*saucer_post_callback)(void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerPostCallback(IntPtr userdata);

/// <summary>typedef void (*saucer_run_callback)(saucer_application *, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerRunCallback(IntPtr application, IntPtr userdata);

/// <summary>typedef void (*saucer_finish_callback)(saucer_application *, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerFinishCallback(IntPtr application, IntPtr userdata);

// ──────────────────────────────────────────────
// stash.h callbacks
// ──────────────────────────────────────────────

/// <summary>typedef saucer_stash *(*saucer_stash_lazy_callback)(void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate IntPtr SaucerStashLazyCallback(IntPtr userdata);

// ──────────────────────────────────────────────
// scheme.h callbacks
// ──────────────────────────────────────────────

/// <summary>typedef void (*saucer_scheme_handler)(saucer_scheme_request *, saucer_scheme_executor *, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerSchemeHandler(IntPtr request, IntPtr executor, IntPtr userdata);

// ──────────────────────────────────────────────
// window.h callbacks
// ──────────────────────────────────────────────

/// <summary>typedef void (*saucer_window_event_decorated)(saucer_window *, saucer_window_decoration, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerWindowEventDecoratedCallback(IntPtr window, SaucerWindowDecoration decoration, IntPtr userdata);

/// <summary>typedef void (*saucer_window_event_maximize)(saucer_window *, bool, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerWindowEventMaximizeCallback(IntPtr window, [MarshalAs(UnmanagedType.U1)] bool maximized, IntPtr userdata);

/// <summary>typedef void (*saucer_window_event_minimize)(saucer_window *, bool, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerWindowEventMinimizeCallback(IntPtr window, [MarshalAs(UnmanagedType.U1)] bool minimized, IntPtr userdata);

/// <summary>typedef void (*saucer_window_event_closed)(saucer_window *, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerWindowEventClosedCallback(IntPtr window, IntPtr userdata);

/// <summary>typedef void (*saucer_window_event_resize)(saucer_window *, int, int, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerWindowEventResizeCallback(IntPtr window, int width, int height, IntPtr userdata);

/// <summary>typedef void (*saucer_window_event_focus)(saucer_window *, bool, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerWindowEventFocusCallback(IntPtr window, [MarshalAs(UnmanagedType.U1)] bool focused, IntPtr userdata);

/// <summary>typedef saucer_policy (*saucer_window_event_close)(saucer_window *, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate SaucerPolicy SaucerWindowEventCloseCallback(IntPtr window, IntPtr userdata);

// ──────────────────────────────────────────────
// webview.h callbacks
// ──────────────────────────────────────────────

/// <summary>typedef saucer_status (*saucer_webview_event_permission)(saucer_webview *, saucer_permission_request *, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate SaucerStatus SaucerWebviewEventPermissionCallback(IntPtr webview, IntPtr permissionRequest, IntPtr userdata);

/// <summary>typedef saucer_policy (*saucer_webview_event_fullscreen)(saucer_webview *, bool, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate SaucerPolicy SaucerWebviewEventFullscreenCallback(IntPtr webview, [MarshalAs(UnmanagedType.U1)] bool fullscreen, IntPtr userdata);

/// <summary>typedef void (*saucer_webview_event_dom_ready)(saucer_webview *, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerWebviewEventDomReadyCallback(IntPtr webview, IntPtr userdata);

/// <summary>typedef void (*saucer_webview_event_navigated)(saucer_webview *, saucer_url *, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerWebviewEventNavigatedCallback(IntPtr webview, IntPtr url, IntPtr userdata);

/// <summary>typedef saucer_policy (*saucer_webview_event_navigate)(saucer_webview *, saucer_navigation *, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate SaucerPolicy SaucerWebviewEventNavigateCallback(IntPtr webview, IntPtr navigation, IntPtr userdata);

/// <summary>typedef saucer_status (*saucer_webview_event_message)(saucer_webview *, const char *, size_t, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate SaucerStatus SaucerWebviewEventMessageCallback(IntPtr webview, IntPtr message, nuint size, IntPtr userdata);

/// <summary>typedef void (*saucer_webview_event_request)(saucer_webview *, saucer_url *, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerWebviewEventRequestCallback(IntPtr webview, IntPtr url, IntPtr userdata);

/// <summary>typedef void (*saucer_webview_event_favicon)(saucer_webview *, saucer_icon *, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerWebviewEventFaviconCallback(IntPtr webview, IntPtr icon, IntPtr userdata);

/// <summary>typedef void (*saucer_webview_event_title)(saucer_webview *, const char *, size_t, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerWebviewEventTitleCallback(IntPtr webview, IntPtr title, nuint size, IntPtr userdata);

/// <summary>typedef void (*saucer_webview_event_load)(saucer_webview *, saucer_state, void *);</summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void SaucerWebviewEventLoadCallback(IntPtr webview, SaucerState state, IntPtr userdata);
