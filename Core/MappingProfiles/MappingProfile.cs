using Application.DTOs;
using AutoMapper;
using Core.DTOs;
using Core.Models;

namespace Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Hotel, HotelForGetDto>().ReverseMap();


            CreateMap<Branch, BranchForGetDto>()
                .ForMember(dest => dest.HotelID, opt => opt.MapFrom(src => src.Hotel.Id))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name))
                .ReverseMap();

            CreateMap<Booking, BookingForGetDto>()
                .ForMember(dest => dest.BranchID, opt => opt.MapFrom(src => src.Branch.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ReverseMap();

            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<User, LoginUserDto>().ReverseMap();


            CreateMap<RoomForPostDto, Room>().ReverseMap();


            CreateMap<BookingForPostDto, Booking>().ReverseMap();

        }
    }
}
