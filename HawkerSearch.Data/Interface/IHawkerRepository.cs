using NetTopologySuite.Geometries;

namespace HawkerSearch.Domain
{
    public interface IHawkerRepository : IBaseRepository<Hawker>
    {
        Task<IReadOnlyList<Hawker>> GetNearest(Point currentLocation, int numOfResults);
    }
}
