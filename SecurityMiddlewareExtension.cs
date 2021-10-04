using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace AspNetCore.SecurityHeader
{
    /// <summary>
    /// Extension for adding the security middleware in ASP.NET Core pipeline
    /// </summary>
    /// <remarks>Inspired from https://github.com/andrewlock/blog-examples/tree/master/adding-default-security-headers</remarks>
    public static class SecurityMiddlewareExtension
    {
        /// <summary>
        /// Adds the security middleware by creating the Security Policy from configuration
        /// </summary>
        /// <param name="app" cref="IApplicationBuilder">Application Request Pipeline</param>
        /// <param name="configuration" cref="IConfiguration">Application configuration</param>
        /// <exception cref="ArgumentNullException">When <see cref="IConfiguration" /> is null</exception>
        public static void UseSecurityMiddleware(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var builder = new SecurityHeaderPolicyBuilder();
            builder.AddConfiguration(configuration);
            UseSecurityMiddleware(app, builder);
        }

        /// <summary>
        /// Adds the security middleware by creating the Security Policy from <see cref="SecurityHeaderPolicyBuilder">policy builder</see>
        /// </summary>
        /// <param name="app" cref="IApplicationBuilder">Application Request Pipeline</param>
        /// <param name="builder" cref="SecurityHeaderPolicyBuilder">Security policy builder</param>
        public static void UseSecurityMiddleware(this IApplicationBuilder app, SecurityHeaderPolicyBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            UseSecurityMiddleware(app, builder.Build());
        }

        /// <summary>
        /// Adds the security middleware from the <see cref="SecurityHeaderPolicy">policy</see>
        /// </summary>
        /// <param name="app" cref="IApplicationBuilder">Application Request Pipeline</param>
        /// <param name="policy" cref="SecurityHeaderPolicy">Security policy</param>
        public static void UseSecurityMiddleware(this IApplicationBuilder app, SecurityHeaderPolicy policy)
        {
            if (policy == null)
                throw new ArgumentNullException(nameof(policy));

            app.UseMiddleware<SecurityHeaderMiddleware>(policy);
        }
    }
}
