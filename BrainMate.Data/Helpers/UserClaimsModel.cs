﻿namespace BrainMate.Data.Helpers
{
	public class UserClaimsModel
	{
		public int Id { get; set; }
		public string? UserName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Role { get; set; }
	}
}