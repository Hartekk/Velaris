using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VelarisFrontend.Models;
using Newtonsoft.Json;
using System;
using System.Web.Security;
using System.Web;

namespace VelarisFrontend.Controllers
{
    public class AuthController : Controller
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"])
        };

        // Load Login page
        [HttpGet]
        public ActionResult Login()
        {
            return Redirect("/Home/Index");
        }

        // Handle Login form submission

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["LoginError"] = "Please enter username and password.";
                return RedirectToAction("Index", "Home");
            }

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/auth/login", content);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                TempData["LoginError"] = "Invalid username or password.";
                return RedirectToAction("Index", "Home");
            }
           

            var resultJson = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LoginResponseViewModel>(resultJson);

            Session["JwtToken"] = result.Token; 
            FormsAuthentication.SetAuthCookie(model.Username, false);

            return RedirectToAction("Index", "Home");
        }


        // Load Registration page
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // Handle Registration form submission
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["RegisterError"] = "Please fill all fields.";
                return View(model);
            }

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/auth/register", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["RegisterError"] = "Registration failed.";
                return View(model);
            }

            TempData["LoginError"] = "Registration successful. Please log in.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            var token = Session["JwtToken"] as string;

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, "api/auth/logout");
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    await _client.SendAsync(request);
                } catch
                {

                }

                FormsAuthentication.SignOut();

                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null) {
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
                    {
                        Expires = DateTime.UtcNow.AddDays(-1),
                        Path = FormsAuthentication.FormsCookiePath
                    };
                    Response.Cookies.Add(cookie);
                        
                        }
                Session.Clear();
                Session.Abandon();

                
            } return RedirectToAction("Index", "Home");
        }


    }
}

