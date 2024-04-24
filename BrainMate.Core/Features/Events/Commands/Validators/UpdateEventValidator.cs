using BrainMate.Core.Features.Events.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Events.Commands.Validators
{
    public class UpdateEventValidator : AbstractValidator<UpdateEventCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IEventService _eventService;

        #endregion
        #region Constructors
        public UpdateEventValidator(IStringLocalizer<SharedResources> localizer, IEventService eventService)
        {
            _localizer = localizer;
            _eventService = eventService;
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

            RuleFor(x => x.Task)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);
            RuleFor(x => x.Time)
              .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
              .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
        }
        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Task)
                .MustAsync(async (model, Key, CancellationToken) => !await _eventService.IsNameExcludeSelf(Key!, model.Id))
                .WithMessage(_localizer[SharedResourcesKeys.IsExist]);
        }
        #endregion
    }
}
