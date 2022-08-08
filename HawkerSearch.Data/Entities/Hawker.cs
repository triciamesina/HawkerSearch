using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace HawkerSearch.Domain
{
    public class Hawker
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string UnitNumber { get; set; }

        public string FloorNumber { get; set; }

        public string BlockNumber { get; set; }

        public string BuildingName { get; set; }

        public string Street { get; set; }

        public string PostalAddress { get; set; }

        public string Region { get; set; }

        public string PhotoUrl { get; set; }

        public Point Coordinate { get; set; }
    }
}