using AutoMapper;
using BrainMate.Core.Features.AlzheimerPatient.Commands.Handler;
using BrainMate.Core.Features.AlzheimerPatient.Commands.Models;
using BrainMate.Core.Mapping.AlzheimerPatient;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities;
using BrainMate.Service.Abstracts;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Moq;
using System.Net;

namespace BrainMate.XUnitTest.CoreTests.Patients.Commands
{
	public class PatientCommandHandlerTest
	{
		private readonly Mock<IPatientService> _patientServiceMock;
		private readonly IMapper _mapperMock;
		private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
		private readonly PatientProfile _patientProfile;
		public PatientCommandHandlerTest()
		{
			_patientServiceMock = new();
			_localizerMock = new();
			_patientProfile = new();
			var configuration = new MapperConfiguration(c => c.AddProfile(_patientProfile));
			_mapperMock = new Mapper(configuration);
		}
		[Fact]
		public async Task Handle_AddPatient_Should_Add_Data_And_StatusCode201()
		{
			// Arrange
			var handler = new PatientCommandHandler(_patientServiceMock.Object, _mapperMock, _localizerMock.Object);
			var addPatientCommand = new AddPatientCommand() { NameAr = "محمد", NameEn = "Mohamed", BirthDate = new DateOnly(2000, 2, 2), Address = "Tanta", Phone = "01234445533", Job = "Pilot" };
			_patientServiceMock.Setup(x => x.AddAsync(It.IsAny<Patient>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("Success"));
			// Act
			var result = await handler.Handle(addPatientCommand, default);
			// Assert
			result.StatusCode.Should().Be(HttpStatusCode.Created);
			result.Succeeded.Should().BeTrue();
			_patientServiceMock.Verify(x => x.AddAsync(It.IsAny<Patient>(), It.IsAny<IFormFile>()), Times.Once, "Not Called");
		}
		[Fact]
		public async Task Handle_AddPatient_Should_Return_StatusCode404()
		{
			// Arrange
			var handler = new PatientCommandHandler(_patientServiceMock.Object, _mapperMock, _localizerMock.Object);
			var addPatientCommand = new AddPatientCommand() { NameAr = "محمد", NameEn = "Mohamed", BirthDate = new DateOnly(2000, 2, 2), Address = "Tanta", Phone = "01234445533", Job = "Pilot" };
			_patientServiceMock.Setup(x => x.AddAsync(It.IsAny<Patient>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("FailedToAdd"));
			_patientServiceMock.Setup(x => x.AddAsync(It.IsAny<Patient>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("FailedToUploadImage"));
			_patientServiceMock.Setup(x => x.AddAsync(It.IsAny<Patient>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("NoImage"));
			// Act
			var result = await handler.Handle(addPatientCommand, default);
			// Assert
			result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
			result.Succeeded.Should().BeFalse();
			_patientServiceMock.Verify(x => x.AddAsync(It.IsAny<Patient>(), It.IsAny<IFormFile>()), Times.Once, "Not Called");
		}
		[Fact]
		public async Task Handle_UpdatePatient_Should_Return_StatusCode404()
		{
			// Arrange
			var handler = new PatientCommandHandler(_patientServiceMock.Object, _mapperMock, _localizerMock.Object);
			var updatePatientCommand = new UpdatePatientCommand() { Id = 2, NameAr = "محمد", NameEn = "Mohamed", BirthDate = new DateOnly(2000, 2, 2), Address = "Tanta", Phone = "01234445533", Job = "Pilot" };
			Patient? patient = null;
			int xResult = 0;
			_patientServiceMock.Setup(x => x.GetByIdAsync(updatePatientCommand.Id)).Returns(Task.FromResult(patient)!).Callback((int x) => xResult = x);
			// Act
			var result = await handler.Handle(updatePatientCommand, default);
			// Assert
			result.StatusCode.Should().Be(HttpStatusCode.NotFound);
			result.Succeeded.Should().BeFalse();
		}
	}
}
