using BrainMate.Core.Features.Authentication.Commands.Models;
using BrainMate.Core.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Authentication.Commands.Validators
{
	public class SignInValidator : AbstractValidator<SignInCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _localizer;
		#endregion
		#region Constructors
		public SignInValidator(IStringLocalizer<SharedResources> localizer)
		{
			_localizer = localizer;
			ApplyValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.Email)
			   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

			RuleFor(x => x.Password)
			   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

		}


		#endregion
	}
}
