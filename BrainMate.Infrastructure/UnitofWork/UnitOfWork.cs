using BrainMate.Infrastructure.Context;
using BrainMate.Infrastructure.Interfaces;
using BrainMate.Infrastructure.Repositories;

namespace BrainMate.Infrastructure.UnitofWork
{
	public class UnitOfWork : IUnitOfWork
	{
		public readonly ApplicationDbContext _context;
		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			patients = new PatientRepository(_context);
			relatives = new RelativesRepository(_context);
			medicines = new MedicineRepository(_context);
			foods = new FoodRepository(_context);
			events = new EventRepository(_context);
			refreshTokens = new RefreshTokenRepository(_context);
		}
		public IPatientRepository patients { get; private set; }

		public IMedicineRepository medicines { get; private set; }

		public IRelativesRepository relatives { get; private set; }

		public IFoodRepository foods { get; private set; }

		public IEventRepository events { get; private set; }

		public IRefreshTokenRepository refreshTokens { get; private set; }

		public Task<int> CompleteAsync()
		{
			return _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
