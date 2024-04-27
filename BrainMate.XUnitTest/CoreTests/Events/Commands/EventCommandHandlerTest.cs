//using AutoMapper;
//using BrainMate.Core.Features.Events.Commands.Handler;
//using BrainMate.Core.Features.Events.Commands.Models;
//using BrainMate.Core.Mapping.Events;
//using BrainMate.Core.Resources;
//using BrainMate.Data.Entities;
//using BrainMate.Service.Abstracts;
//using FluentAssertions;
//using Microsoft.Extensions.Localization;
//using Moq;
//using System.Net;

//namespace BrainMate.XUnitTest.CoreTests.Events.Commands
//{
//    public class EventCommandHandlerTest
//    {
//        private readonly Mock<IEventService> _eventServiceMock;
//        private readonly IMapper _mapperMock;
//        private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
//        private readonly EventProfile _eventProfile;

//        public EventCommandHandlerTest()
//        {
//            _eventServiceMock = new();
//            _localizerMock = new();
//            _eventProfile = new();
//            var configuration = new MapperConfiguration(c => c.AddProfile(_eventProfile));
//            _mapperMock = new Mapper(configuration);
//        }
//        [Fact]
//        public async Task Handle_AddEvent_Should_Add_Data_And_StatusCode201()
//        {
//            // Arrange
//            var handler = new EventCommandHandler(_eventServiceMock.Object, _mapperMock, _localizerMock.Object);
//            var addEventCommand = new AddEventCommand() { Task = "Shopping", Time = new TimeOnly(10, 00) };
//            _eventServiceMock.Setup(x => x.AddAsync(It.IsAny<Event>())).Returns(Task.FromResult("Success"));
//            // Act
//            var result = await handler.Handle(addEventCommand, default);
//            // Assert
//            result.StatusCode.Should().Be(HttpStatusCode.Created);
//            result.Succeeded.Should().BeTrue();
//            _eventServiceMock.Verify(x => x.AddAsync(It.IsAny<Event>()), Times.Once, "Not Called");
//        }
//        [Fact]
//        public async Task Handle_AddEvent_Should_Return_StatusCode404()
//        {
//            // Arrange
//            var handler = new EventCommandHandler(_eventServiceMock.Object, _mapperMock, _localizerMock.Object);
//            var addEventCommand = new AddEventCommand() { Task = "Shopping", Time = new TimeOnly(10, 00) };
//            _eventServiceMock.Setup(x => x.AddAsync(It.IsAny<Event>())).Returns(Task.FromResult("FailedToAdd"));
//            // Act
//            var result = await handler.Handle(addEventCommand, default);
//            // Assert
//            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
//            result.Succeeded.Should().BeFalse();
//            _eventServiceMock.Verify(x => x.AddAsync(It.IsAny<Event>()), Times.Once, "Not Called");
//        }
//        [Fact]
//        public async Task Handle_UpdateEvent_Should_Return_StatusCode404()
//        {
//            // Arrange
//            var handler = new EventCommandHandler(_eventServiceMock.Object, _mapperMock, _localizerMock.Object);
//            var updateEventCommand = new UpdateEventCommand() { Task = "Shopping", Time = new TimeOnly(10, 00) };
//            Event? Event = null;
//            int xResult = 0;
//            _eventServiceMock.Setup(x => x.GetByIdAsync(updateEventCommand.Id)).Returns(Task.FromResult(Event)!).Callback((int x) => xResult = x);
//            // Act
//            var result = await handler.Handle(updateEventCommand, default);
//            // Assert
//            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
//            result.Succeeded.Should().BeFalse();
//        }
//    }
//}
