using BrainMate.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrainMate.Infrastructure.Seeding
{
	public static class RoleSeeding
	{
		public static void SeedAsync(RoleManager<Role> roleManager)
		{
			var roles = roleManager.Roles.CountAsync().GetAwaiter().GetResult();
			if (roles <= 0)
			{
				roleManager.CreateAsync(new Role()
				{
					Name = "Shepherd"
				}).GetAwaiter().GetResult();
				roleManager.CreateAsync(new Role()
				{
					Name = "Patient"
				}).GetAwaiter().GetResult();
			}
		}
	}
}
