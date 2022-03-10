using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.Contrib.PolicyView.Samples
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);    
        }
    }
}
