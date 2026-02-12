using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VelarisFrontend.Models
{
    public class DashboardViewModel
    {
        public List<TaskViewModel> TodaysTasks { get; set; }
        public List<TaskViewModel> UpcomingTasks { get; set; }
    }
}