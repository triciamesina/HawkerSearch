using HawkerSearch.Domain;
using HawkerSearch.Web.Models;

namespace HawkerSearch.Web.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<HawkerViewModel>> GetNearestHawkers(double longitude, double latitude, int numOfResults = 5);
        bool IsValidLocation(double latitude, double longitude);
    }
}
