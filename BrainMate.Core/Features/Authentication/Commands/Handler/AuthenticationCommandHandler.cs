using BrainMate.Core.Bases;
using BrainMate.Core.Features.Authentication.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities.Identity;
using BrainMate.Data.Responses;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Authentication.Commands.Handler
{
	public class AuthenticationCommandHandler : ResponseHandler,
										IRequestHandler<SignInCommand, Response<JwtAuthenticationResponse>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IAuthenticationService _authenticationService;
		#endregion
		#region Constructor
		public AuthenticationCommandHandler(
								  IStringLocalizer<SharedResources> stringLocalizer,
								  UserManager<User> userManager,
								  SignInManager<User> signInManager,
								  IAuthenticationService authenticationService) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_userManager = userManager;
			_signInManager = signInManager;
			_authenticationService = authenticationService;
		}
		#endregion
		#region Handle Functions
		public async Task<Response<JwtAuthenticationResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
		{
			// Check if user is exist or not 
			var user = await _userManager.FindByEmailAsync(request.Email!);

			// return Email not found
			if (user == null) return BadRequest<JwtAuthenticationResponse>(_stringLocalizer[SharedResourcesKeys.EmailIsNotExist]);
			// try to login
			var Result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);
			////if failed return password is wrong
			if (!Result.Succeeded) return BadRequest<JwtAuthenticationResponse>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);
			// confirm email 
			if (!user.EmailConfirmed) return BadRequest<JwtAuthenticationResponse>(_stringLocalizer[SharedResourcesKeys.EmailNotConfirmed]);
			//// Generate token
			var token = await _authenticationService.GetJWTToken(user);
			// return token
			return Success(token);
		}
		#endregion
	}
}
