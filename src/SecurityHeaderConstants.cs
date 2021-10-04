namespace AspNetCore.SecurityHeader
{
    public static class SecurityHeaderConstants
    {
        public static string ENABLED = "Enabled";
        public static string DISABLED = "Disabled";

        // No Sniff
        public static string X_CONTENT_TYPE_OPTIONS = "X-Content-Type-Options";
        public static string NO_SNIFF = "nosniff";

        // Server
        public static string X_POWERED_BY = "X-Powered-By";
        public static string SERVER = "Server";

        // Frame Options
        public static string X_FRAME_DENY_OPTIONS = "X-Frame-Deny";
        public static string DENY = "DENY";
        public static string SAMEORIGIN = "SAMEORIGIN";
        public static string ALLOW_FROM = "Allow-From ";

        // XSS
        public static string X_XSS_PROTECTION = "X-XSS-Protection";
        public static string XSS_ENABLED = "1";
        public static string XSS_DISABLED = "0";
        public static string XSS_BLOCK = "1; mode=block";
        public static string XSS_REPORT = "1; report=";

        // Cache
        public static string STRICT_TRANSPORT_SECURITY = "Strict-Transport-Security";
        public static string MAX_AGE = "max-age={0}";
        public static string MAX_AGE_INCLUDE_SUBDOMAINS = "max-age={0}; includeSubDomains";

        public static class ConfigurationConstants
        {
            public static string NO_SNIFF = "Security:Headers:NoSniff";
            public static string FRAME_OPTIONS = "Security:Headers:FrameOptions";
            public static string XSS = "Security:Headers:XSS";
            public static string POWERED_BY = "Security:Headers:PoweredBy";
            public static string SERVER = "Security:Headers:Server";
            public static string EXTRA = "Security:Headers:Custom:Extra";
            public static string REMOVED = "Security:Headers:Custom:Removed";
        }
    }
}
