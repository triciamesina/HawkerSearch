using NetTopologySuite.Features;

namespace DataLoad.Application.Interfaces
{
    public interface IFileReader
    {
        FeatureCollection GetFeatures(string path);
        string ReadFile(string path);
    }
}
