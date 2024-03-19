using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.Foods.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Foods.Commands.Handler
{
	public class FoodCommandHandler : ResponseHandler,
				 IRequestHandler<AddFoodCommand, Response<string>>,
				 IRequestHandler<UpdateFoodCommand, Response<string>>,
				 IRequestHandler<DeleteFoodCommand, Response<string>>

	{
		#region Fields
		private readonly IFoodService _foodService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructor
		public FoodCommandHandler(IFoodService foodService,
									 IMapper mapper,
									 IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_foodService = foodService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
		}


		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(AddFoodCommand request, CancellationToken cancellationToken)
		{
			// mapping
			var foodMapper = _mapper.Map<Food>(request);
			// Add
			var result = await _foodService.AddAsync(foodMapper, request.Image!);

			// return response
			switch (result)
			{
				case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
				case "FailedToUploadImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadImage]);
				case "FailedToAdd": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]);
			}
			return Created("Added Successfully");
		}
		public async Task<Response<string>> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
		{
			// check if the id is exist or not 
			var food = await _foodService.GetFoodAsync(request.Id);
			// return notFound
			if (food == null) return NotFound<string>("relative is not found");
			// mapping 
			var foodMapper = _mapper.Map(request, food);
			// call service 
			var result = await _foodService.UpdateAsync(foodMapper, request.Image!);
			//return response
			switch (result)
			{
				case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
				case "FailedToUpdateImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateImage]);
				case "FailedToUpdate": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);
			}
			return Success($"{foodMapper.Id} Updated Successfully");
		}
		public async Task<Response<string>> Handle(DeleteFoodCommand request, CancellationToken cancellationToken)
		{
			// check if the id is exist or not 
			var food = await _foodService.GetFoodAsync(request.Id);
			// return notFound
			if (food == null) return NotFound<string>("Food is not found");
			// call service 
			var result = await _foodService.DeleteAsync(food);
			if (result == "Success") return Deleted<string>($"{request.Id} Deleted Successfully ");
			else return BadRequest<string>();
		}
		#endregion
	}
}
