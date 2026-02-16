using System;

namespace SharpSaucer.Types;

[Flags]
public enum SaucerPermissionType : byte
{
    Unknown = 0,
    AudioMedia = 1 << 0,
    VideoMedia = 1 << 1,
    DesktopMedia = 1 << 2,
    MouseLock = 1 << 3,
    DeviceInfo = 1 << 4,
    Location = 1 << 5,
    Clipboard = 1 << 6,
    Notification = 1 << 7,
}
