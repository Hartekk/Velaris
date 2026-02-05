using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VelarisFrontend.Models;
using Newtonsoft.Json;

namespace VelarisFrontend.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(
                    "https://localhost:443xx/api/auth/login",
                    content
                );

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Invalid login");
                    return View(model);
                }

                var resultJson = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(resultJson);

                Session["JwtToken"] = (string)result.token;

                return RedirectToAction("Index", "Home");
            }
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
    }
}
