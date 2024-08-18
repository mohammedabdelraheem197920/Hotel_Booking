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
            CreateMap<Hotel, HotelForGetDto>()
           .ForMember(dest => dest.noOfBranches, opt => opt.MapFrom(src => src.Branches != null ? src.Branches.Count : 0))
           .ReverseMap();


            CreateMap<Branch, BranchForGetDto>()
                .ForMember(dest => dest.HotelID, opt => opt.MapFrom(src => src.Hotel.Id))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name))
                .ReverseMap();

            CreateMap<Booking, BookingForGetDto>()
                .ForMember(dest => dest.numberOfRooms, opt => opt.MapFrom(src => src.Rooms.Count))
                .ReverseMap();

            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<User, LoginUserDto>().ReverseMap();


            CreateMap<RoomForPostDto, Room>().ReverseMap();
            CreateMap<Room, RoomForPostDto>().ReverseMap();


            CreateMap<BookingForPostDto, Booking>().ReverseMap();

        }
    }
}
