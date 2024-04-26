using BrainMate.Core.Features.Relative.Queries.Dtos;
using BrainMate.Data.Entities;

namespace BrainMate.Core.Mapping.Relative
{
    public partial class RelativesProfile
    {
        public void GetRelativeByIdMapping()
        {
            CreateMap<Relatives, GetRelativesResponse>();
        }
    }
}
