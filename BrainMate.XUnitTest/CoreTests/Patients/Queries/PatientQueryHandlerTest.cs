using AutoMapper;
using BrainMate.Core.Mapping.AlzheimerPatient;
using BrainMate.Core.Resources;
using BrainMate.Service.Abstracts;
using Microsoft.Extensions.Localization;
using Moq;

namespace BrainMate.XUnitTest.CoreTests.Patients.Queries
{
    public class PatientQueryHandlerTest
    {
        private readonly Mock<IPatientService> _patientServiceMock;
        private readonly IMapper _mapperMock;
        private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
        private readonly PatientProfile _patientProfile;
        public PatientQueryHandlerTest()
        {
            _patientServiceMock = new();
            _localizerMock = new();
            _patientProfile = new();
            var configuration = new MapperConfiguration(c => c.AddProfile(_patientProfile));
            _mapperMock = new Mapper(configuration);
        }
        //[Theory]
        //[InlineData(3)]
        //public async Task Handle_PatientById_Where_Patient_NotFound_Return_StatusCode404(int id)
        //{
        //    // Arrange 
        //    // try to test a fake data	
        //    var patientList = new List<Patient>()
        //    {
        //        new Patient(){Name = "Mohamed",BirthDate = new DateOnly(2000,2,2),Address = "Tanta",PhoneNumber="01234445533",Job = "Pilot",Image="/Patient/8bc70c2ef11540b3b8725f9d2d2f7734.jpg"}
        //    };

        //    var query = new GetPatientByIdQuery();
        //    _patientServiceMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(patientList.FirstOrDefault(x => x.Id == id)!));

        //    var handler = new PatientQueryHandler(_patientServiceMock.Object, _mapperMock, _localizerMock.Object);
        //    // Act 
        //    var result = await handler.Handle(query, default);
        //    // Assert

        //    result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        //}
        //[Theory]
        //[MemberData(nameof(PassingDataToParamUsingMemberData.GetParamData), MemberType = typeof(PassingDataToParamUsingMemberData))]
        //public async Task Handle_PatientById_Where_Patient_Found_Return_StatusCode200(int id)
        //{
        //    // Arrange 
        //    // try to test a fake data
        //    var patientList = new List<Patient>()
        //    {
        //        	new Patient(){Name = "Mohamed",BirthDate = new DateOnly(2000,2,2),Address = "Tanta",PhoneNumber="01238888333",Job = "Pilot",Image="/Patient/8bc70c2ef11540b3b8725f9d2d2f7734.jpg"}
        //    };

        //    var query = new GetPatientByIdQuery();
        //    _patientServiceMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(patientList.FirstOrDefault(x => x.Id == id)!));

        //    var handler = new PatientQueryHandler(_patientServiceMock.Object, _mapperMock, _localizerMock.Object);
        //    // Act 
        //    var result = await handler.Handle(query, default);
        //    // Assert

        //    result.StatusCode.Should().Be(HttpStatusCode.OK);
        //    result.Data!.Id.Should().Be(id);
        //}
    }
}
