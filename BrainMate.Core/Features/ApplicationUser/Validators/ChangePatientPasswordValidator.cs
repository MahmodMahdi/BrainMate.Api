using BrainMate.Core.Features.ApplicationUser.Models;
using BrainMate.Core.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Validators
{
    public class ChangePatientPasswordValidator : AbstractValidator<ChangePatientPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public ChangePatientPasswordValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.CurrentPassword)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.NewPassword)
               .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
               .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword).WithMessage(SharedResourcesKeys.PasswordNotEqualConfirmPassword)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
            .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }

        #endregion
    }
}
