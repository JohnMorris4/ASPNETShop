using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Infrastructure;
using Shop.Database;
using Shop.UI.Infrastructure;
using Stripe;

namespace Shop.UI
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
            
             services.AddHttpContextAccessor();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["DefaultConnection"]));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.ConfigureApplicationCookie(options => { options.LoginPath = "/Accounts/Login"; });
            
            services.AddAuthorization(options =>
                        {
                            options.AddPolicy("Admin", policy => policy
                                .RequireClaim("Role", "Admin"));
                            //options.AddPolicy("Manager", policy => policy.RequireClaim("Role", "Manager"));
                            options.AddPolicy("Manager", policy => policy
                                .RequireAssertion(context => 
                                    context.User.HasClaim("Role", "Manager") ||
                                    context.User.HasClaim("Role", "Admin")));
                        });
            
            services
                .AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Admin");
                    options.Conventions.AuthorizePage("/Admin/ConfigureUser", "Admin");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSession(
                options =>
                {
                    options.Cookie.Name = "Cart"; 
                    options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
                });

            services.AddTransient<ISessionManager, SessionManager>();
           
            
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            services.AddApplicationServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseAuthentication();
            
            app.UseMvcWithDefaultRoute();
        }
    }
}
