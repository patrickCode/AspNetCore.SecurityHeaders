using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using static AspNetCore.SecurityHeader.SecurityHeaderConstants;

#pragma warning disable CS0618 // Type or member is obsolete
namespace AspNetCore.SecurityHeader
{
    /// <summary>
    /// Builds the Security Policy
    /// </summary>
    public class SecurityHeaderPolicyBuilder
    {
        private readonly SecurityHeaderPolicy _policy;

        public SecurityHeaderPolicyBuilder()
        {
            _policy = new SecurityHeaderPolicy();
        }

        /// <summary>
        /// Reference: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
        /// Description: Stops browser from sniffing the MIME types and doesn't allow MIME types to be changed, from what has been already set
        /// </summary>
        /// <example>x-content-type-options: nosniff</example>
        /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        public SecurityHeaderPolicyBuilder AddNoSniff()
        {
            _policy.AddHeader(X_CONTENT_TYPE_OPTIONS, NO_SNIFF);
            return this;
        }

        /// <summary>
        /// Reference: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
        /// Description: The content/page cannot be displayed in a frame
        /// </summary>
        /// <example>X-Frame-Deny: DENY</example>
        /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        public SecurityHeaderPolicyBuilder AddFrameDenyOptions()
        {
            _policy.AddHeader(X_FRAME_DENY_OPTIONS, DENY, allowMultiple: false);
            return this;
        }

        /// <summary>
        /// Reference: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
        /// Description: The content/page can displayed in a frame of the same origin
        /// </summary>
        /// <example>X-Frame-Deny: SAMEORIGIN</example>
        /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        public SecurityHeaderPolicyBuilder AddFrameDenyOptionsForSameOrigin()
        {
            _policy.AddHeader(X_FRAME_DENY_OPTIONS, SAMEORIGIN, allowMultiple: false);
            return this;
        }

        /// <summary>
        /// Reference: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
        /// Description: The content/page can be displayed in a frame with the given origin URI.
        /// </summary>
        /// <example>X-Frame-Deny: Allow-From <see cref="System.Uri"/></example>
        /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        [Obsolete("Not supported in modern browsers")]
        public SecurityHeaderPolicyBuilder AddFrameDenyOptionWithAllowOrigins(string uri)
        {
            _policy.AddHeader(X_FRAME_DENY_OPTIONS, $"{ALLOW_FROM} {uri}", allowMultiple: false);
            return this;
        }

        /// <summary>
        /// Reference: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-XSS-Protection
        /// Description: Enables XSS filtering. If a cross-site scripting attack is detected, the browser will sanitize the page.
        /// </summary>
        /// <example>X-XSS-Protection: 1</example>
        /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        public SecurityHeaderPolicyBuilder EnableXssProtection()
        {
            _policy.AddHeader(X_XSS_PROTECTION, XSS_ENABLED, allowMultiple: false);
            return this;
        }

        /// <summary>
        /// Reference: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-XSS-Protection
        /// Description: Disables XSS filtering.
        /// </summary>
        /// <example>X-XSS-Protection: 0</example>
        /// <returns></returns>
        public SecurityHeaderPolicyBuilder DisableXssProtection()
        {
            _policy.AddHeader(X_XSS_PROTECTION, XSS_DISABLED, allowMultiple: false);
            return this;
        }

        /// <summary>
        /// Reference: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-XSS-Protection
        /// Description: Enabled XSS filtering. If a cross-site scripting attack is detected, the browser will stop the page from rendering.
        /// </summary>
        /// <example>X-XSS-Protection: 1; mode=block;</example>
        /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        public SecurityHeaderPolicyBuilder EnableBlockingXssProtection()
        {
            _policy.AddHeader(X_XSS_PROTECTION, XSS_BLOCK, allowMultiple: false);
            return this;
        }

        /// <summary>
        /// Reference: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-XSS-Protection
        /// Description: Enabled XSS filtering. If a cross-site scripting attack is detected, the browser sanitize the page and report the violation to the given reporting URI.
        /// Note: Works only in Chromium
        /// </summary>
        /// <param name="reportingUri">URI where a XSS violation is reported</param>
        /// <example>X-XSS-Protection: 1; report=<see cref="System.Uri"/>;</example>
        /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        public SecurityHeaderPolicyBuilder EnableReportXssProtection(string reportingUri)
        {
            _policy.AddHeader(X_XSS_PROTECTION, $"{XSS_REPORT} {reportingUri}", allowMultiple: false);
            return this;
        }

        /// <summary>
        /// Reference: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Strict-Transport-Security
        /// Description: Indicates to the broeser that the site should be accessed only using HTTPS
        /// </summary>
        /// <param name="expirationDuration">Duration, in seconds, how long the browser should remember that the site needs to be accessed via HTTPS</param>
        /// <example>Strict-Transport-Security: max-age=expire-time</example>
        /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        public SecurityHeaderPolicyBuilder AddMaxAge(int expirationDuration)
        {
            var maxAgeValue = string.Format(MAX_AGE, expirationDuration);
            _policy.AddHeader(STRICT_TRANSPORT_SECURITY, maxAgeValue, allowMultiple: false);
            return this;
        }

        /// <summary>
        /// Reference: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Strict-Transport-Security
        /// Description: Indicates to the broeser that the site, and its subdomains, should be accessed only using HTTPS
        /// </summary>
        /// <param name="expirationDuration">Duration, in seconds, how long the browser should remember that the site needs to be accessed via HTTPS</param>
        /// <example>Strict-Transport-Security: max-age=expire-time; includeSubDomains</example>
        /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        public SecurityHeaderPolicyBuilder AddMaxAgeWithSubDomain(int expirationDuration)
        {
            var maxAgeValue = string.Format(MAX_AGE_INCLUDE_SUBDOMAINS, expirationDuration);
            _policy.AddHeader(STRICT_TRANSPORT_SECURITY, maxAgeValue, allowMultiple: false);
            return this;
        }

        /// <summary>
        /// Reference: https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Strict-Transport-Security
        /// Description: Indicates to the broeser that the site should be accesses via HTTPS (with 0 expiration duration)
        // </summary>
        /// <example>Strict-Transport-Security: max-age=0</example>
        /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        public SecurityHeaderPolicyBuilder AddNoCache()
        {
            var maxAgeValue = string.Format(MAX_AGE, 0);
            _policy.AddHeader(STRICT_TRANSPORT_SECURITY, maxAgeValue, allowMultiple: false);
            return this;
        }

        /// <summary>
        /// Adds custom security header (not part of the builder) to Response
        /// </summary>
        /// <param name="key">Header Key</param>
        /// <param name="value">Header Value</param>
        /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        public SecurityHeaderPolicyBuilder AddCustomSecurityHeader(string key, string value)
        {
            _policy.AddHeader(key, value);
            return this;
        }

        /// <summary>
        /// Removes the X-Powered-By header. The header can be used to detect the server technology, ca be used by attackers to launch technology-specific attacks
        /// </summary>
        /// <example>X-Powered-By: ASP.NET</example>
        /// /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        public SecurityHeaderPolicyBuilder RemovePoweredBy()
        {
            _policy.RemoveHeader(X_POWERED_BY);
            return this;
        }

        /// <summary>
        /// Removes the server header. The header can be used to detect the Web Server type, can be used by attackers to launch web server specific targets
        /// </summary>
        /// <example>Server: Microsoft-IIS/10.0</example>
        /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        public SecurityHeaderPolicyBuilder RemoveServer()
        {
            _policy.RemoveHeader(SERVER);
            return this;
        }

        /// <summary>
        /// Remove custom (insecure) headers from the response
        /// </summary>
        /// <param name="customHeaderKey">Custom header to be removed</param>
        /// <returns>The current instance of <see cref="SecurityHeaderPolicyBuilder"/></returns>
        public SecurityHeaderPolicyBuilder RemoveCustomHeader(string customHeaderKey)
        {
            _policy.RemoveHeader(customHeaderKey);
            return this;
        }


        /// <summary>
        /// Builds the Security Policy from <see cref="IConfiguration"/>
        /// </summary>
        /// <param name="configuration">ASP.NET Core Configuration</param>
        /// <returns cref="SecurityHeaderPolicyBuilder"></returns>
        public SecurityHeaderPolicyBuilder AddConfiguration(IConfiguration configuration)
        {
            if (configuration.GetValue(ConfigurationConstants.NO_SNIFF, defaultValue: string.Empty).Equals(ENABLED, System.StringComparison.InvariantCultureIgnoreCase))
                AddNoSniff();

            var frameOptions = configuration.GetValue<string>(ConfigurationConstants.FRAME_OPTIONS);
            if (!string.IsNullOrWhiteSpace(frameOptions))
            {
                if (frameOptions.ToLowerInvariant() == DENY.ToLowerInvariant())
                    AddFrameDenyOptions();
                else if (frameOptions.ToLowerInvariant() == SAMEORIGIN.ToLowerInvariant())
                    AddFrameDenyOptionsForSameOrigin();
                else

                    AddFrameDenyOptionWithAllowOrigins(frameOptions);
            }

            var xssOptions = configuration.GetValue<string>(ConfigurationConstants.XSS);
            if (!string.IsNullOrWhiteSpace(xssOptions))
            {
                if (xssOptions.ToLowerInvariant() == XSS_ENABLED.ToLowerInvariant())
                    EnableXssProtection();
                else if (xssOptions.ToLowerInvariant() == XSS_DISABLED.ToLowerInvariant())
                    DisableXssProtection();
                else if (xssOptions.ToLowerInvariant() == XSS_BLOCK.ToLowerInvariant())
                    EnableBlockingXssProtection();
                else
                    EnableReportXssProtection(xssOptions);
            }

            if (configuration.GetValue(ConfigurationConstants.POWERED_BY, defaultValue: string.Empty).Equals(DISABLED.ToLowerInvariant(), System.StringComparison.InvariantCultureIgnoreCase))
                RemovePoweredBy();

            if (configuration.GetValue(ConfigurationConstants.SERVER, defaultValue: string.Empty).Equals(DISABLED.ToLowerInvariant(), System.StringComparison.InvariantCultureIgnoreCase))
                RemoveServer();

            var extraHeaders = configuration.GetSection(ConfigurationConstants.EXTRA).GetChildren();
            if (extraHeaders != null && extraHeaders.Any())
            {
                foreach (var extraHeader in extraHeaders)
                {
                    if (!string.IsNullOrWhiteSpace(extraHeader.Key) && !(string.IsNullOrWhiteSpace(extraHeader.Value)))
                        AddCustomSecurityHeader(extraHeader.Key, extraHeader.Value);
                }
            }

            var removedHeaders = configuration.GetValue<string>(ConfigurationConstants.REMOVED);
            if (!string.IsNullOrWhiteSpace(removedHeaders))
            {
                foreach (var removedHeader in removedHeaders.Split(','))
                {
                    if (!string.IsNullOrWhiteSpace(removedHeader))
                        RemoveCustomHeader(removedHeader);
                }
            }
            return this;
        }

        /// <summary>
        /// Returns the built <see cref="SecurityHeaderPolicy"/>
        /// </summary>
        /// <returns cref="SecurityHeaderPolicy">Built policy</returns>
        public SecurityHeaderPolicy Build()
        {
            return _policy;
        }
    }
}
#pragma warning restore CS0618 // Type or member is obsolete
