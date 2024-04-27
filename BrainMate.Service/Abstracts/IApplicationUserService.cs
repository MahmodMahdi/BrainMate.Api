using BrainMate.Data.Entities.Identity;

namespace BrainMate.Service.Abstracts;
public interface IApplicationUserService
{
    public Task<string> RegisterAsync(Patient patient, string password);
    public Task<string> CaregiverRegisterAsync(Patient Caregiver, string password);
}