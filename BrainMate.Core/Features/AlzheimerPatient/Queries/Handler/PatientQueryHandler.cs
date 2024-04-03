using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.AlzheimerPatient.Queries.Dto;
using BrainMate.Core.Features.AlzheimerPatient.Queries.Models;
using BrainMate.Core.Resources;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.AlzheimerPatient.Queries.Handler
{
	public class PatientQueryHandler : ResponseHandler,
									   IRequestHandler<GetPatientByIdQuery, Response<GetPatientResponse>>

	{
		// Mediator
		#region Fields
		private readonly IPatientService _patientService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructors
		public PatientQueryHandler(IPatientService patientService,
								   IMapper mapper,
								   IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_patientService = patientService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
		}
		#endregion
		#region Handle Functions
		public async Task<Response<GetPatientResponse>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
		{
			var Patient = await _patientService.GetByIdAsync(request.Id);
			if (Patient == null)
			{
				return NotFound<GetPatientResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			}
			var result = _mapper.Map<GetPatientResponse>(Patient);
			return Success(result);
		}
		#endregion
	}
}
