using AutoMapper;

namespace BrainMate.Core.Mapping.RelativesMapping
{
	public partial class RelativesProfile : Profile
	{
		public RelativesProfile()
		{
			GetRelativesPaginationMapping();
			GetRelativeByIdMapping();
			AddRelativeCommandMapping();
			UpdateRelativeCommandMapping();
		}
	}
}
