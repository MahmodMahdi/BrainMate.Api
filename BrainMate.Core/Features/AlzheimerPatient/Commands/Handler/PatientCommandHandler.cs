using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.AlzheimerPatient.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities.Identity;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace BrainMate.Core.Features.AlzheimerPatient.Commands.Handler
{
    public class PatientCommandHandler : ResponseHandler,
                                          IRequestHandler<UpdatePatientCommand, Response<string>>,
                                        IRequestHandler<DeletePatientCommand, Response<string>>


    {
        #region Fields
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Patient> _userManager;
        #endregion
        #region Constructor
        public PatientCommandHandler(IPatientService patientService,
                                     IMapper mapper,
                                     IStringLocalizer<SharedResources> stringLocalizer,
                                     IHttpContextAccessor httpContextAccessor,
                                     UserManager<Patient> userManager) : base(stringLocalizer)
        {
            _patientService = patientService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        #endregion
        #region Handle Functions

        public async Task<Response<string>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(patientEmailClaim!);
            if (user!.PatientEmail != null)
            {
                var UserEmail = user!.PatientEmail;
                var Patient = _userManager.Users.FirstOrDefault(x => x.Email == UserEmail);
                // check if the id is exist or not
                var patient = await _patientService.GetPatientAsync(Patient!.Id);
                // return notFound
                if (patient == null) return NotFound<string>();
                // mapping 
                var patientMapper = _mapper.Map(request, patient);
                // call service 
                return await Helper(request, patientMapper);
            }
            else
            {
                // check if the id is exist or not
                var patient = await _patientService.GetPatientAsync(user!.Id);
                // return notFound
                if (patient == null) return NotFound<string>();
                // mapping 
                var patientMapper = _mapper.Map(request, patient);
                // call service 
                return await Helper(request, patientMapper);
            }
            //return response

        }

        public async Task<Response<string>> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(patientEmailClaim!);
            // Check if user exist 
            var User = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == user!.Id);

            // Not exist
            if (User == null) return NotFound<string>();

            /// Delete the all data the relative to item
            //var Medicines = await _unitOfWork.medicines.GetTableNoTracking().Where(x => x.PatientId == user!.Id).ToListAsync();
            //var Foods = await _unitOfWork.foods.GetTableNoTracking().Where(x => x.PatientId == user!.Id).ToListAsync();
            //var Events = await _unitOfWork.events.GetTableNoTracking().Where(x => x.PatientId == user!.Id).ToListAsync();
            //var Relatives = await _unitOfWork.relatives.GetTableNoTracking().Where(x => x.PatientId == user!.Id).ToListAsync();
            var result = await _patientService.DeleteAsync(User);
            //var result = await _userManager.DeleteAsync(User);
            switch (result)
            {
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeletedFailed]);
            }
            // Success message
            return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
        }
        public async Task<Response<string>> Helper(UpdatePatientCommand request, Patient patient)
        {
            var patientMapper = _mapper.Map(request, patient);
            // call service 
            var result = await _patientService.UpdateAsync(patientMapper, request.Image!);
            switch (result)
            {
                case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
                case "FailedToUpdateImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateImage]);
                case "FailedToUpdate": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);
                case "PhoneExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.PhoneExist]);
            }
            return Success($"{patientMapper.Id} Updated Successfully");
        }
        #endregion
    }
}
