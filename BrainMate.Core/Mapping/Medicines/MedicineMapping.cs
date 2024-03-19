using AutoMapper;

namespace BrainMate.Core.Mapping.Medicines
{
	public partial class MedicineMapping : Profile
	{
		public MedicineMapping()
		{
			GetMedicinePaginationMapping();
		}
	}
}
