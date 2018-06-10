using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotVVM.Contrib.Samples
{
    public class Startup
    {
        public const string AuthenticationScheme = "authenticationScheme";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDotVVM<DotvvmStartup>();

            services.AddAuthentication(AuthenticationScheme)
                .AddCookie(AuthenticationScheme);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedPolicy",
                        policy => policy.RequireAssertion(
                            context => context.User?.Identity?.IsAuthenticated == true));

                options.AddPolicy("InRolePolicy",
                        policy => policy.RequireAssertion(
                            context => context.User?.IsInRole("Role") == true));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);

            // use static files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(env.WebRootPath)
            });
        }
    }
}
