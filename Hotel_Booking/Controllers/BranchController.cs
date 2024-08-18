using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace wepApp.Controllers
{
    public class BranchController : Controller
    {

        private readonly HttpClient client;
        public BranchController(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:44328/api");
        }


        public async Task<IActionResult> GetAll()
        {
            List<BranchForGetDto> branches = new List<BranchForGetDto>();
            HttpResponseMessage response = await client.GetAsync($"{client.BaseAddress}/Branch/GetAll");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                branches = JsonConvert.DeserializeObject<List<BranchForGetDto>>(data);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(branches);
        }
    }
}
