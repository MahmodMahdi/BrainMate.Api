using BrainMate.Data.Entities.Identity;

namespace SchoolProject.Service.Abstracts;
public interface IApplicationUserService
{
	public Task<string> RegisterAsync(User user, string password);
	public Task<string> CaregiverRegisterAsync(Caregiver caregiver, string password);
}