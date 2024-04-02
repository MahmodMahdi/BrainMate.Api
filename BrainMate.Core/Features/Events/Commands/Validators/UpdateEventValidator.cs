using BrainMate.Core.Features.Events.Commands.Models;
using BrainMate.Core.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Events.Commands.Validators
{
	public class UpdateEventValidator : AbstractValidator<UpdateEventCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _localizer;

		#endregion
		#region Constructors
		public UpdateEventValidator(IStringLocalizer<SharedResources> localizer)
		{
			_localizer = localizer;
			ApplyValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.Id)
				.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

			RuleFor(x => x.TaskEn)
				.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
				.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);

			RuleFor(x => x.TaskAr)
				.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
				.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);
		}
		#endregion
	}
}
