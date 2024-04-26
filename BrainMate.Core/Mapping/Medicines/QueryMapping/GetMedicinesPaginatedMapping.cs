using BrainMate.Core.Features.Medicines.Queries.Dtos;
using BrainMate.Data.Entities;


namespace BrainMate.Core.Mapping.Medicines
{
    public partial class MedicineProfile
    {
        public void GetMedicinePaginationMapping()
        {
            CreateMap<Medicine, GetMedicinePaginatedListResponse>();
        }
    }
}
