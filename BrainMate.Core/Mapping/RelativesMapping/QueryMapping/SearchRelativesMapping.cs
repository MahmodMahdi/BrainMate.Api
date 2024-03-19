using BrainMate.Core.Features.Relative.Queries.Dtos;
using BrainMate.Data.Entities;

namespace BrainMate.Core.Mapping.RelativesMapping;
public partial class RelativesProfile
{
	public void SearchRelativesMapping()
	{
		CreateMap<Relatives, SearchRelativesResponse>()
	   .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)));
	}
}
