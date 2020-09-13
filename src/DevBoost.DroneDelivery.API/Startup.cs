using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DevBoost.DroneDelivery.CrossCutting.IOC;
using DevBoost.DroneDelivery.Infrastructure.Swagger;
using System.Diagnostics.CodeAnalysis;
using DevBoost.DroneDelivery.Infrastructure.Security;
using DevBoost.DroneDelivery.Infrastructure.Data.Contexts;

namespace DevBoost.DroneDelivery.API
{
    [ExcludeFromCodeCoverage]

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<DCDroneDelivery>();
            services.Register(Configuration);
            services.SwaggerAdd();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidIssuer = TokenGenerator.TokenConfig.Emissor,
                    ClockSkew = TimeSpan.FromMinutes(30),
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(TokenGenerator.TokenConfig.ObterChave()),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireAudience = true
                    
                };
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            

            app.SwaggerAdd();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
