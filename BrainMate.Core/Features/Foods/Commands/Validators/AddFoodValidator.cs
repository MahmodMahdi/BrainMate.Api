using BrainMate.Core.Features.Foods.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Foods.Commands.Validators
{
    public class AddFoodValidator : AbstractValidator<AddFoodCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IFoodService _foodService;
        #endregion
        #region Constructors
        public AddFoodValidator(IStringLocalizer<SharedResources> localizer, IFoodService foodService)
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
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
            .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);

            RuleFor(x => x.Type)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Time)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken) => !await _foodService.IsNameExist(Key!))
                .WithMessage(_localizer[SharedResourcesKeys.IsExist]);
        }
        #endregion
    }
}
