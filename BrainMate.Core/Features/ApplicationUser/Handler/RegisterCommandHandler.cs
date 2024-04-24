using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.ApplicationUser.Models;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Service.Abstracts;
using System.Security.Claims;

namespace BrainMate.Core.Features.ApplicationUser.Handler
{
    public class RegisterCommandHandler : ResponseHandler,
        IRequestHandler<RegisterCommand, Response<string>>,
        IRequestHandler<CaregiverRegisterCommand, Response<string>>,

        IRequestHandler<ChangePatientPasswordCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<Patient> _userManager;
        private readonly IApplicationUserService _applicationUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion
        #region Constructor
        public RegisterCommandHandler(IMapper mapper,
                                  IStringLocalizer<SharedResources> stringLocalizer,
                                  UserManager<Patient> userManager,
                                   IApplicationUserService applicationUserService,
                                   IHttpContextAccessor httpContextAccessor) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _applicationUserService = applicationUserService;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion
        #region Handle Function
        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            //Mapping
            var IdentityUser = _mapper.Map<Patient>(request);

            var Result = await _applicationUserService.RegisterAsync(IdentityUser, request.Password!);
            // Failed
            switch (Result)
            {
                case "EmailIsExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SomeThingGoesWrong]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(Result);
            }
        }
        public async Task<Response<string>> Handle(CaregiverRegisterCommand request, CancellationToken cancellationToken)
        {
            //Mapping
            var IdentityCaregiver = _mapper.Map<Patient>(request);
            var Result = await _applicationUserService.CaregiverRegisterAsync(IdentityCaregiver, request.Password!);
            // Failed
            switch (Result)
            {
                case "EmailIsExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);
                case "ThereIsAnotherCaregiverOnThisPatientEmail": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.ThereIsAnotherCaregiverOnThisPatientEmail]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SomeThingGoesWrong]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(Result);
            }
        }
        //public async Task<Response<string>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        //{
        //    // Check if user exist 
        //    // var user = await _userManager.FindByIdAsync(request.Id.ToString());
        //    var existUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

        //    // Not exist
        //    if (existUser == null) return NotFound<string>();

        //    // mapping
        //    var newUser = _mapper.Map(request, existUser);

        //    //Check on Phone Number (can't duplicate)
        //    var SearchByPhone = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == newUser.PhoneNumber && x.Id != newUser.Id);
        //    if (SearchByPhone != null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.PhoneExist]);

        //    //update
        //    var result = await _userManager.UpdateAsync(newUser);

        //    //Not success 
        //    if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdatedFailed]);

        //    //Success message
        //    return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
        //}

        public async Task<Response<string>> Handle(ChangePatientPasswordCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByNameAsync(userIdClaim!);
            // Get User
            var User = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == user!.Id);

            // If not exist => Not found
            if (User == null) return NotFound<string>();

            // if exist => change password
            var result = await _userManager.ChangePasswordAsync(User, request.CurrentPassword!, request.NewPassword!);
            // Not Success
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault()!.Description);

            return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
        }


        #endregion
    }
}
