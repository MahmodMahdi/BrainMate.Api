using BrainMate.Infrastructure.Interfaces;

namespace BrainMate.Infrastructure.UnitofWork
{
	public interface IUnitOfWork : IDisposable
	{
		IPatientRepository patients { get; }
		IMedicineRepository medicines { get; }
		IRelativesRepository relatives { get; }
		IFoodRepository foods { get; }
		IEventRepository events { get; }
		IRefreshTokenRepository refreshTokens { get; }
		Task<int> CompleteAsync();
		public new void Dispose();
	}
}
