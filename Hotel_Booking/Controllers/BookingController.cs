using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace wepApp.Controllers
{
    public class BookingController : Controller
    {
        //Uri baseAdress = new Uri("\"https://localhost:44328/api");

        private readonly HttpClient Client;
        public BookingController(IHttpClientFactory httpClientFactory)
        {
            Client = httpClientFactory.CreateClient();
            Client.BaseAddress = new Uri("https://localhost:44328/api");
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<BookingForGetDto> bookings = new List<BookingForGetDto>();
            HttpResponseMessage response = await Client.GetAsync($"{Client.BaseAddress}/Hotel/GetAll");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                bookings = JsonConvert.DeserializeObject<List<BookingForGetDto>>(data);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(bookings);
        }


        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            BookingForGetDto booking = new BookingForGetDto();
            HttpResponseMessage response = await Client.GetAsync($"{Client.BaseAddress}/Hotel/Get/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                booking = JsonConvert.DeserializeObject<BookingForGetDto>(data);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

            }
            return View(booking);
        }

        [HttpGet]
        public ActionResult CreateBooking()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateBooking(BookingForPostDto booking)
        {
            try
            {
                string data = JsonConvert.SerializeObject(booking);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await Client.PostAsync($"{Client.BaseAddress}/Booking/Add", content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetAll");
                }
            }
            catch (Exception ex)
            {
                return View(ex);
            }
            return View();


        }
    }
}
