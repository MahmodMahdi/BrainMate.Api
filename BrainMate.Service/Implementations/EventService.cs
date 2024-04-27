using BrainMate.Data.Entities;
using BrainMate.Data.Entities.Identity;
using BrainMate.Infrastructure.UnitofWork;
using BrainMate.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BrainMate.Service.Implementations
{
    public class EventService : IEventService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Patient> _userManager;
        #endregion

        #region Constructors
        public EventService(IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            UserManager<Patient> userManager)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        #endregion

        #region Handle Functions
        public IQueryable<Event> FilterEventsPaginatedQueryable(string search)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var user = _userManager.Users.FirstOrDefault(x => x.Email == patientEmailClaim);
            // Patient
            if (user!.PatientEmail == null)
            {
                var queryable = Helper(search);
                var result = queryable!.Where(x => x.PatientEmail == user!.Email);
                if (result.Any(x => x.PatientEmail == user!.Email)) { return result.AsQueryable(); }
                else return result.AsQueryable();
            }
            // Caregiver
            else
            {
                var queryable = Helper(search);
                var result = queryable!.Where(x => x.CaregiverEmail == user!.Email);
                if (result.Any(x => x.CaregiverEmail == user!.Email)) { return result.AsQueryable(); }
                else return result.AsQueryable();
            }

        }
        public async Task<Event> GetByIdAsync(int id)
        {
            var Event = await _unitOfWork.events.GetByIdAsync(id);
            return Event;

        }

        public async Task<string> AddAsync(Event Event)
        {
            // Add
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var caregiver = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmailClaim);
            var patientEmail = caregiver?.PatientEmail;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmail);
            if (user?.Id == null)
                return "PatientDeleteHisEmail";
            try
            {
                Event.PatientEmail = user.Email;
                Event.CaregiverEmail = caregiver!.Email;
                await _unitOfWork.events.AddAsync(Event);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedToAdd";
            }
        }
        public async Task<string> UpdateAsync(Event Event)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var caregiver = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmailClaim);
            var patientEmail = caregiver?.PatientEmail;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmail);

            try
            {
                await _unitOfWork.events.UpdateAsync(Event);
                return "Success";
            }
            catch
            {
                return "FailedToUpdate";
            }
        }
        public async Task<string> DeleteAsync(Event Event)
        {
            var transaction = await _unitOfWork.events.BeginTransactionAsync();
            try
            {
                await _unitOfWork.events.DeleteAsync(Event);
                await transaction.CommitAsync();
                return "Success";
            }
            catch
            {
                await transaction.RollbackAsync();
                return "Failed";
            }
        }

        public async Task<bool> IsNameExist(string name)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var caregiver = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmailClaim);
            var patientEmail = caregiver?.PatientEmail;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmail);
            if (user != null)
            {
                // check if the name exist or not
                var ExistEvent = await _unitOfWork.events.GetTableNoTracking().Where(x => (x.Task!.Equals(name)) && x.PatientEmail == user!.Email).FirstOrDefaultAsync();

                if (ExistEvent == null) { return false; }
                else return true;
            }
            else
            {
                var ExistEvent = await _unitOfWork.events.GetTableNoTracking().Where(x => (x.Task!.Equals(name)) && x.CaregiverEmail == caregiver!.Email).FirstOrDefaultAsync();
                if (ExistEvent == null) { return false; }
                else return true;
            }
        }
        public async Task<bool> IsNameExcludeSelf(string name, int id)
        {
            var patientEmailClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            var caregiver = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmailClaim);
            var patientEmail = caregiver?.PatientEmail;
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == patientEmail);
            if (user != null)
            {
                // check if Name exclude self or exist in another field
                var ExistEvent = await _unitOfWork.events.GetTableNoTracking().Where(x => (x.Task!.Equals(name)) && x.PatientEmail == user!.Email && x.Id != id).FirstOrDefaultAsync();
                if (ExistEvent == null) { return false; }
                else return true;
            }
            else
            {
                var ExistEvent = await _unitOfWork.events.GetTableNoTracking().Where(x => (x.Task!.Equals(name)) || x.CaregiverEmail == caregiver!.Email && x.Id != id).FirstOrDefaultAsync();
                if (ExistEvent == null) { return false; }
                else return true;
            }
        }

        public IQueryable<Event> Helper(string search)
        {
            var queryable = _unitOfWork.events.GetTableNoTracking().OrderBy(x => x.Time).AsQueryable();
            if (search != null)
            {
                queryable = queryable.Where(x => x.Task!.Contains(search));
            }
            return queryable;
        }
        #endregion
    }
}
