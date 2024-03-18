using BrainMate.Core.Features.Relatives.Queries.Dtos;
using BrainMate.Data.Entities;

namespace BrainMate.Core.Mapping.RelativesMapping
{
	public partial class RelativesProfile
	{
		public void GetRelativesPaginationMapping()
		{
			CreateMap<Relatives, GetRelativesPaginatedListResponse>()
		   .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)));
		}
	}
}
