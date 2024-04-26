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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace BrainMate.Core.Features.Caregiver.Queries.Handler
{
    public class CaregiverQueryHandler : ResponseHandler,
        IRequestHandler<GetCaregiverQuery, Response<GetCaregiverResponse>>

    {
        // Mediator
        #region Fields
        private readonly ICaregiverService _caregiverService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Patient> _userManager;
        #endregion
        #region Constructors
        public CaregiverQueryHandler(ICaregiverService caregiverService,
                                   IMapper mapper,
                                   IStringLocalizer<SharedResources> stringLocalizer,
                                   IHttpContextAccessor httpContextAccessor,
                                   UserManager<Patient> userManager) : base(stringLocalizer)
        {
            _caregiverService = caregiverService;
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
            if (user!.PatientEmail == null)
            {
                var caregiver = await _userManager.Users.FirstOrDefaultAsync(x => x.PatientEmail == user.Email);
                if (caregiver == null) { return NotFound<GetCaregiverResponse>(_stringLocalizer[SharedResourcesKeys.ThereIsNoCaregiverOnYourAccount]); }
                var result = await _caregiverService.GetCaregiverAsync(caregiver!.Id);
                var response = _mapper.Map<GetCaregiverResponse>(result);
                return Success(response);
            }
            else
            {
                var Caregiver = await _caregiverService.GetCaregiverAsync(user!.Id);
                if (Caregiver == null)
                {
                    return NotFound<GetCaregiverResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
                }
                var result = _mapper.Map<GetCaregiverResponse>(Caregiver);
                return Success(result);
            }
        }
        #endregion
    }
}
