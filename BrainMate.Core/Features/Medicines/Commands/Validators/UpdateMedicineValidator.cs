using BrainMate.Core.Features.Medicines.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Medicines.Commands.Validators
{
	internal class UpdateMedicineValidator : AbstractValidator<UpdateMedicineCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _localizer;
		private readonly IMedicineService _medicineService;
		#endregion
		#region Constructors
		public UpdateMedicineValidator(IStringLocalizer<SharedResources> localizer, IMedicineService medicineService)
		{
			_medicineService = medicineService;
			_localizer = localizer;
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
				.MustAsync(async (model, Key, CancellationToken) => !await _medicineService.IsNameExcludeSelf(Key!, model.Id))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);

			RuleFor(x => x.NameEn)
				.MustAsync(async (model, Key, CancellationToken) => !await _medicineService.IsNameExcludeSelf(Key!, model.Id))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);
		}
		#endregion
	}
}
