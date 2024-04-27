using BrainMate.Data.Entities.Identity;
using Microsoft.AspNetCore.Http;

namespace BrainMate.Service.Abstracts
{
    public interface ICaregiverService
    {
        public Task<Patient> GetCaregiverByIdAsync(int id);
        public Task<Patient> GetCaregiverAsync(int id);
        public Task<string> UpdateAsync(Patient caregiver, IFormFile file);
        public Task<string> DeleteAsync(Patient caregiver);
    }
}
