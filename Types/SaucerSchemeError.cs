namespace SharpSaucer.Types;

public enum SaucerSchemeError : short
{
    NotFound = 404,
    Invalid = 400,
    Denied = 401,
    Failed = -1,
}
