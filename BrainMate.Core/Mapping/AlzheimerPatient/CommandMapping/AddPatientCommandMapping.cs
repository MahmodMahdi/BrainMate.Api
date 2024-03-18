using AutoMapper;
using BrainMate.Core.Features.AlzheimerPatient.Commands.Models;
using BrainMate.Data.Entities;


namespace BrainMate.Core.Mapping.AlzheimerPatient
{
	public partial class PatientProfile : Profile
	{
		public void AddPatientCommandMapping()
		{
			CreateMap<AddPatientCommand, Patient>()
				.ForMember(dest => dest.Image, op => op.Ignore())
				.ForMember(dest => dest.NameAr, op => op.MapFrom(src => src.NameAr))
				.ForMember(dest => dest.NameEn, op => op.MapFrom(src => src.NameEn));
		}
	}
}
