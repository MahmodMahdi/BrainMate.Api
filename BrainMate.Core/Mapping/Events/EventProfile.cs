using AutoMapper;

namespace BrainMate.Core.Mapping.Events
{
	public partial class EventProfile : Profile
	{
		public EventProfile()
		{
			GetEventsPaginationMapping();
			GetEventByIdMapping();
			AddEventCommandMapping();
			UpdateEventCommandMapping();
		}
	}
}
