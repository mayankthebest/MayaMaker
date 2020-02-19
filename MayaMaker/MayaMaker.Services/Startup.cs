using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MayaMaker.Services.Managers;
using MayaMaker.Services.MessageFactory;
using MayaMaker.Services.Models;
using MayaMaker.Services.Writers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MayaMaker.Services
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
            services.AddDbContext<MayaMakerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MayaMakerContext")));
            services.AddTransient<IMessageFactory, AdtMessageFactory>();
            services.AddTransient<IMessageManager, MessageManager>();
            services.AddTransient<IMessageWriter, MessageWriter>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
