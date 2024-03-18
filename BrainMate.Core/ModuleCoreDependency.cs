using BrainMate.Core.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BrainMate.Core
{
	public static class ModuleCoreDependency
	{
		public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
		{
			// Configuration of (mediator) => handle requests

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			// Get Validators
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			// 
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
			return services;
		}
	}
}
