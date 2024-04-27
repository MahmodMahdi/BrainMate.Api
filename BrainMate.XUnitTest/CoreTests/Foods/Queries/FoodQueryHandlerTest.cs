//using AutoMapper;
//using BrainMate.Core.Features.Foods.Queries.Dtos;
//using BrainMate.Core.Features.Foods.Queries.Handler;
//using BrainMate.Core.Features.Foods.Queries.Models;
//using BrainMate.Core.Mapping.Foods;
//using BrainMate.Core.Resources;
//using BrainMate.Data.Entities;
//using BrainMate.Service.Abstracts;
//using BrainMate.XUnitTest.TestModels;
//using FluentAssertions;
//using Microsoft.Extensions.Localization;
//using Moq;
//using System.Net;

//namespace BrainMate.XUnitTest.CoreTests.Foods.Queries
//{
//    public class FoodQueryHandlerTest
//    {
//        private readonly Mock<IFoodService> _foodServiceMock;
//        private readonly IMapper _mapperMock;
//        private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
//        private readonly FoodProfile _foodProfile;
//        public FoodQueryHandlerTest()
//        {
//            _foodServiceMock = new();
//            _localizerMock = new();
//            _foodProfile = new();
//            var configuration = new MapperConfiguration(c => c.AddProfile(_foodProfile));
//            _mapperMock = new Mapper(configuration);
//        }
//        [Fact]
//        public async Task Handle_FoodList_Should_NotNull_And_NotEmpty()
//        {
//            // try to test a fake data
//            var foodList = new List<Food>()
//            {
//                new Food(){Id = 1,Name = "Salad",Type= "Breakfast",Time= new TimeOnly(10,30),Image="/Foods/8bc70c2ef11540b3b8725f9d2d2f7734.jpg"}
//            };
//            // Arrange 
//            var query = new GetFoodListQuery();
//            _foodServiceMock.Setup(x => x.GetAllAsync()).Returns(Task.FromResult(foodList));

//            var handler = new FoodQueryHandler(_foodServiceMock.Object, _mapperMock, _localizerMock.Object);
//            // Act 
//            var result = await handler.Handle(query, default);
//            // Assert
//            result.Data.Should().NotBeNullOrEmpty();
//            result.Succeeded.Should().BeTrue();
//            result.Data.Should().BeOfType<List<GetFoodListResponse>>();
//        }
//        [Theory]
//        [InlineData(3)]
//        public async Task Handle_FoodById_Where_Food_NotFound_Return_StatusCode404(int id)
//        {
//            // try to test a fake data
//            var foodList = new List<Food>()
//            {
//                new Food(){Id = 1,Name = "Salad",Type= "Breakfast",Time= new TimeOnly(10,30),Image="/Foods/8bc70c2ef11540b3b8725f9d2d2f7734.jpg"},
//                new Food(){Id = 2,Name = "Meats",Type= "dinner",Time= new TimeOnly(6,30),Image="/Foods/8bc70c2ef11534j7b8725f9d2d2f7734.jpg"},
//            };
//            // Arrange 
//            var query = new GetFoodByIdQuery(id);
//            _foodServiceMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(foodList.FirstOrDefault(x => x.Id == id)!));

//            var handler = new FoodQueryHandler(_foodServiceMock.Object, _mapperMock, _localizerMock.Object);
//            // Act 
//            var result = await handler.Handle(query, default);
//            // Assert
//            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
//        }
//        [Theory]
//        [MemberData(nameof(PassingDataToParamUsingMemberData.GetParamData), MemberType = typeof(PassingDataToParamUsingMemberData))]
//        public async Task Handle_FoodById_Where_Food_Found_Return_StatusCode200(int id)
//        {
//            // try to test a fake data
//            var foodList = new List<Food>()
//            {
//                new Food(){Id = 1,Name = "Salad",Type= "Breakfast",Time= new TimeOnly(10,30),Image="/Foods/8bc70c2ef11540b3b8725f9d2d2f7734.jpg"},
//                new Food(){Id = 2,Name = "Meats",Type= "dinner",Time= new TimeOnly(6,30),Image="/Foods/8bc70c2ef11534j7b8725f9d2d2f7734.jpg"},
//            };
//            // Arrange 
//            var query = new GetFoodByIdQuery(id);
//            _foodServiceMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(foodList.FirstOrDefault(x => x.Id == id)!));

//            var handler = new FoodQueryHandler(_foodServiceMock.Object, _mapperMock, _localizerMock.Object);
//            // Act 
//            var result = await handler.Handle(query, default);
//            // Assert
//            result.StatusCode.Should().Be(HttpStatusCode.OK);
//            result.Data!.Id.Should().Be(id);
//        }
//    }
//}
