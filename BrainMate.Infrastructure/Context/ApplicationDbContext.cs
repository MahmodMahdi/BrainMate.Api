using BrainMate.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BrainMate.Infrastructure.Context
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext() { }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
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
			base.OnModelCreating(modelBuilder);
		}
	}
}
