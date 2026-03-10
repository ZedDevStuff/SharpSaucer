namespace SharpSaucer;

public enum SaucerPolicy
{
    Allow = 0,
    Block = 1,
}

public enum SaucerApplicationEvent
{
    Quit = 0,
}

public enum SaucerPermissionType
{
    Unknown = 0,
    AudioMedia = 1,
    VideoMedia = 2,
    DesktopMedia = 4,
    MouseLock = 8,
    DeviceInfo = 16,
    Location = 32,
    Clipboard = 64,
    Notification = 128,
}

public enum SaucerSchemeError
{
    NotFound = 404,
    Invalid = 400,
    Denied = 401,
    Failed = -1,
}

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

public enum SaucerState
{
    Started = 0,
    Finished = 1,
}

public enum SaucerStatus
{
    Andled = 0,
    Nhandled = 1,
}

public enum SaucerScriptTime
{
    Creation = 0,
    Ready = 1,
}

public enum SaucerWebviewEvent
{
    Permission = 0,
    Fullscreen = 1,
    DomReady = 2,
    Navigated = 3,
    Navigate = 4,
    Message = 5,
    Request = 6,
    Favicon = 7,
    Title = 8,
    Load = 9,
}

public enum SaucerPdfLayout
{
    Portrait = 0,
    Landscape = 1,
}

