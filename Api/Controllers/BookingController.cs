using AutoMapper;
using Core.DTOs;
using Core.Enums;
using Core.Models;
using Core.RepositoryInterfaces;
using Infrastructuer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IBranchRepository branchRepository;
        private readonly IRoomRepository roomRepository;
        private readonly IUserRepository userRepository;
        private readonly IHotelRepository hotelRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HotelDbContext context;

        public BookingController(
            IBookingRepository bookingRepository,
            IBranchRepository branchRepository,
            IRoomRepository roomRepository,
            IUserRepository userRepository,
            IHotelRepository hotelRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            HotelDbContext context
            )
        {
            this.bookingRepository = bookingRepository;
            this.branchRepository = branchRepository;
            this.roomRepository = roomRepository;
            this.userRepository = userRepository;
            this.hotelRepository = hotelRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.context = context;
        }

        private string GetLoggedInUserId()
        {
            return httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            List<Booking> bookings = bookingRepository.GetAll().ToList();
            List<BookingForGetDto> getBookingDTOs = mapper.Map<List<BookingForGetDto>>(bookings);
            return Ok(getBookingDTOs);
        }


        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            Booking booking = bookingRepository.GetById(id);
            if (booking != null)
            {
                BookingForGetDto bookingForGetDto = mapper.Map<BookingForGetDto>(booking);

                return Ok(bookingForGetDto);
            }
            else
            {
                throw new KeyNotFoundException();
            }

        }


        [HttpPost]
        public IActionResult Add(BookingForPostDto bookingForPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the user
            var userId = GetLoggedInUserId();
            if (userId == null)
            {
                return Unauthorized("User is not logged in.");
            }

            var user = bookingRepository.GetUserById(userId);
            if (user == null)
            {
                return BadRequest($"No Customer Found with this ID : ({userId})");
            }

            // Check if branch exists
            if (branchRepository.GetById(bookingForPostDto.BranchID) == null)
            {
                return BadRequest($"No Branch Found with this ID : ({bookingForPostDto.BranchID})");
            }

            // Validate CheckIn and CheckOut dates
            if (bookingForPostDto.CheckOut < DateTime.Now ||
                bookingForPostDto.CheckIn < DateTime.Now ||
                bookingForPostDto.CheckIn > bookingForPostDto.CheckOut)
            {
                return BadRequest("Invalid Check-in and Check-out Times, both times should be in the future not past, Checkout Date must be after Checkin Date");
            }

            // Get all rooms for the branch
            List<Room> allBranchRooms = roomRepository.GetAll()
                                              .Where(r => r.BranchID == bookingForPostDto.BranchID)
                                              .ToList();

            // Check if enough rooms are available
            if (allBranchRooms.Count < bookingForPostDto.NumberOfRooms)
            {
                return BadRequest($"Number of Rooms : ({bookingForPostDto.NumberOfRooms}) Bigger than the All Branch Rooms : ({allBranchRooms.Count}) !");
            }

            // Find available rooms
            List<Room> availableBranchRooms = allBranchRooms
                                              .Where(r => r.Booking == null || r.Booking.CheckOutDate < DateTime.Now)
                                              .ToList();

            if (availableBranchRooms.Count == 0)
            {
                return BadRequest("There are no available rooms in this branch");
            }

            // Count requested room types
            int numberOfWantedSingleRooms = bookingForPostDto.Rooms.Count(r => r.Type == RoomType.Single);
            int numberOfWantedDoubleRooms = bookingForPostDto.Rooms.Count(r => r.Type == RoomType.Double);
            int numberOfWantedSuiteRooms = bookingForPostDto.Rooms.Count(r => r.Type == RoomType.Suite);

            // Count available room types
            int numberOfAvailableSingleRooms = availableBranchRooms.Count(r => r.Type == RoomType.Single);
            int numberOfAvailableDoubleRooms = availableBranchRooms.Count(r => r.Type == RoomType.Double);
            int numberOfAvailableSuiteRooms = availableBranchRooms.Count(r => r.Type == RoomType.Suite);

            if (numberOfAvailableSingleRooms < numberOfWantedSingleRooms ||
                numberOfAvailableDoubleRooms < numberOfWantedDoubleRooms ||
                numberOfAvailableSuiteRooms < numberOfWantedSuiteRooms)
            {
                return BadRequest($"There are only available Single Rooms : ({numberOfAvailableSingleRooms}), Double Rooms : ({numberOfAvailableDoubleRooms}), Suite Rooms : ({numberOfAvailableSuiteRooms})");
            }

            // Create the booking entity
            Booking booking = mapper.Map<Booking>(bookingForPostDto);

            // Apply discount if applicable
            if (bookingRepository.GetById(booking.Id) == null)
            {
                booking.DiscountApplied = false;
                booking.Discount = 0m;
            }
            else
            {
                booking.DiscountApplied = true;
                booking.Discount = 0.05m;
            }

            // Add and save the booking
            bookingRepository.Insert(booking);
            bookingRepository.Save();

            // Assign rooms to the booking
            List<Room> bookedRooms = new List<Room>();
            foreach (var roomDto in bookingForPostDto.Rooms)
            {
                Room room = availableBranchRooms.FirstOrDefault(r => r.Type == roomDto.Type);
                if (room != null)
                {
                    room.IsBooked = true;
                    room.BookingId = booking.Id;
                    room.NumberOfChildren = roomDto.NumberOfChilds;
                    room.NumberOfAdults = roomDto.NumberOfAdults;

                    // If room is already being tracked, update its state
                    if (context.Entry(room).State == EntityState.Detached)
                    {
                        context.Attach(room);
                    }
                    else
                    {
                        context.Update(room);
                    }

                    bookedRooms.Add(room);
                    availableBranchRooms.Remove(room);
                }
            }

            roomRepository.Save();

            // Map booked rooms to DTO
            List<RoomForGetDto> roomForGetDTOs = bookedRooms.Select(room => mapper.Map<RoomForGetDto>(room)).ToList();

            // Return the response
            return Ok(new
            {
                Message = $"({bookedRooms.Count}) Rooms Booked Successfully by customer with ID ({bookingForPostDto.userID}) in Branch with ID ({bookingForPostDto.BranchID}).",
                Data = roomForGetDTOs
            });
        }





    }
}
