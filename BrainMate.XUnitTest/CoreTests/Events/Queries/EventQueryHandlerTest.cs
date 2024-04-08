using AutoMapper;
using BrainMate.Core.Features.Events.Queries.Dtos;
using BrainMate.Core.Features.Events.Queries.Handler;
using BrainMate.Core.Features.Events.Queries.Models;
using BrainMate.Core.Mapping.Events;
using BrainMate.Core.Resources;
using BrainMate.Data.Entities;
using BrainMate.Service.Abstracts;
using BrainMate.XUnitTest.TestModels;
using EntityFrameworkCore.Testing.Common;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using System.Net;
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, MaxParallelThreads = 5)]
namespace BrainMate.XUnitTest.CoreTests.Events.Queries
{
	public class EventQueryHandlerTest
	{
		private readonly Mock<IEventService> _eventServiceMock;
		private readonly IMapper _mapperMock;
		private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
		private readonly EventProfile _eventProfile;
		public EventQueryHandlerTest()
		{
			_eventServiceMock = new();
			_localizerMock = new();
			_eventProfile = new();
			var configuration = new MapperConfiguration(c => c.AddProfile(_eventProfile));
			_mapperMock = new Mapper(configuration);
		}
		[Fact]
		public async Task Handle_EventPaginatedList_Should_NotNull_And_NotEmpty()
		{
			// Arrange 
			// try to test a fake data
			var eventList = new AsyncEnumerable<Event>(new List<Event>
			{
				new Event(){Id = 1,TaskAr="التسوق",TaskEn = "Shopping",Time = new TimeOnly(10,00)}
			});

			var query = new GetEventsPaginatedListQuery() { PageNumber = 1, PageSize = 10 };
			_eventServiceMock.Setup(x => x.FilterEventsPaginatedQueryable(query.search!)).Returns(eventList.AsQueryable());

			var handler = new EventQueryHandler(_eventServiceMock.Object, _mapperMock, _localizerMock.Object);
			// Act 
			var result = await handler.Handle(query, default);
			// Assert
			result.Data.Should().NotBeNullOrEmpty();
			result.Data.Should().BeOfType<List<GetEventsPaginatedListResponse>>();
		}
		[Theory]
		[InlineData(3)]
		public async Task Handle_EventById_Where_Event_NotFound_Return_StatusCode404(int id)
		{
			// Arrange 
			// try to test a fake data
			var eventList = new List<Event>()
			{
					new Event(){Id = 1,TaskAr="التسوق",TaskEn = "Shopping",Time=new TimeOnly(2,30)}
			};

			var query = new GetEventByIdQuery(id);
			_eventServiceMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(eventList.FirstOrDefault(x => x.Id == id)!));

			var handler = new EventQueryHandler(_eventServiceMock.Object, _mapperMock, _localizerMock.Object);
			// Act 
			var result = await handler.Handle(query, default);
			// Assert
			result.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}
		[Theory]
		//[ClassData(typeof(PassingDataUsingClassData))]
		[MemberData(nameof(PassingDataToParamUsingMemberData.GetParamData), MemberType = typeof(PassingDataToParamUsingMemberData))]
		public async Task Handle_EventById_Where_Event_Found_Return_StatusCode200(int id)
		{
			// Arrange 
			// try to test a fake data
			var eventList = new List<Event>()
			{
					new Event(){Id = 1,TaskAr="التسوق",TaskEn = "Shopping",Time=new TimeOnly(2,30)}
			};

			var query = new GetEventByIdQuery(id);
			_eventServiceMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(eventList.FirstOrDefault(x => x.Id == id)!));

			var handler = new EventQueryHandler(_eventServiceMock.Object, _mapperMock, _localizerMock.Object);
			// Act 
			var result = await handler.Handle(query, default);
			// Assert
			result.StatusCode.Should().Be(HttpStatusCode.OK);
			result.Data!.Id.Should().Be(id);
		}
	}
}
