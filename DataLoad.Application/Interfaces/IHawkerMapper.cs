using HawkerSearch.Domain;
using NetTopologySuite.Geometries;

namespace DataLoad.Application.Interfaces
{
    public interface IHawkerMapper
    {
        Hawker CreateHawker(string id, Geometry coordinates, Dictionary<string, string> description);
    }
}
