using BrainMate.Core.Wrappers;
using BrainMate.Data.Entities;
using BrainMate.XUnitTest.Wrappers.Interfaces;
using EntityFrameworkCore.Testing.Common;
using FluentAssertions;
using Moq;
namespace BrainMate.XUnitTest.ServiceTest.ExtensionMethod
{
    public class ExtensionMethodTest
    {
        private readonly Mock<IPaginatedService<Event>> _eventPaginatedServiceMock;
        private readonly Mock<IPaginatedService<Medicine>> _medicinePaginatedServiceMock;
        private readonly Mock<IPaginatedService<Relatives>> _relativePaginatedServiceMock;
        public ExtensionMethodTest()
        {
            _eventPaginatedServiceMock = new();
            _medicinePaginatedServiceMock = new();
            _relativePaginatedServiceMock = new();
        }
        [Theory]
        [InlineData(1, 10)]

        public async Task Event_ToPaginatedListAsync_Should_Return_List(int pageNumber, int pageSize)
        {
            //Arrange
            // try to test a fake data
            var eventList = new AsyncEnumerable<Event>(new List<Event>
            {
                new Event(){Id = 1,Task = "Shopping",Time=new TimeOnly(2,30)}
            });
            var PaginatedResult = new PaginateResult<Event>(eventList.ToList());
            _eventPaginatedServiceMock.Setup(x => x.ReturnPaginatedResult(eventList, pageNumber, pageSize)).Returns(Task.FromResult(PaginatedResult));
            //Act
            var result = await _eventPaginatedServiceMock.Object.ReturnPaginatedResult(eventList, pageNumber, pageSize);
            //Assert
            result.Data.Should().NotBeNullOrEmpty();
        }
        [Theory]
        [InlineData(1, 10)]

        public async Task Medicine_ToPaginatedListAsync_Should_Return_List(int pageNumber, int pageSize)
        {
            //Arrange
            // try to test a fake data
            var medicineList = new AsyncEnumerable<Medicine>(new List<Medicine>
            {
                new Medicine(){Id = 1,Name = "Petro",Image = "/Medicines/8bc70c2ef11540b3b8725f9d2d2f7734.jpg",StartAt=new DateOnly(2024,5,23),EndAt= new DateOnly(2024,9,23),Frequency=3}
            });
            var PaginatedResult = new PaginateResult<Medicine>(medicineList.ToList());
            _medicinePaginatedServiceMock.Setup(x => x.ReturnPaginatedResult(medicineList, pageNumber, pageSize)).Returns(Task.FromResult(PaginatedResult));
            //Act
            var result = await _medicinePaginatedServiceMock.Object.ReturnPaginatedResult(medicineList, pageNumber, pageSize);
            //Assert
            result.Data.Should().NotBeNullOrEmpty();
        }
        [Theory]
        [InlineData(1, 10)]

        public async Task Relative_ToPaginatedListAsync_Should_Return_List(int pageNumber, int pageSize)
        {
            //Arrange
            // try to test a fake data
            var relativeList = new AsyncEnumerable<Relatives>(new List<Relatives>
            {
                new Relatives(){Id = 1,Name = "Mohamed",RelationShip= "Brother",RelationShipDegree=1,Description="The closest person to me",Age = 30,Address = "Tanta",PhoneNumber="0123333",Job = "Pilot",Image="/Foods/8bc70c2ef11540b3b8725f9d2d2f7734.jpg"}
            });
            var PaginatedResult = new PaginateResult<Relatives>(relativeList.ToList());
            _relativePaginatedServiceMock.Setup(x => x.ReturnPaginatedResult(relativeList, pageNumber, pageSize)).Returns(Task.FromResult(PaginatedResult));
            //Act
            var result = await _relativePaginatedServiceMock.Object.ReturnPaginatedResult(relativeList, pageNumber, pageSize);
            //Assert
            result.Data.Should().NotBeNullOrEmpty();
        }
    }
}
