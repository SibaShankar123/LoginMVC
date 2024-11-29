using LoginMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginMVC.Controllers
{
    public class AccountController : Controller
    {
        readonly UserDataEntities db = new UserDataEntities();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegister ur)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(ur.Email) || string.IsNullOrEmpty(ur.Password))
                {
                    ViewBag.Message = "Please fill in all the above fields.";
                }
                else if (db.UserRegisters.Any(x => x.Email == ur.Email))
                {
                    ViewBag.Message = "Email already registered.";
                }
                else
                {
                    db.UserRegisters.Add(ur);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Registration Successful!";
                    return RedirectToAction("Login");
                }
            }
            return View(ur);
        }

        [HttpGet]
        public ActionResult Login()
        {
            // Display any success messages after registration
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }

        [HttpPost]
        public ActionResult Login(MyLogin l)
        {
            if (string.IsNullOrEmpty(l.Email) || string.IsNullOrEmpty(l.Password))
            {
                ViewBag.Message = "Please fill in both the email and password fields.";
            }
            else
            {
                var query = db.UserRegisters.SingleOrDefault(m => m.Email == l.Email && m.Password == l.Password);

                if (query != null)
                {
                    TempData["LoginMessage"] = "Login Successful!";

                    //return RedirectToAction("Dashboard"); // Replace with your actual redirect page
                    switch (query.UserType.ToString())
                    {
                        case "Student":
                            return RedirectToAction("StudentDashboard", "Dashboard");
                        case "Faculty":
                            return RedirectToAction("FacultyDashboard", "Dashboard");
                        case "Admin":
                            return RedirectToAction("AdminDashboard", "Dashboard");
                        default:
                            return RedirectToAction("Index", "Home"); // Fallback
                    }

                }
                else
                {
                    ViewBag.Message = "Invalid Credentials.";
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }


        public ActionResult Dashboard()
        {
            ViewBag.Message = TempData["LoginMessage"];
            return View(); // Your dashboard view
        }
    }
}
