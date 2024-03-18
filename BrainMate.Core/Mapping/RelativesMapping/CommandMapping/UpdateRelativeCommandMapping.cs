using BrainMate.Core.Features.Relative.Commands.Models;
using BrainMate.Data.Entities;
namespace BrainMate.Core.Mapping.RelativesMapping;
public partial class RelativesProfile
{
	public void UpdateRelativeCommandMapping()
	{
		CreateMap<UpdateRelativeCommand, Relatives>()
				.ForMember(dest => dest.Image, op => op.Ignore());
	}
}
