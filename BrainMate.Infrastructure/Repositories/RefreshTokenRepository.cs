using BrainMate.Data.Entities.Identity;
using BrainMate.Infrastructure.Context;
using BrainMate.Infrastructure.InfrastructureBases;
using BrainMate.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BrainMate.Infrastructure.Repositories
{
	public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
	{
		#region fields
		private readonly DbSet<UserRefreshToken> _userRefreshToken;
		#endregion
		#region Constructors
		public RefreshTokenRepository(ApplicationDbContext _context) : base(_context)
		{
			_userRefreshToken = _context.Set<UserRefreshToken>();
		}
		#endregion
	}
}
