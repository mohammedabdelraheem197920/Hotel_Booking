using AutoMapper;
using Core.DTOs;
using Core.Enums;
using Core.Models;
using Core.RepositoryInterfaces;
using Infrastructuer.Context;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
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
            string[] includes = { "Rooms" };
            List<Booking> bookings = bookingRepository.GetAll(userId, includes).ToList();
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

            //context.Entry(allBranchRooms).State = EntityState.Detached;


            // Check if enough rooms are available
            if (allBranchRooms.Count < bookingForPostDto.NumberOfRooms)
            {
                return BadRequest($"Number of Rooms : ({bookingForPostDto.NumberOfRooms}) Bigger than the All Branch Rooms : ({allBranchRooms.Count}) !");
            }

            // Find available rooms
            List<Room> availableBranchRooms = allBranchRooms
                .Where(r => !r.IsBooked || (r.Booking != null && r.Booking.CheckOutDate < DateTime.Now))
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
                user.isOldClient = true;
                userRepository.Save();
            }


            // Assign rooms to the booking
            List<Room> wantedRooms = new List<Room>();
            foreach (var roomDto in bookingForPostDto.Rooms)
            {
                Room wantedRoom = availableBranchRooms.FirstOrDefault(r => r.Type == roomDto.Type);
                if (wantedRoom != null)
                {
                    //// Ensure room is not tracked already
                    //var trackedRoom = context.ChangeTracker.Entries<Room>().FirstOrDefault(e => e.Entity.Id == room.Id);
                    //if (trackedRoom != null)
                    //{
                    //    context.Entry(trackedRoom.Entity).State = EntityState.Detached;
                    //}

                    // Update room status
                    wantedRoom.IsBooked = true;
                    //     wantedRoom.Id = roomDto.Id;
                    wantedRoom.NumberOfChildren = roomDto.NumberOfChilds;
                    wantedRoom.NumberOfAdults = roomDto.NumberOfAdults;
                    // wantedRoom.BookingId = booking.Id;

                    // Attach and update the room entity
                    //context.Rooms.Attach(room);
                    //context.Entry(room).State = EntityState.Modified;
                    wantedRooms.Add(wantedRoom);
                    //  bookingForPostDto.Rooms=mapper.Map<RoomForPostDto>(wantedRooms);
                    availableBranchRooms.Remove(wantedRoom);

                }
            }
            booking.Rooms = wantedRooms;
            booking.UserId = userId;
            roomRepository.Save();
            bookingRepository.Insert(booking);
            bookingRepository.Save();

            // Add and save the booking
            //bookingRepository.Insert(booking);

            // context.SaveChanges();

            return Ok(booking);
        }


    }
}
