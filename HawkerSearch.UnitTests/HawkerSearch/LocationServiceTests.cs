using AutoMapper;
using DataLoad.UnitTests.Attributes;
using HawkerSearch.Domain;
using HawkerSearch.Web.Interfaces;
using HawkerSearch.Web.Mapper;
using HawkerSearch.Web.Models;
using HawkerSearch.Web.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NetTopologySuite.Geometries;

namespace HawkerSearch.UnitTests.HawkerSearch
{
    public class LocationServiceTests
    {
        protected readonly Mock<IHawkerRepository> _mockRepository;
        protected readonly Mock<ILogger<LocationService>> _mockLogger;
        protected readonly ILocationService _sut;
        private IMapper _mapper;

        public IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.AddProfile(new MappingProfile());
                    });
                    _mapper = config.CreateMapper();
                }
                return _mapper;
            }
        }


        public LocationServiceTests()
        {
            _mockRepository = new Mock<IHawkerRepository>();
            _mockLogger = new Mock<ILogger<LocationService>>();
            _sut = new LocationService(_mockRepository.Object, _mockLogger.Object, Mapper);
        }

        [Theory]
        [AutoMoqData]
        public async void GetNearestHawkers_ReturnsHawkers(string id, string name, double latitude, double longitude, double currentLatitude, double currentLongitude)
        {
            // arrange
            var expected = new List<Hawker> { CreateStubHawker(id, name, latitude, longitude) };
            _mockRepository.Setup(x => x.GetNearest(It.IsAny<Point>(), It.IsAny<int>()))
                .Returns(Task.FromResult<IReadOnlyList<Hawker>>(expected));

            // act
            var result = await _sut.GetNearestHawkers(currentLatitude, currentLongitude, 5);

            // assert
            Assert.Equal(expected.Count(), result.Count());
            Assert.Equal(expected.First().Name, result.First().Name);
            Assert.Equal(expected.First().Coordinate.Y, result.First().Latitude);
            Assert.Equal(expected.First().Coordinate.X, result.First().Longitude);
        }

        [Theory]
        [AutoMoqData]
        public async void GetNearestHawkers_AndResultEmpty_ReturnsHawkers(double currentLatitude, double currentLongitude)
        {
            // arrange
            var expected = new List<Hawker>();
            _mockRepository.Setup(x => x.GetNearest(It.IsAny<Point>(), It.IsAny<int>()))
                .Returns(Task.FromResult<IReadOnlyList<Hawker>>(expected));

            // act
            var result = await _sut.GetNearestHawkers(currentLongitude, currentLatitude, 5);

            // assert
            Assert.Empty(result);
        }

        [Theory]
        [AutoMoqData]
        public async void GetNearestHawkers_CallsRepository(string id, string name, double latitude, double longitude, double currentLatitude, double currentLongitude)
        {
            // arrange
            var expected = new List<Hawker> { CreateStubHawker(id, name, latitude, longitude) };
            _mockRepository.Setup(x => x.GetNearest(It.IsAny<Point>(), It.IsAny<int>()))
                .Returns(Task.FromResult<IReadOnlyList<Hawker>>(expected));
            int numOfResults = 5;

            // act
            var result = await _sut.GetNearestHawkers(currentLongitude, currentLatitude, numOfResults);

            // assert
            _mockRepository.Verify(x => x.GetNearest(It.Is<Point>(p => p.X == currentLongitude && p.Y == currentLatitude), numOfResults), Times.Once);
        }

        private Hawker CreateStubHawker(string id, string name, double latitude, double longitude)
        {
            return new Hawker
            {
                Id = id,
                Name = name,
                Coordinate = new Point(latitude, longitude)
            };
        }
    }
}
