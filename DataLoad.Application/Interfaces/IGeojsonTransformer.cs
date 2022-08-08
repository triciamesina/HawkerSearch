using HawkerSearch.Domain;
using NetTopologySuite.Features;

namespace DataLoad.Application.Interfaces
{
    public interface IGeojsonTransformer
    {
        Hawker Transform(IFeature feature);
        Dictionary<string, string> GetAttributeValues(IFeature feature);
        bool HasAttribute(string description, string propertyName, out string value);
    }
}
