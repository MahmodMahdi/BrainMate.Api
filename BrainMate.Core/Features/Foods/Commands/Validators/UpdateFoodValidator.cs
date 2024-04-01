using BrainMate.Core.Features.Medicines.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Foods.Commands.Validators
{
	public class UpdateFoodValidator : AbstractValidator<UpdateMedicineCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _localizer;
		private readonly IFoodService _foodService;
		#endregion
		#region Constructors
		public UpdateFoodValidator(IStringLocalizer<SharedResources> localizer, IFoodService foodService)
		{
			_localizer = localizer;
			_foodService = foodService;
			ApplyValidationsRules();
			ApplyCustomValidationsRules();
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
		public void ApplyCustomValidationsRules()
		{
			RuleFor(x => x.NameAr)
				.MustAsync(async (model, Key, CancellationToken) => !await _foodService.IsNameExcludeSelf(Key!, model.Id))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);

			RuleFor(x => x.NameEn)
				.MustAsync(async (model, Key, CancellationToken) => !await _foodService.IsNameExcludeSelf(Key!, model.Id))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);
		}
		#endregion
	}
}
