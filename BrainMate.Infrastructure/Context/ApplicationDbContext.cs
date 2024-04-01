using BrainMate.Data.Entities;
using BrainMate.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace BrainMate.Infrastructure.Context
{
	public class ApplicationDbContext : IdentityDbContext<User, Role, int,
																IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
	{
		//private readonly IEncryptionProvider? _encryptionProvider;
		public ApplicationDbContext() { }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			//_encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
		}
		public DbSet<User> users { get; set; }
		public DbSet<UserRefreshToken> userRefreshTokens { get; set; }
		public DbSet<Patient> patients { get; set; }
		public DbSet<Medicine> medicines { get; set; }
		public DbSet<Relatives> relatives { get; set; }
		public DbSet<Food> foods { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			//modelBuilder.UseEncryption(_encryptionProvider);

			base.OnModelCreating(modelBuilder);
		}


	}
}
