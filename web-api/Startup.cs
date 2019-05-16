using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using web.api.Data;
using web.api.Entites;
using web.api.MyDbContext;
using web.api.Repositories;
using web.api.Service;

namespace web.api
{
    public class Startup
    {
        public static IConfiguration configuration { get;private set; }

        public Startup(IConfiguration configuration)
        {
            Startup.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<MyContext>(o => o.UseSqlServer(connectionString));

            services.AddMvc().AddMvcOptions(
                option => {
                    option.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                }
                );
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddTransient<IProductRepository, ProductRepository>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory,MyContext myContext)
        {
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            myContext.EnsureSeedDataForContext();

            //网页使用状态码（出404什么的。。）
            app.UseStatusCodePages();
            app.UseMvc();            
        }
    }
}
