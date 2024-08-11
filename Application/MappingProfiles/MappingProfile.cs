using Application.DTOs;
using AutoMapper;
using Core.Models;

namespace Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Hotel, HotelForGetDto>().ReverseMap();
            CreateMap<HotelForGetDto, Hotel>().ReverseMap();

        }
    }
}
