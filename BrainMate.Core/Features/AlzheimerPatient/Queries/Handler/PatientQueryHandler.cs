using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.AlzheimerPatient.Queries.Dto;
using BrainMate.Core.Features.AlzheimerPatient.Queries.Models;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities.Identity;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace BrainMate.Core.Features.AlzheimerPatient.Queries.Handler
{
    public class PatientQueryHandler : ResponseHandler,
                                       IRequestHandler<GetPatientByIdQuery, Response<GetPatientResponse>>,
                                       IRequestHandler<LockUnlockCaregiverQuery, Response<string>>

    {
        // Mediator
        #region Fields
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Patient> _userManager;
        #endregion
        #region Constructors
        public PatientQueryHandler(IPatientService patientService,
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
        public async Task<Response<GetPatientResponse>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(patientEmailClaim!);
            if (user!.PatientEmail == null)
            {
                var Patient = await _patientService.GetByIdAsync(user!.Id);
                var result = _mapper.Map<GetPatientResponse>(Patient);
                if (result == null)
                {
                    return NotFound<GetPatientResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
                }
                return Success(result);
            }
            else
            {
                var PatientEmail = user.PatientEmail;
                var Result = await _userManager.FindByEmailAsync(PatientEmail);
                if (Result == null) return NotFound<GetPatientResponse>(_stringLocalizer[SharedResourcesKeys.PatientDeleteAccount]);
                var Patient = await _patientService.GetByIdAsync(Result!.Id);

                var result = _mapper.Map<GetPatientResponse>(Patient);

                return Success(result);
            }
        }
        public async Task<Response<string>> Handle(LockUnlockCaregiverQuery request, CancellationToken cancellationToken)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(patientEmailClaim!);
            if (user != null)
            {
                var caregiver = _userManager.Users.FirstOrDefault(x => x.PatientEmail == user.Email);
                if (caregiver == null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ThereIsNoCaregiver]);
                var result = await _patientService.LockUnlockAsync(caregiver!.Id);
                switch (result)
                {
                    case "NotFound": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
                    case "LockedDone": return Success<string>(_stringLocalizer[SharedResourcesKeys.LockedDone]);
                    case "UnLockDone": return Success<string>(_stringLocalizer[SharedResourcesKeys.UnLockDone]);
                }
                return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
            }
            else return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SomeThingGoesWrong]);


        }
        #endregion
    }
}
