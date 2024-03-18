using AutoMapper;
using BrainMate.Core.Features.AlzheimerPatient.Queries.Dto;
using BrainMate.Data.Entities;

namespace BrainMate.Core.Mapping.AlzheimerPatient
{
	public partial class PatientProfile : Profile
	{
		public void GetPatientByIdMapping()
		{
			CreateMap<Patient, GetPatientResponse>()
			.ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)));
		}
	}
}
