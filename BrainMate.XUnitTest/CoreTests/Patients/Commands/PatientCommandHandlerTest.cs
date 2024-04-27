//using AutoMapper;
//using BrainMate.Core.Mapping.AlzheimerPatient;
//using BrainMate.Core.Resources;
//using BrainMate.Service.Abstracts;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Localization;
//using Moq;

//namespace BrainMate.XUnitTest.CoreTests.Patients.Commands
//{
//    public class PatientCommandHandlerTest
//    {
//        private readonly Mock<IPatientService> _patientServiceMock;
//        private readonly IMapper _mapperMock;
//        private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
//        private readonly PatientProfile _patientProfile;
//        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
//        public PatientCommandHandlerTest()
//        {
//            _mockHttpContextAccessor = new();
//            _patientServiceMock = new();
//            _localizerMock = new();
//            _patientProfile = new();
//            var configuration = new MapperConfiguration(c => c.AddProfile(_patientProfile));
//            _mapperMock = new Mapper(configuration);
//        }

//        //[Fact]
//        //public async Task Handle_UpdatePatient_Should_Return_StatusCode404()
//        //{
//        //    // Arrange
//        //    var handler = new PatientCommandHandler(_patientServiceMock.Object, _mapperMock, _localizerMock.Object,_mockHttpContextAccessor.Object);
//        //    var updatePatientCommand = new UpdatePatientCommand() { Name = "محمد", BirthDate = new DateOnly(2000, 2, 2), Address = "Tanta", Phone = "01234445533", Job = "Pilot" };
//        //    Patient? patient = null;
//        //    int xResult = 0;
//        //    _patientServiceMock.Setup(x => x.GetByIdAsync(updatePatientCommand.Id)).Returns(Task.FromResult(patient)!).Callback((int x) => xResult = x);
//        //    // Act
//        //    var result = await handler.Handle(updatePatientCommand, default);
//        //    // Assert
//        //    result.StatusCode.Should().Be(HttpStatusCode.NotFound);
//        //    result.Succeeded.Should().BeFalse();
//        //}
//    }
//}
