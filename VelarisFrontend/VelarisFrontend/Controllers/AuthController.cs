using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VelarisFrontend.Models;
using Newtonsoft.Json;
using System;

namespace VelarisFrontend.Controllers
{
    public class AuthController : Controller
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"])
        };

        [HttpGet]
        public ActionResult Login()
        {
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        { 
            if (!ModelState.IsValid)
            {
                return View(model);
            }

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(
                    "api/auth/login",
                    content
                );

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Invalid login");
                    return View(model);
                }

            var resultJson = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginResponseViewModel>(resultJson);

            Session["JwtToken"] = result.Token;

            return Redirect("/Home/Index");


        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(
                "api/auth/register",
                content
            );
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Registration failed");
                return View(model);
            }
            return RedirectToAction("Login");
        }
    }
}

