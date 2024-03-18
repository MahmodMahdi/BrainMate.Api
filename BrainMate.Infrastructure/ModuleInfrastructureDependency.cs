using BrainMate.Infrastructure.InfrastructureBases;
using BrainMate.Infrastructure.Interfaces;
using BrainMate.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace BrainMate.Infrastructure
{
	public static class ModuleInfrastructureDependency
	{
		public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
		{
			services.AddTransient<IPatientRepository, PatientRepository>();
			//services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
			services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
			return services;
		}

	}
}
