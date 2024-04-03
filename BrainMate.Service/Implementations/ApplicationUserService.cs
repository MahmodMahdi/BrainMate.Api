using BrainMate.Data.Entities.Identity;
using BrainMate.Infrastructure.Context;
using BrainMate.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Service.Abstracts;
using System.Text;

namespace SchoolProject.Service.Implementations
{
	public class ApplicationUserService : IApplicationUserService
	{
		#region Fields
		private readonly UserManager<User> _userManager;
		// to make me access on request
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IEmailService _emailService;
		private readonly ApplicationDbContext _context;
		private readonly IUrlHelper _urlHelper;
		#endregion

		#region Constructor
		public ApplicationUserService(UserManager<User> userManager,
									   IHttpContextAccessor httpContextAccessor,
										IEmailService emailService,
									   ApplicationDbContext context,
									   IUrlHelper urlHelper)
		{
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
			_emailService = emailService;
			_context = context;
			_urlHelper = urlHelper;
		}
		#endregion

		#region Handle Functions
		public async Task<string> RegisterAsync(User user, string password)
		{
			var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				// If Email is exist
				var Olduser = await _userManager.FindByEmailAsync(user.Email!);

				// Email is Already Exist
				if (Olduser != null) return "EmailIsExist";

				var SearchByPhone = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == user.PhoneNumber);
				if (SearchByPhone != null) return "PhoneExist";

				//Create
				var Result = await _userManager.CreateAsync(user, password!);

				// Failed
				if (!Result.Succeeded) return string.Join(",", Result.Errors.Select(x => x.Description).ToList());
				await _userManager.AddToRoleAsync(user, "User");

				// Send Confirm Email
				var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				var requestAccessor = _httpContextAccessor.HttpContext!.Request;
				var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host + _urlHelper
					.Action("ConfirmEmail", "Authentication", new { userId = user.Id, Code = code });
				var message = $"To Confirm Email Click Link: {returnUrl}";
				await _emailService.SendEmailAsync(user.Email!, message, "Confirm Email");
				await transaction.CommitAsync();
				return "Success";
			}
			catch (Exception)
			{
				await transaction.RollbackAsync();
				return "Failed";
			}
		}
		public async Task<string> CaregiverRegisterAsync(User caregiver, string password)
		{
			var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				// If Email is exist
				var OldUser = await _userManager.FindByEmailAsync(caregiver.Email!);
				// Email is Already Exist
				if (OldUser != null) return "EmailIsExist";
				var OldPatient = await _userManager.FindByEmailAsync(caregiver.PatientEmail!);
				if (OldPatient != null) return "ThereIsAnotherCaregiverOnThisPatientEmail";
				var SearchByPhone = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == caregiver.PhoneNumber);
				if (SearchByPhone != null) return "PhoneExist";

				var ExistUser = await _userManager.Users.AnyAsync(x => x.Email == caregiver.PatientEmail);
				//Create
				if (ExistUser == true)
				{
					var Result = await _userManager.CreateAsync(caregiver, password!);

					// Failed
					if (!Result.Succeeded) return string.Join(",", Result.Errors.Select(x => x.Description).ToList());
					await _userManager.AddToRoleAsync(caregiver, "Caregiver");
					// Send Confirm Email
					var code = await _userManager.GenerateEmailConfirmationTokenAsync(caregiver);
					code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
					var requestAccessor = _httpContextAccessor.HttpContext!.Request;
					var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host + _urlHelper
						.Action("ConfirmEmail", "Authentication", new { userId = caregiver.Id, Code = code });
					var message = $"To Confirm Email Click Link: {returnUrl}";
					await _emailService.SendEmailAsync(caregiver.PatientEmail!, message, "Confirm Email");
					await transaction.CommitAsync();
					return "Success";
				}
				else
				{
					return "This Patient Email is not exist";
				}
			}
			catch (Exception)
			{
				await transaction.RollbackAsync();
				return "Failed";
			}
		}
		#endregion
	}
}
