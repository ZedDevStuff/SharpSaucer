using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SharpSaucer;



internal struct saucer_scheme_request { }

public unsafe partial class SaucerSchemeRequest
{
    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_request_free(saucer_scheme_request* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_scheme_request* saucer_scheme_request_copy(saucer_scheme_request* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_url* saucer_scheme_request_url(saucer_scheme_request* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_request_method(saucer_scheme_request* arg0, sbyte* arg1, ref nuint arg2);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial saucer_stash* saucer_scheme_request_content(saucer_scheme_request* arg0);

    [LibraryImport(Consts.LibraryName), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void saucer_scheme_request_headers(saucer_scheme_request* arg0, sbyte* arg1, ref nuint arg2);

}
