using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.Events.Queries.Dtos;
using BrainMate.Core.Features.Events.Queries.Models;
using BrainMate.Core.Resources;
using BrainMate.Core.Wrappers;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Events.Queries.Handler
{
	public class EventQueryHandler : ResponseHandler,
	IRequestHandler<GetEventsPaginatedListQuery, PaginateResult<GetEventsPaginatedListResponse>>,
	//IRequestHandler<SearchRelativesQuery, PaginateResult<SearchRelativesResponse>>,
	IRequestHandler<GetEventByIdQuery, Response<GetEventResponse>>
	{
		// Mediator
		#region Fields
		private readonly IEventService _eventService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructors
		public EventQueryHandler(IEventService eventService,
								   IMapper mapper,
								   IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_eventService = eventService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
		}
		#endregion
		#region Handle Functions
		public async Task<PaginateResult<GetEventsPaginatedListResponse>> Handle(GetEventsPaginatedListQuery request, CancellationToken cancellationToken)
		{
			var FilterQuery = _eventService.FilterEventsPaginatedQueryable();
			var paginatedList = await _mapper.ProjectTo<GetEventsPaginatedListResponse>(FilterQuery).ToPaginatedListAsync(request.PageNumber, request.PageSize);
			paginatedList.Meta = new { Count = paginatedList.Data!.Count };
			return paginatedList;
		}
		public async Task<Response<GetEventResponse>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
		{
			var Relative = await _eventService.GetEventByIdAsync(request.Id);
			if (Relative == null)
			{
				// using the localization
				return NotFound<GetEventResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			}
			var result = _mapper.Map<GetEventResponse>(Relative);
			return Success(result);
		}
		#endregion
	}
}
