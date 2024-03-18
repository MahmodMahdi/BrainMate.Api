﻿using BrainMate.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace BrainMate.Service.Abstracts
{
	public interface IPatientService
	{
		public Task<Patient> GetPatientById(int id);
		public Task<Patient> GetPatient(int id);
		public Task<string> AddAsync(Patient patient, IFormFile file);
		public Task<string> EditAsync(Patient patient, IFormFile file);
	}
}
