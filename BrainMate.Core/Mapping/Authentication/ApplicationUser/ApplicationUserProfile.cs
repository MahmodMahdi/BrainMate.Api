using AutoMapper;

namespace BrainMate.Core.Mapping.Authentication.ApplicationUser
{
	public partial class ApplicationUserProfile : Profile
	{
		public ApplicationUserProfile()
		{
			AddUserMapping();
		}
	}
}
