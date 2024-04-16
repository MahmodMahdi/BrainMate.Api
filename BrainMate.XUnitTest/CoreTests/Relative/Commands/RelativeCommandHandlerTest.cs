using AutoMapper;
using BrainMate.Core.Features.Relative.Commands.Handler;
using BrainMate.Core.Features.Relative.Commands.Models;
using BrainMate.Core.Mapping.Relative;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities;
using BrainMate.Service.Abstracts;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Moq;
using System.Net;

namespace BrainMate.XUnitTest.CoreTests.Relative.Commands
{
	public class RelativeCommandHandlerTest
	{
		private readonly Mock<IRelativesService> _relativeServiceMock;
		private readonly IMapper _mapperMock;
		private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
		private readonly RelativesProfile _relativeProfile;
		public RelativeCommandHandlerTest()
		{
			_relativeServiceMock = new();
			_localizerMock = new();
			_relativeProfile = new();
			var configuration = new MapperConfiguration(c => c.AddProfile(_relativeProfile));
			_mapperMock = new Mapper(configuration);
		}
		[Fact]
		public async Task Handle_AddRelative_Should_Add_Data_And_StatusCode201()
		{
			// Arrange
			var handler = new RelativeCommandHandler(_relativeServiceMock.Object, _mapperMock, _localizerMock.Object);
			var AddRelativeCommand = new AddRelativeCommand() { NameAr = "محمد", NameEn = "Mohamed", RelationShip = "Brother", RelationShipDegree = 1, Description = "The closest person to me", Age = 30, Address = "Tanta", Phone = "0123333", Job = "Pilot" };
			_relativeServiceMock.Setup(x => x.AddAsync(It.IsAny<Relatives>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("Success"));
			// Act
			var result = await handler.Handle(AddRelativeCommand, default);
			// Assert
			result.StatusCode.Should().Be(HttpStatusCode.Created);
			result.Succeeded.Should().BeTrue();
			_relativeServiceMock.Verify(x => x.AddAsync(It.IsAny<Relatives>(), It.IsAny<IFormFile>()), Times.Once, "Not Called");
		}
		[Fact]
		public async Task Handle_AddRelative_Should_Return_StatusCode404()
		{
			// Arrange
			var handler = new RelativeCommandHandler(_relativeServiceMock.Object, _mapperMock, _localizerMock.Object);
			var AddRelativeCommand = new AddRelativeCommand() { NameAr = "محمد", NameEn = "Mohamed", RelationShip = "Brother", RelationShipDegree = 1, Description = "The closest person to me", Age = 30, Address = "Tanta", Phone = "0123333", Job = "Pilot" };
			_relativeServiceMock.Setup(x => x.AddAsync(It.IsAny<Relatives>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("FailedToAdd"));
			_relativeServiceMock.Setup(x => x.AddAsync(It.IsAny<Relatives>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("FailedToUploadImage"));
			_relativeServiceMock.Setup(x => x.AddAsync(It.IsAny<Relatives>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("NoImage"));
			// Act
			var result = await handler.Handle(AddRelativeCommand, default);
			// Assert
			result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
			result.Succeeded.Should().BeFalse();
			_relativeServiceMock.Verify(x => x.AddAsync(It.IsAny<Relatives>(), It.IsAny<IFormFile>()), Times.Once, "Not Called");
		}
		[Fact]
		public async Task Handle_UpdateRelative_Should_Return_StatusCode404()
		{
			// Arrange
			var handler = new RelativeCommandHandler(_relativeServiceMock.Object, _mapperMock, _localizerMock.Object);
			var updateRelativeCommand = new UpdateRelativeCommand() { Id = 1, NameAr = "محمد", NameEn = "Mohamed", RelationShip = "Brother", RelationShipDegree = 1, Description = "The closest person to me", Age = 30, Address = "Tanta", Phone = "0123333", Job = "Pilot" };
			int xResult = 0;
			Relatives? relative = null;
			_relativeServiceMock.Setup(x => x.GetByIdAsync(updateRelativeCommand.Id)).Returns(Task.FromResult(relative)!).Callback((int x) => xResult = x);
			// Act
			var result = await handler.Handle(updateRelativeCommand, default);
			// Assert
			result.StatusCode.Should().Be(HttpStatusCode.NotFound);
			result.Succeeded.Should().BeFalse();
		}
	}
}
