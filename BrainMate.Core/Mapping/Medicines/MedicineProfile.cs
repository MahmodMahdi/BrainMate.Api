using AutoMapper;

namespace BrainMate.Core.Mapping.Medicines
{
    public partial class MedicineProfile : Profile
    {
        public MedicineProfile()
        {
            GetMedicinePaginationMapping();
            GetMedicineByIdMapping();
            AddMedicineCommandMapping();
            UpdateMedicineCommandMapping();
        }
    }
}
