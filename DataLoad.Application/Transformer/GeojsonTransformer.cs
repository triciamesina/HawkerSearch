using DataLoad.Application.Enumerations;
using DataLoad.Application.Interfaces;
using HawkerSearch.Domain;
using NetTopologySuite.Features;
using System.Text.RegularExpressions;

namespace DataLoad.Application.Transformer
{
    public class GeojsonTransformer : IGeojsonTransformer
    {
        private const string SearchEndString = "</tr>";
        private const string SearchStartString = "<th>{0}</th>";
        private const string PropertyMatchRegex = "<td>(.*)<\\/td>";
        private const string AttributeNameKey = "Name";
        private const string AttributeDescriptionKey = "Description";


        protected readonly IHawkerMapper _hawkerMapper;

        public GeojsonTransformer(IHawkerMapper hawkerMapper)
        {
            _hawkerMapper = hawkerMapper;
        }

        public Hawker Transform(IFeature feature)
        {
            var prop = GetAttributeValues(feature);
            var id = feature.Attributes[AttributeNameKey];
            if (id == null || id is not string)
                throw new TransformException("Invalid input format. Name is required.");

            var hawker = _hawkerMapper.CreateHawker(id as string, feature.Geometry, prop);
            return hawker;
        }

        public Dictionary<string, string> GetAttributeValues(IFeature feature)
        {
            var properties = new Dictionary<string, string>();
            var attribute = feature.Attributes[AttributeDescriptionKey] as string;
            if (attribute == null || attribute is not string)
                throw new TransformException("Invalid input format. Description is required.");

            foreach (AttributeNameEnum attributeName in Enum.GetValues(typeof(AttributeNameEnum)))
            {
                if (HasAttribute(attribute, attributeName.ToString(), out string searchString))
                {
                    var match = Regex.Match(searchString, PropertyMatchRegex);
                    if (match.Success && match.Groups[1] != null)
                    {
                        properties.Add(attributeName.ToString(), match.Groups[1].Value);
                    }
                }
            }
            return properties;
        }

        public bool HasAttribute(string description, string propertyName, out string value)
        {
            value = string.Empty;
            int searchStartIndex = description.IndexOf(string.Format(SearchStartString, propertyName));
            if (searchStartIndex < 0) return false;
            int searchEndIndex = description.IndexOf(SearchEndString, searchStartIndex);
            if (searchEndIndex < 0) return false;

            value = description.Substring(searchStartIndex, searchEndIndex - searchStartIndex);
            return true;
        }
    }
}