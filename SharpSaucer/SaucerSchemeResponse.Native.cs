using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;



internal struct saucer_scheme_response { }

public unsafe partial class SaucerSchemeResponse
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_response_free(saucer_scheme_response* arg0);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_scheme_response* saucer_scheme_response_new(saucer_stash* arg0, string mime);

    [LibraryImport(Consts.LibraryName, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_response_append_header(saucer_scheme_response* arg0, string arg1, string arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_response_set_status(saucer_scheme_response* arg0, int arg1);

}
