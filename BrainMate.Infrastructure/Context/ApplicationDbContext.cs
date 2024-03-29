using BrainMate.Data.Entities;
using BrainMate.Data.Entities.Identity;
using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using System.Reflection;
namespace BrainMate.Infrastructure.Context
{
	public class ApplicationDbContext : IdentityDbContext<User, Role, int,
																IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
	{
		private readonly IEncryptionProvider _encryptionProvider;
		public ApplicationDbContext() { }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			_encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
		}
		public DbSet<User> users { get; set; }
		public DbSet<Caregiver> caregivers { get; set; }
		public DbSet<UserRefreshToken> userRefreshTokens { get; set; }
		public DbSet<Patient> patients { get; set; }
		public DbSet<Medicine> medicines { get; set; }
		public DbSet<Relatives> relatives { get; set; }
		public DbSet<Food> foods { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			//modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			modelBuilder.UseEncryption(_encryptionProvider);

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

	}
}
