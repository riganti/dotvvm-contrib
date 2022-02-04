using DotVVM.Contrib.Samples.Pages.PagingRepeater;
using DotVVM.Contrib.Samples.Pages.Sample1;
using DotVVM.Contrib.Samples.Pages.Sample2;
using DotVVM.Contrib.Samples.Pages.Sample3;
using DotVVM.Contrib.Samples.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DotVVM.Contrib.Samples
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDotVVM<DotvvmStartup>();

            //Registering viewmodels
            services.AddTransient<Sample1ViewModel>();
            services.AddTransient<Sample2ViewModel>();
            services.AddTransient<Sample3ViewModel>();
            services.AddTransient<PagingRepeaterViewModel>();

            //Registering UI services
            services.AddTransient<ColorService>();
            services.AddTransient<GridService>();
            services.AddTransient<ItemService>();
            services.AddTransient<DataService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
