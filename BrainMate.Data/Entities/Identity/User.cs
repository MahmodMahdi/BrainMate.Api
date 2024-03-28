using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;

namespace BrainMate.Data.Entities.Identity
{
	public class User : IdentityUser<int>
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public override string? UserName { get; set; }
		public string? Address { get; set; }
		[EncryptColumn]
		public string? Code { get; set; }
		public virtual ICollection<UserRefreshToken>? UserRefreshTokens { get; set; }
	}
}
