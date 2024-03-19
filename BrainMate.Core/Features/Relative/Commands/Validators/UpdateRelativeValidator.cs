using BrainMate.Core.Features.Relative.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Relative.Commands.Validators
{
	public class UpdateRelativeValidator : AbstractValidator<UpdateRelativeCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _localizer;
		private readonly IRelativesService _relativesService;
		#endregion
		#region Constructors
		public UpdateRelativeValidator(IStringLocalizer<SharedResources> localizer, IRelativesService relativesService)
		{
			_localizer = localizer;
			_relativesService = relativesService;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
		}
		#endregion
		#region Actions
		public void ApplyValidationsRules()
		{
			RuleFor(x => x.Id)
				.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

			RuleFor(x => x.NameEn)
				.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
				.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
				.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);

			RuleFor(x => x.NameAr)
			.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
			.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);
		}
		public void ApplyCustomValidationsRules()
		{
			RuleFor(x => x.Phone)
				.MustAsync(async (model, Key, CancellationToken) => !await _relativesService.IsPhoneExcludeSelf(Key!, model.Id))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);
		}
		#endregion
	}
}
