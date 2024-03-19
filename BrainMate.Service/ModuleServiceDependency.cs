using BrainMate.Service.Abstracts;
using BrainMate.Service.Implementations;
using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;

namespace BrainMate.Service
{
	public static class ModuleServiceDependency
	{
		public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
		{
			services.AddTransient<IPatientService, PatientService>();
			services.AddTransient<IRelativesService, RelativesService>();
			services.AddTransient<IMedicineService, MedicineService>();
			//services.AddTransient<IAuthenticationService, AuthenticationService>();
			//services.AddTransient<IAuthorizationService, AuthorizationService>();
			//services.AddTransient<IEmailService, EmailService>();
			services.AddTransient<IFileService, FileService>();
			return services;
		}
	}
}
