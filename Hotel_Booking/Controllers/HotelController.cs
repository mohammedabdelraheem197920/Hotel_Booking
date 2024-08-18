using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace wepApp.Controllers
{
    public class HotelController : Controller
    {
        //Uri baseAdress = new Uri("\"https://localhost:44328/api");

        private readonly HttpClient Client;
        public HotelController(IHttpClientFactory httpClientFactory)
        {
            Client = httpClientFactory.CreateClient();
            Client.BaseAddress = new Uri("https://localhost:44328/api");
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<HotelForGetDto> hotels = new List<HotelForGetDto>();
            HttpResponseMessage response = await Client.GetAsync($"{Client.BaseAddress}/Hotel/GetAll");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                hotels = JsonConvert.DeserializeObject<List<HotelForGetDto>>(data);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(hotels);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            HotelForGetDto hotel = new HotelForGetDto();
            HttpResponseMessage response = await Client.GetAsync($"{Client.BaseAddress}/Hotel/Get/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                hotel = JsonConvert.DeserializeObject<HotelForGetDto>(data);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

            }
            return View(hotel);
        }

    }
}
