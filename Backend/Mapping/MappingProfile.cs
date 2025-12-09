using AutoMapper;
using Backend.Models;
using Backend.DTOs;

namespace Backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Orders
            CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.BoardIds, opt => opt.MapFrom(src => src.Boards.Select(b => b.Id)));

            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.Boards, opt => opt.Ignore()); 

            // Boards
            CreateMap<Board, BoardDto>()
                .ForMember(dest => dest.ComponentIds, opt => opt.MapFrom(src => src.Components.Select(c => c.Id)));

            CreateMap<BoardDto, Board>()
                .ForMember(dest => dest.Components, opt => opt.Ignore());

            // Components
            CreateMap<Component, ComponentDto>().ReverseMap();
        }
    }
}
