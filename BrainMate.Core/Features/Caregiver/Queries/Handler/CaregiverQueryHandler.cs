using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.Caregiver.Queries.Dto;
using BrainMate.Core.Features.Caregiver.Queries.Models;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities.Identity;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace BrainMate.Core.Features.Caregiver.Queries.Handler
{
    public class CaregiverQueryHandler : ResponseHandler,
        IRequestHandler<GetCaregiverQuery, Response<GetCaregiverResponse>>

    {
        // Mediator
        #region Fields
        private readonly ICaregiverService _patientService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Patient> _userManager;
        #endregion
        #region Constructors
        public CaregiverQueryHandler(ICaregiverService patientService,
                                   IMapper mapper,
                                   IStringLocalizer<SharedResources> stringLocalizer,
                                   IHttpContextAccessor httpContextAccessor,
                                   UserManager<Patient> userManager) : base(stringLocalizer)
        {
            _patientService = patientService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<GetCaregiverResponse>> Handle(GetCaregiverQuery request, CancellationToken cancellationToken)
        {
            var userClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByNameAsync(userClaim!);
            var Caregiver = await _patientService.GetCaregiverAsync(user!.Id);
            if (Caregiver == null)
            {
                return NotFound<GetCaregiverResponse>(_stringLocalizer[SharedResourcesKeys.ThereIsNoCaregiverOnYourAccount]);
            }
            var result = _mapper.Map<GetCaregiverResponse>(Caregiver);
            return Success(result);
        }
        #endregion
    }
}
