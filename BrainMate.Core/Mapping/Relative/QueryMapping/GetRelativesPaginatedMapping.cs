using BrainMate.Core.Features.Relative.Queries.Dtos;
using BrainMate.Data.Entities;

namespace BrainMate.Core.Mapping.Relative
{
    public partial class RelativesProfile
    {
        public void GetRelativesPaginationMapping()
        {
            CreateMap<Relatives, GetRelativesPaginatedListResponse>();
        }
    }
}
