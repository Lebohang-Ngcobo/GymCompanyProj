using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymCompany.Models;

namespace GymCompany.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            try
            {
                var employee = db.Employees.ToList().Find(x => x.email == User.Identity.Name);
                var client = db.Clients.ToList().Find(x => x.email == User.Identity.Name);

                if (Request.IsAuthenticated)
                {
                    if (User.Identity.Name == "lebo.ngcobom@gmail.com")
                    {
                        return View("ManagerDashboard");
                    }
                    if (employee.position.ToLower().Equals("recruiter")|| employee.position.ToLower().Equals("Personal Trainer"))
                    {
                        if(employee.status=="Active")
                        {
                            return View("RecruiterDashboard");

                        }
                        else
                        {
                            TempData["AlertMessage"] = "Account was Deactivated";
                            return RedirectToAction("Login","Account");

                        }

                    }

                    if(client.status=="Active")
                    {
                        return View("ClientDashboard");
                    }
                    else
                    {
                        TempData["AlertMessage"] = "Account was Deactivated";
                        return RedirectToAction("Login", "Account");

                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");

                }
            }
            catch (Exception)
            {

            }
           
            

            return View();

        }





        public ActionResult ManagerDashboard()
        {
            if (Request.IsAuthenticated)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult RecruiterDashboard()
        {
            try
            {
                var employee = db.Employees.ToList().Find(x => x.email == User.Identity.Name);
                if (Request.IsAuthenticated)
                {
                    if(employee.status=="Active")
                    {
                        return View();

                    }
                    else
                    {
                        TempData["AlertMessage"] = "Account was Deactivated";
                        return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch(Exception)
            {
                TempData["AlertMessage"] = "Account was Deactivated";
                return RedirectToAction("Login", "Account");
            }
           
        }

        public ActionResult ClientrDashboard()
        {
            try
            {
                var employee = db.Clients.ToList().Find(x => x.email == User.Identity.Name);
                if (Request.IsAuthenticated)
                {
                    if (employee.status == "Active")
                    {
                        return View();

                    }
                    else
                    {
                        TempData["AlertMessage"] = "Account was Deactivated";
                        return RedirectToAction("Login", "Account");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception)
            {
                TempData["AlertMessage"] = "Account was Deactivated";
                return RedirectToAction("Login", "Account");
            }

        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
    }
