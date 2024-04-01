using AutoMapper;

namespace BrainMate.Core.Mapping.Relative
{
	public partial class RelativesProfile : Profile
	{
		public RelativesProfile()
		{
			GetRelativesPaginationMapping();
			GetRelativeByIdMapping();
			AddRelativeCommandMapping();
			UpdateRelativeCommandMapping();
			SearchRelativesMapping();
		}
	}
}
