using BrainMate.Core.Features.Authentication.Queries.Dtos;
using BrainMate.Core.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Authentication.Commands.Validators
{
	public class ConfirmResetPasswordQueryValidator : AbstractValidator<ConfirmResetPasswordQuery>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _localizer;
		#endregion
		#region Constructors
		public ConfirmResetPasswordQueryValidator(IStringLocalizer<SharedResources> localizer)
		{
			_localizer = localizer;
			ApplyValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.Code)
			   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
			RuleFor(x => x.Email)
			   .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			   .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
		}


		#endregion
	}
}
