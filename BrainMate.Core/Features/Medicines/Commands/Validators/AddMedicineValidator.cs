using BrainMate.Core.Features.Medicines.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Medicines.Commands.Validators
{
    public class AddMedicineValidator : AbstractValidator<AddMedicineCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMedicineService _medicineService;
        #endregion
        #region Constructors
        public AddMedicineValidator(IStringLocalizer<SharedResources> localizer,
            IMedicineService medicineService)
        {
            _localizer = localizer;
            _medicineService = medicineService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
            .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);

            RuleFor(x => x.Frequency)
          .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
          .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Image)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.StartAt)
          .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
          .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.EndAt)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken) => !await _medicineService.IsNameExist(Key!))
                .WithMessage(_localizer[SharedResourcesKeys.IsExist]);
        }
        #endregion
    }
}
