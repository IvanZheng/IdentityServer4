using System;
using System.Reflection;
using IdentityServer.Managers;
using IdentityServer.Models;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IdentityServerAspNetIdentity
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContextPool<ApplicationDbContext>(options =>
                                                            options.UseSqlServer(connectionString));

            services.AddScoped<TenantManager>();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddRoleManager<RoleManager>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.Replace(new ServiceDescriptor(typeof(IRoleValidator<ApplicationRole>), typeof(RoleValidator), ServiceLifetime.Scoped));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.Configure<IISOptions>(iis =>
            //{
            //    iis.AuthenticationDisplayName = "Windows";
            //    iis.AutomaticAuthentication = false;
            //});

            var builder = services.AddIdentityServer(options =>
                                  {
                                      options.Events.RaiseErrorEvents = true;
                                      options.Events.RaiseInformationEvents = true;
                                      options.Events.RaiseFailureEvents = true;
                                      options.Events.RaiseSuccessEvents = true;
                                  })
                                  // this adds the config data from DB (clients, resources)
                                  .AddConfigurationStore(options =>
                                  {
                                      options.ConfigureDbContext = b =>
                                          b.UseSqlServer(connectionString,
                                                         sql => sql.MigrationsAssembly(migrationsAssembly));
                                  })
                                  // this adds the operational data from DB (codes, tokens, consents)
                                  .AddOperationalStore(options =>
                                  {
                                      options.ConfigureDbContext = b =>
                                          b.UseSqlServer(connectionString,
                                                         sql => sql.MigrationsAssembly(migrationsAssembly));

                                      // this enables automatic token cleanup. this is optional.
                                      options.EnableTokenCleanup = true;
                                  })
                                  .AddAspNetIdentity<ApplicationUser>();

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        // register your IdentityServer with Google at https://console.developers.google.com
                        // enable the Google+ API
                        // set the redirect URI to http://localhost:5000/signin-google
                        options.ClientId = "copy client ID from Google here";
                        options.ClientSecret = "copy client secret from Google here";
                    });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}