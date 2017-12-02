using System;
using System.Collections.Generic;
using Couchbase;
using Couchbase.Extensions.Caching;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.Extensions.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NoSQLPOSExample.Infrastructure;

namespace NoSQLPOSExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddCouchbase(config =>
            {
                config.Servers = new List<Uri>
                {
                    new Uri("http://10.111.170.101:8091")
                };
                config.Username = "Administrator";
                config.Password = "password";
            });

            services.AddCouchbaseBucket<IPosBucketProvider>("pos");
            services.AddTransient<IRepository, Repository>();

            //Add the distributed cache for storage
            services.AddDistributedCouchbaseCache("pos", opt => { });

            //Add the session state
            services.AddCouchbaseSession(opt =>
            {
                opt.IdleTimeout = new TimeSpan(0, 0, 10, 0);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSession();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //cleanup the Couchbase dependencies
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                app.ApplicationServices.GetRequiredService<ICouchbaseLifetimeService>().Close();
            });
        }
    }
}
