using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles();
            services.AddMvc();
        }
        public void Configure(IApplicationBuilder app)
        {

            app.UseStaticFiles();
            app.UseMvc(

                r =>
                {
                    r.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}"

                        );
                });

        }
    }
}
