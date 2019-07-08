using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GymCompany.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GymCompany.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "empID,fname,lname,email,position,status,date_reg")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.empID = Guid.NewGuid().ToString();
                employee.date_reg = DateTime.Now;
                employee.status = "Active";
                db.Employees.Add(employee);
                db.SaveChanges();

                var userManager = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                var user = new ApplicationUser();
                user.UserName = employee.email;
                user.Email = employee.email;
                string password = "Password01";
                var checkUser = userManager.Create(user, password);
                TempData["AlertMessage"] = "Employee Added Succesfully.";
                return RedirectToAction("ManagerDashboard","Home");
            }

            return View(employee);
        }

        [HttpPost]
        public ActionResult Deactivate(string id)
        {
            var user = db.Employees.Find(id);
            if(user!=null)
            {
                user.status = "Blocked";
                db.SaveChanges();
            }
            TempData["AlertMessage"] = "User Deactivated Succesfully.";
            return RedirectToAction("ManagerDashboard", "Home");
        }

        [HttpPost]
        public ActionResult Activate(string id)
        {
            var user = db.Employees.Find(id);
            if (user != null)
            {
                user.status = "Active";
                db.SaveChanges();
            }
            TempData["AlertMessage"] = "User Activated Succesfully.";
            return RedirectToAction("ManagerDashboard", "Home");
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "empID,fname,lname,email,position,status,date_reg")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
