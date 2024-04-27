//using AutoMapper;
//using BrainMate.Core.Features.Medicines.Commands.Handler;
//using BrainMate.Core.Features.Medicines.Commands.Models;
//using BrainMate.Core.Mapping.Medicines;
//using BrainMate.Core.Resources;
//using BrainMate.Data.Entities;
//using BrainMate.Service.Abstracts;
//using FluentAssertions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Localization;
//using Moq;
//using System.Net;

//namespace BrainMate.XUnitTest.CoreTests.Medicines.Commands
//{
//    public class MedicineCommandHandlerTest
//    {
//        private readonly Mock<IMedicineService> _medicineServiceMock;
//        private readonly IMapper _mapperMock;
//        private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
//        private readonly MedicineProfile _medicineProfile;
//        public MedicineCommandHandlerTest()
//        {
//            _medicineServiceMock = new();
//            _localizerMock = new();
//            _medicineProfile = new();
//            var configuration = new MapperConfiguration(c => c.AddProfile(_medicineProfile));
//            _mapperMock = new Mapper(configuration);
//        }
//        [Fact]
//        public async Task Handle_AddMedicine_Should_Add_Data_And_Status201()
//        {
//            // Arrange
//            var handler = new MedicineCommandHandler(_medicineServiceMock.Object, _mapperMock, _localizerMock.Object);
//            var AddMedicineCommand = new AddMedicineCommand() { Name = "Petro", StartAt = new DateOnly(2024, 5, 23), EndAt = new DateOnly(2024, 9, 23), Frequency = 3 };
//            _medicineServiceMock.Setup(x => x.AddAsync(It.IsAny<Medicine>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("Success"));
//            // Act
//            var result = await handler.Handle(AddMedicineCommand, default);
//            // Assert
//            result.StatusCode.Should().Be(HttpStatusCode.Created);
//            result.Succeeded.Should().BeTrue();
//            _medicineServiceMock.Verify(x => x.AddAsync(It.IsAny<Medicine>(), It.IsAny<IFormFile>()), Times.Once, "Not Called");
//        }
//        [Fact]
//        public async Task Handle_AddMedicine_Should_Return_StatusCode404()
//        {
//            // Arrange
//            var handler = new MedicineCommandHandler(_medicineServiceMock.Object, _mapperMock, _localizerMock.Object);
//            var AddMedicineCommand = new AddMedicineCommand() { Name = "Petro", StartAt = new DateOnly(2024, 5, 23), EndAt = new DateOnly(2024, 9, 23), Frequency = 3 };
//            _medicineServiceMock.Setup(x => x.AddAsync(It.IsAny<Medicine>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("FailedToAdd"));
//            _medicineServiceMock.Setup(x => x.AddAsync(It.IsAny<Medicine>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("FailedToUploadImage"));
//            _medicineServiceMock.Setup(x => x.AddAsync(It.IsAny<Medicine>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("NoImage"));

//            var result = await handler.Handle(AddMedicineCommand, default);
//            // Assert
//            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
//            result.Succeeded.Should().BeFalse();
//            _medicineServiceMock.Verify(x => x.AddAsync(It.IsAny<Medicine>(), It.IsAny<IFormFile>()), Times.Once, "Not Called");
//        }
//        [Fact]
//        public async Task Handle_UpdateMedicine_Should_Return_StatusCode404()
//        {
//            // Arrange
//            var handler = new MedicineCommandHandler(_medicineServiceMock.Object, _mapperMock, _localizerMock.Object);
//            var updateMedicineCommand = new UpdateMedicineCommand() { Id = 1, Name = "Petro", StartAt = new DateOnly(2024, 5, 23), EndAt = new DateOnly(2024, 9, 23), Frequency = 3 };
//            Medicine? medicine = null;
//            int xResult = 0;
//            _medicineServiceMock.Setup(x => x.GetByIdAsync(updateMedicineCommand.Id)).Returns(Task.FromResult(medicine)!).Callback((int x) => xResult = x);
//            // Act
//            var result = await handler.Handle(updateMedicineCommand, default);
//            // Assert
//            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
//            result.Succeeded.Should().BeFalse();
//        }
//    }
//}
