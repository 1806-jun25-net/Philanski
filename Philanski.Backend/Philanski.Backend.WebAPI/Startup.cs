using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Philanski.Backend.DataContext.Models;
using Philanski.Backend.Library.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace Philanski.Backend.WebAPI
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

         


           // List<string> connectionstring = GetDBConnectionString();

            services.AddScoped<IRepository, Repository>();
            services.AddDbContext<PhilanskiManagementSolutionsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("dev_db")));

           services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("identity_db"),
                b => b.MigrationsAssembly("Philanski.Backend.DataContext"))); 

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Password settings (defaults - optional)
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyz" +
                    "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                    "0123456789" +
                    "-._@+";
               
            })
             .AddEntityFrameworkStores<IdentityDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                //  options.Cookie.HttpOnly = false;
                options.CookieSecure = CookieSecurePolicy.None;
                options.Cookie.Name = "PhilanskiApiAuth2";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        ctx.Response.StatusCode = 401; // Unauthorized
                        return Task.FromResult(0);
                    },
                    OnRedirectToAccessDenied = ctx =>
                    {
                        ctx.Response.StatusCode = 403; // Forbidden
                        return Task.FromResult(0);
                    },
                };
            });

            services.AddAuthentication();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            //fix later after host works
           if (env.IsDevelopment())
           {
                app.UseDeveloperExceptionPage();
           }
           else
           {
                app.UseHsts();
           }
            app.UseAuthentication();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();

            //ran once to create roles
          //  CreateUserRoles(services).Wait();
        }
        //private async Task CreateUserRoles(IServiceProvider serviceProvider)
      //  {
         //   var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
         //   var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

          //  IdentityResult roleResult;
            //Adding Admin Role
         //   var roleCheck = await RoleManager.RoleExistsAsync("Admin");
         //   if (!roleCheck)
          //  {
                //create the roles and seed them to the database
          //      roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
           // }
            //Assign Admin role to the main User here we have given our newly registered 
            //login id for Admin management
          //  IdentityUser user = await UserManager.FindByNameAsync("warchief@gmail.com");
           // var User = new IdentityUser();
           // await UserManager.AddToRoleAsync(user, "Admin");
       // }
    }
}
