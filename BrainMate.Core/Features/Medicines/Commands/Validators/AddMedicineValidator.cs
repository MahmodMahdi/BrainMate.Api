﻿using BrainMate.Core.Features.Medicines.Commands.Models;
using BrainMate.Core.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Medicines.Commands.Validators
{
	public class AddMedicineValidator : AbstractValidator<AddMedicineCommand>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _localizer;
		#endregion
		#region Constructors
		public AddMedicineValidator(IStringLocalizer<SharedResources> localizer)
		{
			_localizer = localizer;
			ApplyValidationsRules();
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
		#endregion
	}
}
