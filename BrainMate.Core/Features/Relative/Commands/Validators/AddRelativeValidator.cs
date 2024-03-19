﻿using BrainMate.Core.Features.Relative.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Relative.Commands.Validators
{
	public class AddRelativeValidator : AbstractValidator<AddRelativeCommand>
	{
		#region Fields
		private readonly IRelativesService _relativesService;
		private readonly IStringLocalizer<SharedResources> _localizer;

		#endregion
		#region Constructors
		public AddRelativeValidator(IStringLocalizer<SharedResources> localizer, IRelativesService relativesService)
		{
			_relativesService = relativesService;
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
			RuleFor(x => x.Phone)
				.MustAsync(async (Key, CancellationToken) => !await _relativesService.IsPhoneExist(Key!))
				.WithMessage(_localizer[SharedResourcesKeys.IsExist]);
		}
		#endregion
	}
}
