using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace wepApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:44328/api/");
        }

        public IActionResult Index()
        {
            return View("Register");
        }

        [HttpGet]
        public IActionResult Register()
        {
            Logout();
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserDto userVM)
        {
            if (!ModelState.IsValid)
            {
                return View(userVM);
            }

            try
            {
                HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync("Account/Register", userVM);

                if (httpResponse.IsSuccessStatusCode)
                {

                    var responseMessage = await httpResponse.Content.ReadAsStringAsync();

                    if (responseMessage.Contains("Success"))
                    {
                        return RedirectToAction("RegisterSuccess");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, responseMessage);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error occurred: {ex.Message}");
            }

            return View(userVM);
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
                return View(userVM);
            }

            try
            {
                HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync("Account/Login", userVM);

                if (httpResponse.IsSuccessStatusCode)
                {
                    string jsonResponse = await httpResponse.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                    var token = (string)responseData?.token;
                    var tokenExpires = (string)responseData?.tokenExpires;
                    var customerId = (int?)responseData?.customerId;

                    if (!string.IsNullOrEmpty(token))
                    {
                        HttpContext.Session.SetString("Token", token);
                        HttpContext.Session.SetString("TokenExpires", tokenExpires);
                        if (customerId.HasValue)
                        {
                            HttpContext.Session.SetString("CustomerID", customerId.Value.ToString());
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
        [Route("account/RegisterSuccess")]
        public IActionResult RegisterSuccess()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("account/LoginSuccess")]
        public IActionResult LoginSuccess()
        {
            return View();
        }

        [HttpGet]
        [Route("account/debug-session")]
        public IActionResult DebugSession()
        {
            var token = HttpContext.Session.GetString("Token");
            var tokenExpired = HttpContext.Session.GetString("TokenExpires");

            if (string.IsNullOrEmpty(token))
            {
                return Content("Token was not found in the session.");
            }

            return Content($"Token found in the session: {token} and the Expire Date: {tokenExpired}");
        }

        private bool CheckSessionToken()
        {
            var token = HttpContext.Session.GetString("Token");
            return !string.IsNullOrEmpty(token);
        }

        public IActionResult Logout()
        {
            if (CheckSessionToken())
            {
                HttpContext.Session.Clear();
            }

            return View("Login");
        }
    }
}
