using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.Cors;
using System.Web.Http;

namespace Loans
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase());

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            var context = app.ApplicationServices.GetService<ApiContext>();
            AddTestData(context);

            app.UseMvc();
        }

        private static void AddTestData(ApiContext context)
        {
            var testLoan1 = new Models.Loan
            {
                Id = 23,
                Customer = "Luke",
                repayAmount = 200f,
                fundAmount = 180f
            };

            var testLoan2 = new Models.Loan
            {
                Id = 50,
                Customer = "Han",
                repayAmount = 250f,
                fundAmount = 220f
            };

            context.Loans.Add(testLoan1);
            context.Loans.Add(testLoan2);
            context.SaveChanges();
        }
    }
}
