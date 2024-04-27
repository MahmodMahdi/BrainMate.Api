//using AutoMapper;
//using BrainMate.Core.Features.Relative.Queries.Handler;
//using BrainMate.Core.Features.Relative.Queries.Models;
//using BrainMate.Core.Mapping.Relative;
//using BrainMate.Core.Resources;
//using BrainMate.Data.Entities;
//using BrainMate.Service.Abstracts;
//using BrainMate.XUnitTest.TestModels;
//using FluentAssertions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Localization;
//using Moq;
//using System.Net;

//namespace BrainMate.XUnitTest.CoreTests.Relative.Queries
//{
//    public class RelativeQueryHandlerTest
//    {
//        private readonly Mock<IRelativesService> _relativeServiceMock;
//        private readonly IMapper _mapperMock;
//        private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
//        private readonly RelativesProfile _relativeProfile;
//        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
//        public RelativeQueryHandlerTest()
//        {
//            _httpContextAccessor = new();
//            _relativeServiceMock = new();
//            _localizerMock = new();
//            _relativeProfile = new();
//            var configuration = new MapperConfiguration(c => c.AddProfile(_relativeProfile));
//            _mapperMock = new Mapper(configuration);
//        }
//        //[Fact]
//        //public async Task Handle_RelativesPaginatedList_Should_NotNull_And_NotEmpty()
//        //{
//        //	// try to test a fake data
//        //	var relativeList = new AsyncEnumerable<Relatives>(new List<Relatives>
//        //	{
//        //		new Relatives(){Id = 1,NameAr="محمد",NameEn = "Mohamed",RelationShip= "Brother",RelationShipDegree=1,Description="The closest person to me",Age = 30,Address = "Tanta",Phone="0123333",Job = "Pilot",Image="/Relatives/8bc70c2ef11540b3b8725f9d2d2f7734.jpg"}
//        //	});
//        //	// Arrange 
//        //	var query = new GetRelativesPaginatedListQuery() { PageNumber = 1, PageSize = 10 };
//        //	_relativeServiceMock.Setup(x => x.FilterRelativesPaginatedQueryable(query.Search!)).Returns(relativeList.AsQueryable());

//        //	var handler = new RelativesQueryHandler(_relativeServiceMock.Object, _mapperMock, _localizerMock.Object);
//        //	// Act 
//        //	var result = await handler.Handle(query, default);
//        //	// Assert
//        //	result.Data.Should().NotBeNullOrEmpty();
//        //	result.Data.Should().BeOfType<List<GetRelativesPaginatedListResponse>>();
//        //}
//        [Theory]
//        [InlineData(3)]
//        public async Task Handle_RelativeById_Where_Relative_NotFound_Return_StatusCode404(int id)
//        {
//            //Arrange
//            // try to test a fake data
//            var relativeList = new List<Relatives>()
//            {
//                new Relatives(){Id = 1,Name = "Mohamed",RelationShip= "Brother",RelationShipDegree=1,Description="The closest person to me",Age = 30,Address = "Tanta",Phone="0123333",Job = "Pilot",Image="/Relative/8bc70c2ef11540b3b8725f9d2d2f7734.jpg"}
//            };
//            var query = new GetRelativesByIdQuery(id);
//            _relativeServiceMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(relativeList.FirstOrDefault(x => x.Id == id)!));

//            var handler = new RelativesQueryHandler(_relativeServiceMock.Object, _mapperMock, _localizerMock.Object, _httpContextAccessor.Object);
//            //Act
//            var result = await handler.Handle(query, default);
//            //Assert

//            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
//        }
//        [Theory]
//        // try to pass a memberData
//        [MemberData(nameof(PassingDataToParamUsingMemberData.GetParamData), MemberType = typeof(PassingDataToParamUsingMemberData))]
//        public async Task Handle_PatientById_Where_Patient_Found_Return_StatusCode200(int id)
//        {
//            //	Arrange
//            // try to test a fake data
//            var relativeList = new List<Relatives>()
//            {
//                new Relatives(){Id = 1,Name = "Mohamed",RelationShip= "Brother",RelationShipDegree=1,Description="The closest person to me",Age = 30,Address = "Tanta",Phone="0123333",Job = "Pilot",Image="/Foods/8bc70c2ef11540b3b8725f9d2d2f7734.jpg"}
//            };

//            var query = new GetRelativesByIdQuery(id);
//            _relativeServiceMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(relativeList.FirstOrDefault(x => x.Id == id)!));

//            var handler = new RelativesQueryHandler(_relativeServiceMock.Object, _mapperMock, _localizerMock.Object, _httpContextAccessor.Object);
//            //Act
//            var result = await handler.Handle(query, default);
//            //Assert
//            result.StatusCode.Should().Be(HttpStatusCode.OK);
//            result.Data!.Id.Should().Be(id);
//        }
//    }
//}
