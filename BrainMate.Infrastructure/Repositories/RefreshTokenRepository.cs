using BrainMate.Data.Entities.Identity;
using BrainMate.Infrastructure.Context;
using BrainMate.Infrastructure.InfrastructureBases;
using BrainMate.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrainMate.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<PatientRefreshToken>, IRefreshTokenRepository
    {
        #region fields
        private readonly DbSet<PatientRefreshToken> _patientRefreshToken;
        #endregion
        #region Constructors
        public RefreshTokenRepository(ApplicationDbContext _context) : base(_context)
        {
            _patientRefreshToken = _context.Set<PatientRefreshToken>();
        }
        #endregion
    }
}
