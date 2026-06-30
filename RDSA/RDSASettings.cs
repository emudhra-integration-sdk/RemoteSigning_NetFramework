using System;
using System.IO;
using System.Text;

namespace RDSA
{
    internal static class RDSASettings
    {
        internal static string ClientId { get; set; }
        internal static string RDSAURL { get; set; }
        internal static bool IsProxyRequired { get; set; }
        internal static int ProxyPort { get; set; }
        internal static string ProxyIP { get; set; }
        internal static string ProxyUserName { get; set; }
        internal static string ProxyPassword { get; set; }
        internal static bool IsProxyRequireAuth { get; set; }
    }
}
