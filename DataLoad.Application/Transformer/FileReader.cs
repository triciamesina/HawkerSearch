using DataLoad.Application.Interfaces;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace DataLoad.Application.Transformer
{
    public class FileReader : IFileReader
    {
        public FeatureCollection GetFeatures(string path)
        {
            try
            {
                var fileContents = ReadFile(path);
                var serializer = GeoJsonSerializer.Create();

                using (var stringReader = new StringReader(fileContents))
                using (var jsonReader = new JsonTextReader(stringReader))
                {
                    var features = serializer.Deserialize<FeatureCollection>(jsonReader);
                    return features;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error parsing Geojson file.", e);
            }
        }

        public string ReadFile(string path)
        {
            string fileContents;
            using (var reader = new StreamReader(path))
            {
                fileContents = reader.ReadToEnd();
            }
            return fileContents;
        }

    }
}
