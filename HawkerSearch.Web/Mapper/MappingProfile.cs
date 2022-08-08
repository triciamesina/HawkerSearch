using AutoMapper;
using HawkerSearch.Domain;
using HawkerSearch.Web.Models;

namespace HawkerSearch.Web.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Hawker, HawkerViewModel>()
                .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Coordinate.X))
                .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Coordinate.Y))
                .ReverseMap();
        }
    }
}
