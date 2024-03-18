﻿using BrainMate.Data.Entities;

namespace BrainMate.Service.Abstracts
{
	public interface IRelativesService
	{
		//	public Task<List<Relatives>> GetRelativesListAsync();
		public IQueryable<Relatives> FilterRelativesPaginatedQueryable();
		public Task<Relatives> GetByIdAsync(int id);
		public Task<Relatives> GetRelativeAsync(int id);
	}
}
