using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Google;
using backend.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace backend
{
    public class Startup
    {
        private string[] roles = new[] { "User", "Employee", "Administrator" };
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        private async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    var newRole = new IdentityRole(role);
                    await roleManager.CreateAsync(newRole);
                }
            }
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opts => 
            {
                opts.AddPolicy("AllowAll", builder => 
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
            });

            // services.AddAuthentication()
            //     .AddGoogle(options => 
            //     {
            //         options.ClientId = AppSettings.appSettings.GoogleClientId;
            //         options.ClientSecret = AppSettings.appSettings.GoogleClientSecret;
            //     });
            // Not sure if these can chain off each other?
            #region AuthRegion
            services.AddAuthentication()
                .AddJwtBearer(options => 
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    // JwtPayload.Deserialize() // easier way of access jwt contents
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:JwtSecret"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents 
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["jacolateChip"];
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddGoogle(options => 
                {
                    options.AccessType = "offline";
                    options.ClientId = AppSettings.appSettings.GoogleClientId;
                    options.ClientSecret = AppSettings.appSettings.GoogleClientSecret;
                    options.SaveTokens = true;
                    // options.ClaimActions
                });
            #endregion
            services.AddDbContext<MyContext>(options => options.UseMySql(Configuration["DbInfo:ConnectionString"]));
            services.AddIdentityCore<User>(opts => opts.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<MyContext>();
            //         .AddUserStore<MyContext>();
            // services.AddDbContextFactory<MyContext>(options => options.UseMySql(Configuration["DbInfo:ConnectionString"]));
            
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            // services.AddAuthorization(options => 
            // {
            //     options.AddPolicy("AdminOnly",policy => policy.AddRequirements(new IsAdminRequirement()));
            //     options.AddPolicy("EmployeeOnly", policy => policy.AddRequirements(new IsEmployeeRequirement()));
            //     options.AddPolicy("UserOnly", policy => policy.AddRequirements(new IsUserRequirement()));
            // })
            // .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            // .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
            //     options => {
            //         options.AccessDeniedPath = new PathString("/api/auth/denied");
            //     });

            // services.AddSingleton<IAuthorizationHandler, IsAdminAuthorizationHandler>();
            // services.AddSingleton<IAuthorizationHandler, IsEmployeeAuthorizationHandler>();
            // services.AddSingleton<IAuthorizationHandler, IsUserAuthorizationHandler>();

            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFurnitureService, FurnitureService>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
