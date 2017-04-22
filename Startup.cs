using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using ProfessionalProfile.Models;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace ProfessionalProfile
{    
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.AddMvc();
            // services.Configure<MySqlOptions>(Configuration.GetSection("DBInfo"));
            services.AddDbContext<MainContext>(options => options.UseMySQL(Configuration["DBInfo:ConnectionString"]));
            services.AddScoped<DbConnector>();
        }
         
        public void Configure(IApplicationBuilder app, ILoggerFactory logger)
        {
            logger.AddConsole();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();

            app.UseSession();
            app.UseMvc();
            
        }
    }
}
