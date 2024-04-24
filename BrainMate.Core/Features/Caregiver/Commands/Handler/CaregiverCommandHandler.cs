using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.Caregiver.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities.Identity;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace BrainMate.Core.Features.Caregiver.Commands.Handler
{
    public class CaregiverCommandHandler : ResponseHandler,
                                        IRequestHandler<DeleteCaregiverCommand, Response<string>>,
                                        IRequestHandler<UpdateCaregiverCommand, Response<string>>



    {
        #region Fields
        private readonly ICaregiverService _caregiverService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Patient> _userManager;
        #endregion
        #region Constructor
        public CaregiverCommandHandler(ICaregiverService caregiverService,
                                     IMapper mapper,
                                     IStringLocalizer<SharedResources> stringLocalizer,
                                     IHttpContextAccessor httpContextAccessor,
                                     UserManager<Patient> userManager) : base(stringLocalizer)
        {
            _caregiverService = caregiverService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(UpdateCaregiverCommand request, CancellationToken cancellationToken)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var caregiver = await _userManager.FindByEmailAsync(patientEmailClaim!);
            // check if the id is exist or not 
            var CaregiverResult = await _caregiverService.GetCaregiverAsync(caregiver!.Id);
            // return notFound
            if (CaregiverResult == null) return NotFound<string>("Caregiver is not found");
            // mapping 
            var patientMapper = _mapper.Map(request, CaregiverResult);
            // call service 
            var result = await _caregiverService.UpdateAsync(patientMapper, request.Image!);
            //return response
            switch (result)
            {
                case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
                case "FailedToUpdateImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateImage]);
                case "FailedToUpdate": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);
                case "PhoneExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.PhoneExist]);
            }
            return Success($"{patientMapper.Id} Updated Successfully");
        }
        public async Task<Response<string>> Handle(DeleteCaregiverCommand request, CancellationToken cancellationToken)
        {
            var caregiverEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(caregiverEmailClaim!);

            if (user == null) return NotFound<string>();
            // Delete the item
            var result = await _caregiverService.DeleteAsync(user);

            //Not success 
            switch (result)
            {
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeletedFailed]);
            }
            //Success message
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
        }
        #endregion
    }
}
