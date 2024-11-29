using System.Web.Mvc;

namespace LoginMVC.Controllers
{
    public class DashboardController : Controller
    {
        // Student Dashboard
        public ActionResult StudentDashboard()
        {
            return View(); // Ensure a corresponding view exists
        }

        // Faculty Dashboard
        public ActionResult FacultyDashboard()
        {
            return View(); // Ensure a corresponding view exists
        }

        // Admin Dashboard
        public ActionResult AdminDashboard()
        {
            return View(); // Ensure a corresponding view exists
        }
    }
}
