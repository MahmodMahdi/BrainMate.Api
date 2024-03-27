﻿using BrainMate.Data.Entities.Identity;

namespace SchoolProject.Service.Abstracts;
public interface IApplicationUserService
{
	public Task<string> RegisterAsync(User user, string password);
}