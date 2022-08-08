using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace HawkerSearch.Domain
{
    public class HawkerRepository : BaseRepository<Hawker>, IHawkerRepository
    {
        public HawkerRepository(HawkerContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Hawker>> GetNearest(Point currentLocation, int numOfResults)
        {
            return await _hawkerContext.Set<Hawker>()
                .OrderBy(x => x.Coordinate.Distance(currentLocation))
                .Take(numOfResults)
                .ToListAsync();
        }
    }
}
