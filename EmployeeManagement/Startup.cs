using EmployeeManagement.Models;
using EmployeeManagement.Models.Repository;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    public class Startup
    {
        readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            
            //password Complexity
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
            });
            //services.AddSingleton<ICompanyRepository<Employee>, EmployeeRepository>();
            services.AddScoped<ICompanyRepository<Employee>, SQLEmployeeRepository>();
            services.AddMvc(options => {
                options.EnableEndpointRouting = false;
                //glt lih obligatoire ikun mssjel
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });             
            
            services.AddDbContext<AppDbContext>(
                    options => options.UseSqlServer(_configuration.GetConnectionString("EmployeeDbConnection"))
                );

            //services.AddMvc(options => options.EnableEndpointRouting = false);
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
                app.UseExceptionHandler("/ ");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseFileServer();
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "Details", template: "{controller=Employee}/{action=Acceuil}/{id?}");
            });


        }
    }
}
