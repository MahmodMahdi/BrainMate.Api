using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Resources;
using BrainMate.Service.Abstracts;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Relatives.Commands.Handler
{
	public class RelativeCommandHandler : ResponseHandler
	//IRequestHandler<AddRelativeCommand, Response<string>>

	{
		#region Fields
		private readonly IPatientService _patientService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructor
		public RelativeCommandHandler(IPatientService patientService,
									 IMapper mapper,
									 IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_patientService = patientService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
		}
		#endregion
		#region Handle Functions
		#endregion
	}
}
