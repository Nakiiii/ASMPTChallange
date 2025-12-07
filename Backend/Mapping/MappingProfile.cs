using AutoMapper;
using Backend.Models;
using Backend.DTOs;

namespace Backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<BoardDto, Board>().ReverseMap();
            CreateMap<ComponentDto, Component>().ReverseMap();
        }
    }
}
