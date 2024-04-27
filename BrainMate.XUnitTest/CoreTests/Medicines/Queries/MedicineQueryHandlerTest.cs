//using AutoMapper;
//using BrainMate.Core.Features.Medicines.Queries.Handler;
//using BrainMate.Core.Features.Medicines.Queries.Modes;
//using BrainMate.Core.Mapping.Medicines;
//using BrainMate.Core.Resources;
//using BrainMate.Data.Entities;
//using BrainMate.Service.Abstracts;
//using BrainMate.XUnitTest.TestModels;
//using FluentAssertions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Localization;
//using Moq;
//using System.Net;

//namespace BrainMate.XUnitTest.CoreTests.Medicines.Queries
//{
//    public class MedicineQueryHandlerTest
//    {
//        private readonly Mock<IMedicineService> _medicineServiceMock;
//        private readonly IMapper _mapperMock;
//        private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
//        private readonly MedicineProfile _medicineProfile;
//        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
//        public MedicineQueryHandlerTest()
//        {
//            _httpContextAccessor = new();
//            _medicineServiceMock = new();
//            _localizerMock = new();
//            _medicineProfile = new();
//            var configuration = new MapperConfiguration(c => c.AddProfile(_medicineProfile));
//            _mapperMock = new Mapper(configuration);
//        }
//        //[Fact]
//        //public async Task Handle_MedicinePaginatedList_Should_NotNull_And_NotEmpty()
//        //{
//        //	// try to test a fake data
//        //	var medicineList = new AsyncEnumerable<Medicine>(new List<Medicine>
//        //	{
//        //		new Medicine(){Id = 1,NameAr="بترو",NameEn = "Petro",Image = "/Medicines/8bc70c2ef11540b3b8725f9d2d2f7734.jpg",StartAt=new DateOnly(2024,5,23),EndAt= new DateOnly(2024,9,23),Frequency=3}
//        //	});
//        //	// Arrange 
//        //	var query = new GetMedicinePaginatedListQuery() { PageNumber = 1, PageSize = 10 };
//        //	_medicineServiceMock.Setup(x => x.FilterMedicinesPaginatedQueryable(query.search!)).Returns(medicineList.AsQueryable());
//        //	var handler = new MedicineQueryHandler(_medicineServiceMock.Object, _mapperMock, _localizerMock.Object, _httpContextAccessor.Object);

//        //	// Act 
//        //	var result = await handler.Handle(query, default);
//        //	// Assert
//        //	result.Data.Should().NotBeNullOrEmpty();
//        //	result.Data.Should().BeOfType<List<GetMedicinePaginatedListResponse>>();
//        //}
//        [Theory]
//        [InlineData(3)]
//        //[InlineData(2)]
//        public async Task Handle_MedicineById_Where_Medicine_NotFound_Return_StatusCode404(int id)
//        {
//            // Arrange 
//            // try to test a fake data
//            var medicineList = new List<Medicine>()
//            {
//                    new Medicine(){Id = 1,Name = "Petro",Image = "/Medicines/8bc70c2ef11540b3b8725f9d2d2f7734.jpg",StartAt=new DateOnly(2024,5,23),EndAt= new DateOnly(2024,9,23),Frequency=3}
//            };

//            var query = new GetMedicineByIdQuery(id);
//            _medicineServiceMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(medicineList.FirstOrDefault(x => x.Id == id)!));

//            var handler = new MedicineQueryHandler(_medicineServiceMock.Object, _mapperMock, _localizerMock.Object, _httpContextAccessor.Object);
//            // Act 
//            var result = await handler.Handle(query, default);
//            // Assert
//            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
//        }
//        [Theory]
//        [MemberData(nameof(PassingDataToParamUsingMemberData.GetParamData), MemberType = typeof(PassingDataToParamUsingMemberData))]
//        public async Task Handle_MedicineById_Where_Medicine_Found_Return_StatusCode200(int id)
//        {
//            // Arrange 
//            // try to test a fake data
//            var medicineList = new List<Medicine>()
//            {
//                    new Medicine(){Id = 1,Name = "Petro",Image = "/Medicines/8bc70c2ef11540b3b8725f9d2d2f7734.jpg",StartAt=new DateOnly(2024,5,23),EndAt= new DateOnly(2024,9,23),Frequency=3}
//            };

//            var query = new GetMedicineByIdQuery(id);
//            _medicineServiceMock.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(medicineList.FirstOrDefault(x => x.Id == id)!));

//            var handler = new MedicineQueryHandler(_medicineServiceMock.Object, _mapperMock, _localizerMock.Object, _httpContextAccessor.Object);
//            // Act 
//            var result = await handler.Handle(query, default);
//            // Assert

//            result.StatusCode.Should().Be(HttpStatusCode.OK);
//            result.Data!.Id.Should().Be(id);
//        }
//    }
//}
