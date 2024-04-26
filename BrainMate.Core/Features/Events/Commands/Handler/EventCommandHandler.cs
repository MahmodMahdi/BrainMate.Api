using AutoMapper;
using BrainMate.Core.Bases;
using BrainMate.Core.Features.Events.Commands.Models;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities;
using BrainMate.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace BrainMate.Core.Features.Events.Commands.Handler
{
    public class EventCommandHandler : ResponseHandler,
                 IRequestHandler<AddEventCommand, Response<string>>,
                 IRequestHandler<UpdateEventCommand, Response<string>>,
                 IRequestHandler<DeleteEventCommand, Response<string>>

    {
        #region Fields
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion
        #region Constructor
        public EventCommandHandler(IEventService eventService,
                                     IMapper mapper,
                                     IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _eventService = eventService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion
        #region Handle Functions
        #region Create
        public async Task<Response<string>> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {

            // mapping
            var eventMapper = _mapper.Map<Event>(request);
            // Add
            var result = await _eventService.AddAsync(eventMapper);

            // return response
            switch (result)
            {
                case "Exist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NameIsExist]);
                case "PatientDeleteHisEmail": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.PatientDeleteAccount]);
                case "FailedToAdd": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]);
            }
            return Created("Added Successfully");

        }
        #endregion
        #region Update
        public async Task<Response<string>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            // check if the id is exist or not 
            var Event = await _eventService.GetByIdAsync(request.Id);
            // return notFound
            if (Event == null) return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            // mapping 
            var eventMapper = _mapper.Map(request, Event);
            // call service 
            var result = await _eventService.UpdateAsync(eventMapper);
            //return response
            switch (result)
            {
                case "Exist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NameIsExist]);
                case "FailedToUpdate": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);
            }
            return Success($"{eventMapper.Id} Updated Successfully");
        }
        #endregion
        #region Delete
        public async Task<Response<string>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            // check if the id is exist or not 
            var Event = await _eventService.GetByIdAsync(request.Id);
            // return notFound
            if (Event == null) return NotFound<string>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            // call service 
            var result = await _eventService.DeleteAsync(Event);
            if (result == "Success") return Deleted<string>($"{request.Id} Deleted Successfully ");
            else return BadRequest<string>();
        }
        #endregion
        #endregion
    }
}
