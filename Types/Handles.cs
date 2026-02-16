using System;
using System.Runtime.InteropServices;

namespace SharpSaucer.Types;

/// <summary>
/// Represents an opaque handle to a native saucer object.
/// Used as a type-safe wrapper around native pointers for all saucer opaque structs.
/// </summary>
/// <remarks>
/// Each specific handle type (e.g. <see cref="SaucerApplication"/>, <see cref="SaucerWindow"/>)
/// wraps a raw <see cref="IntPtr"/> to the corresponding native struct.
/// These handles should be freed through their respective free functions.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
public struct SaucerScreen
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerScreen(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerScreen s) => s.Handle;
    public static implicit operator SaucerScreen(IntPtr p) => new(p);
}

[StructLayout(LayoutKind.Sequential)]
public struct SaucerApplication
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerApplication(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerApplication s) => s.Handle;
    public static implicit operator SaucerApplication(IntPtr p) => new(p);
}

[StructLayout(LayoutKind.Sequential)]
public struct SaucerApplicationOptions
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerApplicationOptions(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerApplicationOptions s) => s.Handle;
    public static implicit operator SaucerApplicationOptions(IntPtr p) => new(p);
}

[StructLayout(LayoutKind.Sequential)]
public struct SaucerWindow
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerWindow(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerWindow s) => s.Handle;
    public static implicit operator SaucerWindow(IntPtr p) => new(p);
}

[StructLayout(LayoutKind.Sequential)]
public struct SaucerWebview
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerWebview(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerWebview s) => s.Handle;
    public static implicit operator SaucerWebview(IntPtr p) => new(p);
}

[StructLayout(LayoutKind.Sequential)]
public struct SaucerWebviewOptions
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerWebviewOptions(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerWebviewOptions s) => s.Handle;
    public static implicit operator SaucerWebviewOptions(IntPtr p) => new(p);
}

[StructLayout(LayoutKind.Sequential)]
public struct SaucerIcon
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerIcon(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerIcon s) => s.Handle;
    public static implicit operator SaucerIcon(IntPtr p) => new(p);
}

[StructLayout(LayoutKind.Sequential)]
public struct SaucerStash
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerStash(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerStash s) => s.Handle;
    public static implicit operator SaucerStash(IntPtr p) => new(p);
}

[StructLayout(LayoutKind.Sequential)]
public struct SaucerUrl
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerUrl(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerUrl s) => s.Handle;
    public static implicit operator SaucerUrl(IntPtr p) => new(p);
}

[StructLayout(LayoutKind.Sequential)]
public struct SaucerNavigation
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerNavigation(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerNavigation s) => s.Handle;
    public static implicit operator SaucerNavigation(IntPtr p) => new(p);
}

[StructLayout(LayoutKind.Sequential)]
public struct SaucerPermissionRequest
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerPermissionRequest(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerPermissionRequest s) => s.Handle;
    public static implicit operator SaucerPermissionRequest(IntPtr p) => new(p);
}

[StructLayout(LayoutKind.Sequential)]
public struct SaucerSchemeExecutor
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerSchemeExecutor(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerSchemeExecutor s) => s.Handle;
    public static implicit operator SaucerSchemeExecutor(IntPtr p) => new(p);
}

[StructLayout(LayoutKind.Sequential)]
public struct SaucerSchemeRequest
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerSchemeRequest(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerSchemeRequest s) => s.Handle;
    public static implicit operator SaucerSchemeRequest(IntPtr p) => new(p);
}

[StructLayout(LayoutKind.Sequential)]
public struct SaucerSchemeResponse
{
    public IntPtr Handle;

    public bool IsNull => Handle == IntPtr.Zero;

    public SaucerSchemeResponse(IntPtr handle) => Handle = handle;
    public static implicit operator IntPtr(SaucerSchemeResponse s) => s.Handle;
    public static implicit operator SaucerSchemeResponse(IntPtr p) => new(p);
}
