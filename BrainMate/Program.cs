using BrainMate.Core;
using BrainMate.Core.Middleware;
using BrainMate.Data.Entities.Identity;
using BrainMate.Data.Helpers;
using BrainMate.Infrastructure;
using BrainMate.Infrastructure.Context;
using BrainMate.Infrastructure.Seeding;
using BrainMate.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolProject.Infrastructure;
using Serilog;
using System.Globalization;

namespace BrainMate
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new DateConverter());
			});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			#region Context
			//Connection to SQL Server 
			var DB = builder.Configuration.GetConnectionString("DB");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(DB);
			});
			#endregion

			#region Dependecy Injection
			builder.Services.AddInfrastructureDependencies()
				.AddServiceDependencies()
				.AddRegistration(builder.Configuration)
				.AddCoreDependencies();
			#endregion
			#region Localization
			builder.Services.AddControllersWithViews();
			builder.Services.AddLocalization(opt =>
			{
				opt.ResourcesPath = "";
			});

			builder.Services.Configure<RequestLocalizationOptions>(options =>
			{
				List<CultureInfo> supportedCultures = new List<CultureInfo>
				{
						 new CultureInfo("en-US"),
						 new CultureInfo("de-DE"),
						 new CultureInfo("fr-FR"),
						 new CultureInfo("ar-EG")
				};

				options.DefaultRequestCulture = new RequestCulture("ar-EG");
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;
			});


			#endregion
			#region Cors
			var MyCors = "MyCors";
			builder.Services.AddCors(option =>
			option.AddPolicy(name: MyCors, policy =>
			{
				// when i need to specify
				//policy.WithOrigins("http://example.com");
				policy.AllowAnyOrigin();
				policy.AllowAnyHeader();
				policy.AllowAnyMethod();
			}));
			#endregion
			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

			builder.Services.AddTransient<IUrlHelper>(x =>
			{
				var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
				var factory = x.GetRequiredService<IUrlHelperFactory>();
				return factory.GetUrlHelper(actionContext!);
			});

			//Serilog Configure
			Log.Logger = new LoggerConfiguration()
						  .ReadFrom.Configuration(builder.Configuration).CreateLogger();

			builder.Services.AddSerilog();

			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					// 1. الوصول لـ DbContext وعمل Migration تلقائي
					var context = services.GetRequiredService<ApplicationDbContext>();
					if (context.Database.GetPendingMigrations().Any())
					{
						await context.Database.MigrateAsync();
					}

					// 2. استدعاء الـ Seed للأدوار الذي كتبناه سابقاً
					var roleManager = services.GetRequiredService<RoleManager<Role>>();
					await RoleSeeding.SeedAsync(roleManager);
				}
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "حدث خطأ أثناء إعداد قاعدة البيانات.");
				}
			}
			// Configure the HTTP request pipeline.
			//if (app.Environment.IsDevelopment())
			//{
			app.UseSwagger();
			app.UseSwaggerUI();
			//}

			#region Localization Middleware
			var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>()!;
			app.UseRequestLocalization(options.Value);
			#endregion


			app.UseHttpsRedirection();
			app.UseMiddleware<ErrorHandlerMiddleware>();

			app.UseCors(MyCors);
			app.UseStaticFiles();

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
