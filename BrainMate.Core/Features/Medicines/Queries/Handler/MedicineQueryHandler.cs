using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.Medicines.Queries.Dtos;
using BrainMate.Core.Features.Medicines.Queries.Modes;
using BrainMate.Core.Resources;
using BrainMate.Core.Wrappers;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Medicines.Queries.Handler
{
	public class MedicineQueryHandler : ResponseHandler,
									   IRequestHandler<GetMedicinePaginatedListQuery, PaginateResult<GetMedicinePaginatedListResponse>>
	//   IRequestHandler<SearchRelativesQuery, PaginateResult<SearchRelativesResponse>>,
	//  IRequestHandler<GetRelativesByIdQuery, Response<GetRelativesResponse>>
	{
		// Mediator
		#region Fields
		private readonly IMedicineService _medicineService;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructors
		public MedicineQueryHandler(IMedicineService medicineService,
								   IMapper mapper,
								   IStringLocalizer<SharedResources> stringLocalizer,
								   IHttpContextAccessor httpContextAccessor) : base(stringLocalizer)
		{
			_medicineService = medicineService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
			_httpContextAccessor = httpContextAccessor;
		}
		#endregion
		#region Handle Functions
		public async Task<PaginateResult<GetMedicinePaginatedListResponse>> Handle(GetMedicinePaginatedListQuery request, CancellationToken cancellationToken)
		{
			var context = _httpContextAccessor.HttpContext!.Request;
			var baseUrl = context.Scheme + "://" + context.Host;
			var FilterQuery = _medicineService.FilterMedicinesPaginatedQueryable();
			var paginatedList = await _mapper.ProjectTo<GetMedicinePaginatedListResponse>(FilterQuery).ToPaginatedListAsync(request.PageNumber, request.PageSize);
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
	}
}
