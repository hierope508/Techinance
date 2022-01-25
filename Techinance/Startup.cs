using BLL;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model;

namespace Techinance
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

            services.AddControllersWithViews();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll",
            //        p => p.AllowAnyOrigin().
            //            AllowAnyHeader().
            //            AllowAnyMethod().
            //            AllowCredentials()
            //            );
            //});

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            string connectionString = Configuration.GetConnectionString("SqlConnectionString");

            DbContextOptionsBuilder<BaseContext> optionsBulder = new DbContextOptionsBuilder<BaseContext>();
            optionsBulder.UseSqlServer(connectionString);
            var options = optionsBulder.Options;

            services.AddScoped(x => new BLLUser(options));
            services.AddScoped(x => new BLLReport(options));

#if DEBUG
            CreateTestData(options);
#endif

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        private async void CreateTestData(DbContextOptions<BaseContext> options)
        {

            BLLUser bLLUser = new BLLUser(options);
            BLLReport bLLReport = new BLLReport(options);
            
            if(bLLUser.GetAll().Result.Count == 0) { 
                User user1 = new User { Name = "Adminstrator", Age = 99, Login = "Adm", Password = "123" };
                User user2 = new User { Name = "Iran Lopes", Age = 26, Login = "iran.santos", Password = "123" };
                User user3 = new User { Name = "Maria Rosef", Age = 42, Login = "maria.r", Password = "123" };
                User user4 = new User { Name = "Jose Silva", Age = 35, Login = "jose.silva", Password = "123" };
                User user5 = new User { Name = "Bernard", Age = 17, Login = "bernard", Password = "123" };

                await bLLUser.Insert(user1);
                await bLLUser.Insert(user2);
                await bLLUser.Insert(user3);
                await bLLUser.Insert(user4);
                await bLLUser.Insert(user5);
            }

            if(bLLReport.GetAll().Result.Count == 0)
            {
                Report report = new Report { Name = "All users", Enabled = true, Query = "SELECT Name, Age, Login FROM Users" };
                Report report1 = new Report { Name = "Users with Ager >= 18 ", Enabled = true, Query = "SELECT Name, Age FROM Users WHERE Age >= 18" };
                Report report2 = new Report { Name = "Users with Ager <= 18", Enabled = true, Query = "SELECT Name, Age FROM Users WHERE Age <= 18" };

                await bLLReport.Insert(report);
                await bLLReport.Insert(report1);
                await bLLReport.Insert(report2);
            }
        }

    }
}
