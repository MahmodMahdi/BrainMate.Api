using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.Relative.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Relative.Commands.Handler
{
	public class RelativeCommandHandler : ResponseHandler,
				 IRequestHandler<AddRelativeCommand, Response<string>>,
				 IRequestHandler<UpdateRelativeCommand, Response<string>>,
				 IRequestHandler<DeleteRelativeCommand, Response<string>>

	{
		#region Fields
		private readonly IRelativesService _relativeService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructor
		public RelativeCommandHandler(IRelativesService relativeService,
									 IMapper mapper,
									 IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_relativeService = relativeService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
		}


		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(AddRelativeCommand request, CancellationToken cancellationToken)
		{
			// mapping
			var relativeMapper = _mapper.Map<Relatives>(request);
			// Add
			var result = await _relativeService.AddAsync(relativeMapper, request.Image!);

			// return response
			switch (result)
			{
				case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
				case "FailedToUploadImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadImage]);
				case "FailedToAdd": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]);
			}
			return Created("Added Successfully");
		}

		public async Task<Response<string>> Handle(UpdateRelativeCommand request, CancellationToken cancellationToken)
		{
			// check if the id is exist or not 
			var relative = await _relativeService.GetRelativeAsync(request.Id);
			// return notFound
			if (relative == null) return NotFound<string>("relative is not found");
			// mapping 
			var relativeMapper = _mapper.Map(request, relative);
			// call service 
			var result = await _relativeService.UpdateAsync(relativeMapper, request.Image!);
			//return response
			switch (result)
			{
				case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
				case "FailedToUpdateImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateImage]);
				case "FailedToUpdate": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);
			}
			return Success($"{relativeMapper.Id} Updated Successfully");
		}

		public async Task<Response<string>> Handle(DeleteRelativeCommand request, CancellationToken cancellationToken)
		{
			// check if the id is exist or not 
			var relative = await _relativeService.GetRelativeAsync(request.Id);
			// return notFound
			if (relative == null) return NotFound<string>("relative is not found");
			// call service 
			var result = await _relativeService.DeleteAsync(relative);
			if (result == "Success") return Deleted<string>($"{request.Id} Deleted Successfully ");
			else return BadRequest<string>();
		}
		#endregion
	}
}
