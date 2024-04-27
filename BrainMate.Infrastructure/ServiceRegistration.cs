using BrainMate.Data.Entities.Identity;
using BrainMate.Data.Helpers;
using BrainMate.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace SchoolProject.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<Patient, Role>(
              options =>
              {
                  options.Password.RequireLowercase = true;
                  options.Password.RequireUppercase = true;
                  options.Password.RequireNonAlphanumeric = false;
                  options.Password.RequiredLength = 8;
                  options.Password.RequireDigit = false;

                  options.User.RequireUniqueEmail = true;

              })
                  .AddDefaultUI()
                  .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders()
                  .AddSignInManager<SignInManager<Patient>>();
            //JWT Authentication
            var jwtSettings = new jwtSettings();
            var emailSettings = new EmailSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);
            services.AddSingleton(jwtSettings);
            services.AddSingleton(emailSettings);
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
                      ValidateIssuer = jwtSettings.ValidateIssuer,
                      ValidIssuers = new[] { jwtSettings.Issuer },
                      ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret!)),
                      ValidAudience = jwtSettings.Audience,
                      ValidateAudience = jwtSettings.ValidateAudience,
                      ValidateLifetime = jwtSettings.ValidateLifeTime,
                  };
              });
            #region Swagger Configuration
            services.AddSwaggerGen(swagger =>
            {
                //swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Brain Mate", Version = "v1" });
                swagger.EnableAnnotations();
                swagger.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer dsakfljsa')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {{
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
            }});
            });
            #endregion
            return services;
        }
    }
}
