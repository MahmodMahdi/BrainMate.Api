using BrainMate.Core.Features.Events.Queries.Dtos;
using BrainMate.Data.Entities;


namespace BrainMate.Core.Mapping.Events
{
	public partial class EventProfile
	{
		public void GetEventsPaginationMapping()
		{
			CreateMap<Event, GetEventsPaginatedListResponse>()
		   .ForMember(dest => dest.Task, op => op.MapFrom(src => src.Localize(src.TaskAr!, src.TaskEn!)));
		}
	}
}
