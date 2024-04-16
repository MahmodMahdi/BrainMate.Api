using AutoMapper;
using BrainMate.Core.Features.Foods.Commands.Handler;
using BrainMate.Core.Features.Foods.Commands.Models;
using BrainMate.Core.Mapping.Foods;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities;
using BrainMate.Service.Abstracts;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Moq;
using System.Net;

namespace BrainMate.XUnitTest.CoreTests.Foods.Commands
{
	public class FoodCommandHandlerTest
	{
		private readonly Mock<IFoodService> _foodServiceMock;
		private readonly IMapper _mapperMock;
		private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
		private readonly FoodProfile _foodProfile;
		public FoodCommandHandlerTest()
		{
			_foodServiceMock = new();
			_localizerMock = new();
			_foodProfile = new();
			var configuration = new MapperConfiguration(c => c.AddProfile(_foodProfile));
			_mapperMock = new Mapper(configuration);
		}
		[Fact]
		public async Task Handle_AddFood_Should_Add_Data_And_StatusCode201()
		{
			// Arrange
			var handler = new FoodCommandHandler(_foodServiceMock.Object, _mapperMock, _localizerMock.Object);
			var addFoodCommand = new AddFoodCommand() { NameAr = "سلطة", NameEn = "Salad", Type = "Breakfast", Time = new TimeOnly(10, 30) };
			_foodServiceMock.Setup(x => x.AddAsync(It.IsAny<Food>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("Success"));
			// Act
			var result = await handler.Handle(addFoodCommand, default);
			// Assert
			result.StatusCode.Should().Be(HttpStatusCode.Created);
			result.Succeeded.Should().BeTrue();
			_foodServiceMock.Verify(x => x.AddAsync(It.IsAny<Food>(), It.IsAny<IFormFile>()), Times.Once, "Not Called");
		}
		[Fact]
		public async Task Handle_AddFood_Should_Return_StatusCode404()
		{
			// Arrange
			var handler = new FoodCommandHandler(_foodServiceMock.Object, _mapperMock, _localizerMock.Object);
			var addFoodCommand = new AddFoodCommand() { NameAr = "سلطة", NameEn = "Salad", Type = "Breakfast", Time = new TimeOnly(10, 30) };
			_foodServiceMock.Setup(x => x.AddAsync(It.IsAny<Food>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("FailedToAdd"));
			_foodServiceMock.Setup(x => x.AddAsync(It.IsAny<Food>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("FailedToUploadImage"));
			_foodServiceMock.Setup(x => x.AddAsync(It.IsAny<Food>(), It.IsAny<IFormFile>())).Returns(Task.FromResult("NoImage"));
			// Act
			var result = await handler.Handle(addFoodCommand, default);
			// Assert
			result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
			result.Succeeded.Should().BeFalse();
			_foodServiceMock.Verify(x => x.AddAsync(It.IsAny<Food>(), It.IsAny<IFormFile>()), Times.Once, "Not Called");
		}
		[Fact]
		public async Task Handle_UpdateFood_Should_Return_StatusCode404()
		{
			// Arrange
			var handler = new FoodCommandHandler(_foodServiceMock.Object, _mapperMock, _localizerMock.Object);
			var updateFoodCommand = new UpdateFoodCommand() { Id = 1, NameAr = "سلطة", NameEn = "Salad", Type = "Breakfast", Time = new TimeOnly(10, 30) };
			Food? food = null;
			int xResult = 0;
			_foodServiceMock.Setup(x => x.GetByIdAsync(updateFoodCommand.Id)).Returns(Task.FromResult(food)!).Callback((int x) => xResult = x);
			// Act
			var result = await handler.Handle(updateFoodCommand, default);
			// Assert
			result.StatusCode.Should().Be(HttpStatusCode.NotFound);
			result.Succeeded.Should().BeFalse();
		}
	}
}
