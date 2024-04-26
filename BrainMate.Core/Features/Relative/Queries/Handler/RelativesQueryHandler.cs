using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.Relative.Queries.Dtos;
using BrainMate.Core.Features.Relative.Queries.Models;
using BrainMate.Core.Resources;
using BrainMate.Core.Wrappers;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Relative.Queries.Handler
{
    public class RelativesQueryHandler : ResponseHandler,
                                       IRequestHandler<GetRelativesPaginatedListQuery, PaginateResult<GetRelativesPaginatedListResponse>>,
                                       IRequestHandler<GetRelativesByIdQuery, Response<GetRelativesResponse>>
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
        #endregion
        #region Handle Functions
        public async Task<PaginateResult<GetRelativesPaginatedListResponse>> Handle(GetRelativesPaginatedListQuery request, CancellationToken cancellationToken)
        {
            #region Another Ways

            // delegate (first way)
            //Expression<Func<Relatives, GetRelativesPaginatedListResponse>> expression = (e => new GetRelativesPaginatedListResponse(e.Id,
            // e.Localize(e.NameAr!, e.NameEn!), e.Address!, e.Age,
            #endregion

            var context = _httpContextAccessor.HttpContext!.Request;
            var baseUrl = context.Scheme + "://" + context.Host;
            var FilterQuery = _relativesService.FilterRelativesPaginatedQueryable(request.Search!);

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

        public async Task<Response<GetRelativesResponse>> Handle(GetRelativesByIdQuery request, CancellationToken cancellationToken)
        {
            var Relative = await _relativesService.GetByIdAsync(request.Id);
            if (Relative == null)
            {
                // using the localization
                return NotFound<GetRelativesResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            var result = _mapper.Map<GetRelativesResponse>(Relative);
            return Success(result);
        }
        #endregion
    }
}
