using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VelarisFrontend.Models;

namespace VelarisFrontend.Controllers
{
    public class DashboardController : Controller
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"])
        };

        public async Task<ActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new DashboardViewModel
            {
                TodaysTasks = new List<TaskViewModel>(),
                UpcomingTasks = new List<TaskViewModel>()
            };


            try
            {
                var token = Session["AuthToken"] as string;
                if (!string.IsNullOrEmpty(token))
                {
                    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                var response = await _client.GetAsync("/api/todoitems");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var tasks = JsonConvert.DeserializeObject<List<TaskViewModel>>(json);

                    var today = DateTime.Today;

                    model.TodaysTasks = tasks.Where(t => t.DueDate.Date == today && t.IsCompleted == false).ToList();

                    model.UpcomingTasks = tasks.Where(t => t.DueDate.Date > today && t.IsCompleted == false).ToList();
                }
                else
                {
                    ModelState.AddModelError("", "Failed to load tasks. Please try again later.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while loading tasks: " + ex.Message);
            }

            return View(model);

        }
    }
}