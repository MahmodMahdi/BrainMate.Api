using BrainMate.Core.Features.Events.Commands.Models;
using BrainMate.Data.Entities;


namespace BrainMate.Core.Mapping.Events;
public partial class EventProfile
{
	public void AddEventCommandMapping()
	{
		CreateMap<AddEventCommand, Event>();

	}
}