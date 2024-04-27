using BrainMate.Data.Entities;
using BrainMate.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
namespace BrainMate.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<Patient, Role, int,
                                                    IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Patient> patients { get; set; }
        public DbSet<PatientRefreshToken> patientRefreshTokens { get; set; }
        public DbSet<Event> events { get; set; }
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
            base.OnModelCreating(modelBuilder);
        }
    }
}
