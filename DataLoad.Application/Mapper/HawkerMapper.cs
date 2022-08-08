using DataLoad.Application.Enumerations;
using DataLoad.Application.Interfaces;
using HawkerSearch.Domain;
using NetTopologySuite.Geometries;

namespace DataLoad.Application.Mapper
{
    public class HawkerMapper : IHawkerMapper
    {
        public Hawker CreateHawker(string id, Geometry coordinates, Dictionary<string, string> description)
        {
            return new Hawker
            {
                Id = id,
                Coordinate = coordinates.InteriorPoint,
                Name = description.GetValueOrDefault(AttributeNameEnum.NAME.ToString()),
                UnitNumber = description.GetValueOrDefault(AttributeNameEnum.ADDRESSUNITNUMBER.ToString()),
                FloorNumber = description.GetValueOrDefault(AttributeNameEnum.ADDRESSFLOORNUMBER.ToString()),
                BlockNumber = description.GetValueOrDefault(AttributeNameEnum.ADDRESSBLOCKHOUSENUMBER.ToString()),
                BuildingName = description.GetValueOrDefault(AttributeNameEnum.ADDRESSBUILDINGNAME.ToString()),
                Street = description.GetValueOrDefault(AttributeNameEnum.ADDRESSSTREETNAME.ToString()),
                PostalAddress = description.GetValueOrDefault(AttributeNameEnum.ADDRESSPOSTALCODE.ToString()),
                Region = description.GetValueOrDefault(AttributeNameEnum.REGION.ToString()),
                PhotoUrl = description.GetValueOrDefault(AttributeNameEnum.PHOTOURL.ToString())
            };
        }
    }
}
