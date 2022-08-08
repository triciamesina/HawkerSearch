using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace DataLoad.UnitTests
{
    public static class GeojsonTestHelper
    {
        public static T GetTestFeature<T>(string stringToConvert)
        {
            var serializer = GeoJsonSerializer.Create();

            using (var stringReader = new StringReader(stringToConvert))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                var serializedObject = serializer.Deserialize<T>(jsonReader);
                return serializedObject;
            }

        }
    }

}