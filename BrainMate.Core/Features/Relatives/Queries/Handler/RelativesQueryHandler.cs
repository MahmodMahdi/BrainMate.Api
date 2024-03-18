using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.Relatives.Queries.Dtos;
using BrainMate.Core.Features.Relatives.Queries.Models;
using BrainMate.Core.Resources;
using BrainMate.Core.Wrappers;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Relatives.Queries.Handler
{
	public class RelativesQueryHandler : ResponseHandler,
									   IRequestHandler<GetRelativesPaginatedListQuery, PaginateResult<GetRelativesPaginatedListResponse>>
	{
		// Mediator
		#region Fields
		private readonly IRelativesService _relativesService;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructors
		public RelativesQueryHandler(IRelativesService relativesService,
								   IMapper mapper,
								   IStringLocalizer<SharedResources> stringLocalizer,
								   IHttpContextAccessor httpContextAccessor) : base(stringLocalizer)
		{
			_relativesService = relativesService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<PaginateResult<GetRelativesPaginatedListResponse>> Handle(GetRelativesPaginatedListQuery request, CancellationToken cancellationToken)
		{
			var context = _httpContextAccessor.HttpContext!.Request;
			var baseUrl = context.Scheme + "://" + context.Host;
			// delegate (first way)
			//Expression<Func<Relatives, GetRelativesPaginatedListResponse>> expression = (e => new GetRelativesPaginatedListResponse(e.Id,
			// e.Localize(e.NameAr!, e.NameEn!), e.Address!, e.Age,
			var FilterQuery = _relativesService.FilterRelativesPaginatedQueryable();

			// second way
			//await FilterQuery.Select(x => new GetRelativesPaginatedListResponse(x.Id, x.Localize(x.NameAr!, x.NameEn!), x.Address!, x.Age!)))
			var paginatedList = await _mapper.ProjectTo<GetRelativesPaginatedListResponse>(FilterQuery).ToPaginatedListAsync(request.PageNumber, request.PageSize);
			paginatedList.Meta = new { Count = paginatedList.Data!.Count };
			foreach (var relative in paginatedList.Data)
			{
				if (relative.Image != null)
				{
					relative.Image = baseUrl + relative.Image;
				}
			}
			return paginatedList;
		}


		#endregion
		#region Handle Functions



		#endregion
	}
}
