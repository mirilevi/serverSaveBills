
using Bal.Classes;
using Bal.Interfaces;
using Dal.Classes;
using Dal.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SaveBills
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            /*services.AddScoped(typeof(IBillBL), typeof(BillBL));

            services.AddScoped(typeof(IBillDL), typeof(BillDL));*/
            services.AddDbContext<SaveBillsContext>(Options =>
            Options.UseSqlServer("Data Source=.;Initial Catalog=SaveBills;Integrated Security=True"));
            services.AddScoped<IBillDL, BillDL>();
            services.AddScoped<IBillBL, BillBL>();
            services.AddScoped<IUserDL, UserDL>();
            services.AddScoped<IuserBL, UserBL>();

            services.AddSession();
            services.AddTransient<IBillDL, BillDL>();
            services.AddTransient<IBillBL, BillBL>();
            services.AddTransient<IUserDL, UserDL>();
            services.AddTransient<IuserBL, UserBL>();

            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(x => x
                             .AllowAnyOrigin()
                             .AllowAnyMethod()
                             .AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseCors("AllowAll");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}


