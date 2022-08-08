using AutoMapper;
using HawkerSearch.Domain;
using HawkerSearch.Web.Interfaces;
using HawkerSearch.Web.Models;
using Microsoft.Extensions.Logging;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.Operation.Valid;

namespace HawkerSearch.Web.Services
{
    public class LocationService : ILocationService
    {
        protected readonly IHawkerRepository _repository;
        protected readonly ILogger<LocationService> _logger;
        protected readonly IMapper _mapper;
        protected readonly GeometryFactory _geometryFactory;

        public LocationService(IHawkerRepository repository, ILogger<LocationService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        }

        public async Task<IEnumerable<HawkerViewModel>> GetNearestHawkers(double longitude, double latitude, int numOfResults = 5)
        {
            var currentLocation = _geometryFactory.CreatePoint(new Coordinate(x: longitude, y: latitude));
            var hawkers = await _repository.GetNearest(currentLocation, numOfResults);
            return _mapper.Map<IEnumerable<Hawker>, IEnumerable<HawkerViewModel>>(hawkers);
        }

        public bool IsValidLocation(double latitude, double longitude)
        {
            return (new Coordinate(longitude, latitude)).IsValid;
        }
    }
}