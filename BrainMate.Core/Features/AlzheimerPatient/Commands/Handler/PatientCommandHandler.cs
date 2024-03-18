using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.AlzheimerPatient.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.AlzheimerPatient.Commands.Handler
{
	public class PatientCommandHandler : ResponseHandler,
										IRequestHandler<AddPatientCommand, Response<string>>,
										IRequestHandler<UpdatePatientCommand, Response<string>>

	{
		#region Fields
		private readonly IPatientService _patientService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructor
		public PatientCommandHandler(IPatientService patientService,
									 IMapper mapper,
									 IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_patientService = patientService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
		}
		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(AddPatientCommand request, CancellationToken cancellationToken)
		{
			// mapping
			var patientMapper = _mapper.Map<Patient>(request);
			// Add
			var result = await _patientService.AddAsync(patientMapper, request.Image!);

			// return response
			switch (result)
			{
				case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
				case "FailedToUploadImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadImage]);
				case "FailedToAdd": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]);
			}
			return Created("Added Successfully");
		}

		public async Task<Response<string>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
		{
			// check if the id is exist or not 
			var patient = await _patientService.GetPatientAsync(request.Id);
			// return notFound
			if (patient == null) return NotFound<string>("patient is not found");
			// mapping 
			var patientMapper = _mapper.Map(request, patient);
			// call service 
			var result = await _patientService.UpdateAsync(patientMapper, request.Image!);
			//return response
			switch (result)
			{
				case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
				case "FailedToUpdateImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateImage]);
				case "FailedToUpdate": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);
			}
			return Success($"{patientMapper.Id} Updated Successfully");
		}
		#endregion
	}
}
