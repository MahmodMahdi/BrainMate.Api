using BrainMate.Core.Features.Relatives.Commands.Models;
using BrainMate.Core.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Relatives.Commands.Validators
{
	public class AddRelativeValidator : AbstractValidator<AddRelativeCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _localizer;
		#endregion
		#region Constructors
		public AddRelativeValidator(IStringLocalizer<SharedResources> localizer)
		{
			_localizer = localizer;
			ApplyValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.NameEn)
				.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
				.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);

			RuleFor(x => x.NameAr)
			.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
			.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);
		}
		#endregion
	}
}
