using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.Foods.Queries.Dtos;
using BrainMate.Core.Features.Foods.Queries.Models;
using BrainMate.Core.Resources;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Foods.Queries.Handler
{
	public class FoodQueryHandler : ResponseHandler,
									   IRequestHandler<GetFoodListQuery, Response<List<GetFoodListResponse>>>,
									   IRequestHandler<GetFoodByIdQuery, Response<GetFoodResponse>>,
									   IRequestHandler<SearchFoodQuery, Response<List<SearchFoodResponse>>>
	{
		// Mediator
		#region Fields
		private readonly IFoodService _foodService;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructors
		public FoodQueryHandler(IFoodService foodService,
								   IMapper mapper,
								   IStringLocalizer<SharedResources> stringLocalizer,
								   IHttpContextAccessor httpContextAccessor) : base(stringLocalizer)
		{
			_foodService = foodService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
			_httpContextAccessor = httpContextAccessor;
		}

		#endregion
		#region Handle Functions
		public async Task<Response<List<GetFoodListResponse>>> Handle(GetFoodListQuery request, CancellationToken cancellationToken)
		{
			var FoodList = await _foodService.GetAllAsync();
			var FoodListMapper = _mapper.Map<List<GetFoodListResponse>>(FoodList);
			var result = Success(FoodListMapper);
			result.Meta = new { Count = FoodListMapper.Count };
			return result;
		}

		public async Task<Response<GetFoodResponse>> Handle(GetFoodByIdQuery request, CancellationToken cancellationToken)
		{
			var food = await _foodService.GetByIdAsync(request.Id);
			if (food == null)
			{
				// using the localization
				return NotFound<GetFoodResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			}
			var result = _mapper.Map<GetFoodResponse>(food);
			return Success(result);
		}
		public async Task<Response<List<SearchFoodResponse>>> Handle(SearchFoodQuery request, CancellationToken cancellationToken)
		{
			var foods = await _foodService.SearchAsync(request.Search!);
			var foodMapped = _mapper.Map<List<SearchFoodResponse>>(foods);
			var result = Success(foodMapped);
			result.Meta = new { Count = foodMapped.Count };
			return result;
		}
		#endregion
	}
}
