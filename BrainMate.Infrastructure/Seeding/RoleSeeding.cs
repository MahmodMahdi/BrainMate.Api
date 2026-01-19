using BrainMate.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrainMate.Infrastructure.Seeding
{
	public static class RoleSeeding
	{
		public static async Task SeedAsync(RoleManager<Role> roleManager)
		{
			int maxRetries = 5;
			for (int i = 0; i < maxRetries; i++)
			{
				try
				{
					// استخدام await الحقيقي بدلاً من GetResult
					if (!await roleManager.Roles.AnyAsync())
					{
						await roleManager.CreateAsync(new Role { Name = "Caregiver", NormalizedName = "CAREGIVER" });
						await roleManager.CreateAsync(new Role { Name = "Patient", NormalizedName = "PATIENT" });
					}
					return; // الخروج بنجاح
				}
				catch (Exception)
				{
					if (i == maxRetries - 1) throw; // إذا فشلت آخر محاولة ارفع الخطأ
					await Task.Delay(3000); // انتظر 3 ثوانٍ قبل المحاولة التالية
				}
			}
		}
	}
}
