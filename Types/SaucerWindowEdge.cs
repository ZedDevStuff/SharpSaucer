using System;

namespace SharpSaucer.Types;

[Flags]
public enum SaucerWindowEdge : byte
{
    Top = 1 << 0,
    Bottom = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
    BottomLeft = Bottom | Left,
    BottomRight = Bottom | Right,
    TopLeft = Top | Left,
    TopRight = Top | Right,
}
