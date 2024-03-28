﻿using BrainMate.Core.Features.ApplicationUser.Models;
using BrainMate.Data.Entities.Identity;

namespace BrainMate.Core.Mapping.Authentication.ApplicationUser
{
	public partial class ApplicationUserProfile
	{
		public void AddUserMapping()
		{
			CreateMap<RegisterCommand, User>()
			.ForMember(dest => dest.Email, op => op.MapFrom(src => src.Email))
			.ForMember(dest => dest.UserName, op => op.MapFrom(src => src.Email))
			.ForMember(dest => dest.PhoneNumber, op => op.MapFrom(src => src.Phone));
		}
	}
}
