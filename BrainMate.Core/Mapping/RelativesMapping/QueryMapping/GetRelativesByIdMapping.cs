using BrainMate.Core.Features.Relatives.Queries.Dtos;
using BrainMate.Data.Entities;

namespace BrainMate.Core.Mapping.RelativesMapping
{
	public partial class RelativesProfile
	{
		public void GetRelativeByIdMapping()
		{
			CreateMap<Relatives, GetRelativesResponse>()
		   .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)));
		}
	}
}
