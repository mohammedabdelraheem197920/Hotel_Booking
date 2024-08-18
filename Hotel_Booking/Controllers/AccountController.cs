using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace wepApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient Client;
        public AccountController(IHttpClientFactory httpClientFactory)
        {
            Client = httpClientFactory.CreateClient();
            Client.BaseAddress = new Uri("https://localhost:44328/api");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDto userVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string data = JsonConvert.SerializeObject(userVM);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponse = await Client.PostAsync($"{Client.BaseAddress}/Account/Login/login", content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<LoginResponseDTO>(jsonResponse);

                    if (responseData != null && !string.IsNullOrEmpty(responseData.Token))
                    {
                        HttpContext.Session.SetString("Token", responseData.Token);
                        HttpContext.Session.SetString("TokenExpires", responseData.Expired.ToString());

                        if (responseData.CustomerID != null)
                        {
                            HttpContext.Session.SetString("CustomerID", responseData.CustomerID.ToString());
                        }

                        return RedirectToAction("LoginSuccess");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }
                }
                else
                {
                    ViewBag.ResponseMessage = "Server error. Please contact administrator.";
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ResponseMessage = $"Error occurred: {ex.Message}";
                ModelState.AddModelError(string.Empty, $"Error occurred: {ex.Message}");
            }

            return View(userVM);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("account/LoginSuccess")]
        public IActionResult LoginSuccess()
        {
            return View();
        }

        public class LoginResponseDTO
        {
            public string Token { get; set; }
            public DateTime Expired { get; set; }
            public string? CustomerID { get; set; }
        }
    }
}
