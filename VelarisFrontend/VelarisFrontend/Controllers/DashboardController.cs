using Newtonsoft.Json;
using System;
using System.Text;
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

        private void AttachJwtToken()
        {
            var token = Session["JwtToken"] as string;
            if (!string.IsNullOrEmpty(token)) {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        private void PrepareClient()
        {
            _client.DefaultRequestHeaders.Authorization = null;
            AttachJwtToken();
        }

        public async Task<ActionResult> Index()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new DashboardViewModel
            {
                TodaysTasks = new List<TaskViewModel>(),
                UpcomingTasks = new List<TaskViewModel>(),
                OverdueTasks = new List<TaskViewModel>()
            };


            try
            {
                PrepareClient();

                var response = await _client.GetAsync("/api/todoitem/getall");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("RAW JSON: " + json);

                    var tasks = JsonConvert.DeserializeObject<List<TaskViewModel>>(json);

                    var today = DateTime.Today;

                    model.TodaysTasks = tasks.Where(t => t.DueDate.Date == today && t.IsCompleted == false).ToList();

                    model.UpcomingTasks = tasks.Where(t => t.DueDate.Date > today && t.IsCompleted == false).ToList();

                    model.OverdueTasks = tasks.Where(t => t.DueDate.Date < today && t.IsCompleted == false).ToList();
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

        [HttpPost]
        public async Task<ActionResult> AddTask(TaskViewModel model)
        {
            PrepareClient();

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/todoitem/create", content);

            return Json(new { success = response.IsSuccessStatusCode });
        }

        [HttpPost]
        public async Task<ActionResult> EditTask(TaskViewModel model) {
            PrepareClient();

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"/api/todoitem/edit/{model.Id}", content);

            return Json(new { success = response.IsSuccessStatusCode });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteTask(int id)
        {

            PrepareClient();

            var response = await _client.DeleteAsync($"/api/todoitem/delete/{id}");

          
            System.Diagnostics.Debug.WriteLine("Delete API status: " + response.StatusCode);

            return Json(new { success = response.IsSuccessStatusCode });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAllTasks()
        {

            PrepareClient();

            var response = await _client.DeleteAsync($"/api/todoitem/deleteall");


            System.Diagnostics.Debug.WriteLine("Delete API status: " + response.StatusCode);

            return Json(new { success = response.IsSuccessStatusCode });
        }

    }
}