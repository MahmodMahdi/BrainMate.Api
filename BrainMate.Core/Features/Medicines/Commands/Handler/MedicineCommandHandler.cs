using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.Medicines.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Medicines.Commands.Handler
{
	internal class MedicineCommandHandler : ResponseHandler,
				 IRequestHandler<AddMedicineCommand, Response<string>>

	{
		#region Fields
		private readonly IMedicineService _medicineService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructor
		public MedicineCommandHandler(IMedicineService medicineService,
									 IMapper mapper,
									 IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_medicineService = medicineService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
		}


		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(AddMedicineCommand request, CancellationToken cancellationToken)
		{
			// mapping
			var medicineMapper = _mapper.Map<Medicine>(request);
			// Add
			var result = await _medicineService.AddAsync(medicineMapper, request.Image!);

			// return response
			switch (result)
			{
				case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
				case "FailedToUploadImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadImage]);
				case "FailedToAdd": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]);
			}
			return Created("Added Successfully");
		}
		#endregion
	}
}
