using BrainMate.Core.Features.Events.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Events.Commands.Validators;

public class AddEventValidator : AbstractValidator<AddEventCommand>
{
	#region Fields
	private readonly IStringLocalizer<SharedResources> _localizer;
	private readonly IEventService _eventService;
	#endregion
	#region Constructors
	public AddEventValidator(IStringLocalizer<SharedResources> localizer, IEventService eventService)
	{
		_eventService = eventService;
		_localizer = localizer;
		ApplyValidationsRules();
		ApplyCustomValidationsRules();
	}
	#endregion
	#region Actions
	public void ApplyValidationsRules()
	{
		RuleFor(x => x.TaskEn)
			.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
			.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);

		RuleFor(x => x.TaskAr)
			.NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
			.NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
			.MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthIs100]);
	}
	public void ApplyCustomValidationsRules()
	{
		RuleFor(x => x.TaskEn)
			.MustAsync(async (Key, CancellationToken) => !await _eventService.IsNameExist(Key!))
			.WithMessage(_localizer[SharedResourcesKeys.IsExist]);
		RuleFor(x => x.TaskAr)
			.MustAsync(async (Key, CancellationToken) => !await _eventService.IsNameExist(Key!))
			.WithMessage(_localizer[SharedResourcesKeys.IsExist]);
	}
	#endregion
}