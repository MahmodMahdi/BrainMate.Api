using BrainMate.Data.Entities.Identity;
using BrainMate.Infrastructure.Context;
using BrainMate.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Service.Abstracts;
using System.Text.Encodings.Web;

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
				var OldUser = await _userManager.FindByEmailAsync(user.Email!);

				// Email is Already Exist
				if (OldUser != null) return "EmailIsExist";

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
				var message = $"Please Confirm your account by <a href='{HtmlEncoder.Default.Encode(returnUrl)}'>Click here</a>.";
				await _emailService.SendEmailAsync(user.Email!, message, "Confirmation Email");
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
				// Caregiver Email is Already Exist
				if (OldUser != null) return "EmailIsExist";
				// check if another caregiver exist with this patient email
				var OldPatient = await _userManager.Users.FirstOrDefaultAsync(x => x.PatientEmail == caregiver.PatientEmail!);
				if (OldPatient != null) return "ThereIsAnotherCaregiverOnThisPatientEmail";
				// check if the phone number is exist (should be Unique)
				var SearchByPhone = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == caregiver.PhoneNumber);
				if (SearchByPhone != null) return "PhoneExist";
				// check if there any email in db equal the entered patient email (access بيتأكد اذا فعلا فيه ايميل لمريض متسجل قبل كده وبيساوي الايميل اللى المرافق عايز ي )
				var ExistUser = await _userManager.Users.AnyAsync(x => x.Email == caregiver.PatientEmail);
				//Create
				if (ExistUser == true)
				{
					// create email to db
					var Result = await _userManager.CreateAsync(caregiver, password!);

					// Failed
					if (!Result.Succeeded) return string.Join(",", Result.Errors.Select(x => x.Description).ToList());
					await _userManager.AddToRoleAsync(caregiver, "Caregiver");
					// Send Confirm Email
					var code = await _userManager.GenerateEmailConfirmationTokenAsync(caregiver);
					//code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
					var requestAccessor = _httpContextAccessor.HttpContext!.Request;
					var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host + _urlHelper
						.Action("ConfirmEmail", "Authentication", new { userId = caregiver.Id, Code = code });
					// send message to email with encrypt code
					var message = $"Please Confirm your account by <a href='{HtmlEncoder.Default.Encode(returnUrl)}'>Click here</a>.";
					await _emailService.SendEmailAsync(caregiver.PatientEmail!, message, "Confirmation Email");
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
