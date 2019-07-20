using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GymCompany.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace GymCompany.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clients
        public ActionResult Index()
        {
            var clients = db.Clients.Include(c => c.Employee).Include(c => c.Package);
            return View(clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.empID = new SelectList(db.Employees, "empID", "fname");
            ViewBag.nID = new SelectList(db.NextOfKins, "nID", "Fname");
            ViewBag.pID = new SelectList(db.Packages, "pID", "type");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "clientID,clientTitle,clientFname,clientLname,type,passportno,IDno,postalAddress,postalCode,hometel,cellno,email,requirepersonal,empID,date_reg,pID")] Client client)
        {
            if (ModelState.IsValid)
            {
                var trainerID = Request["employeeID"].ToString();
                client.personatrainer = trainerID;
                client.clientID = Guid.NewGuid().ToString();
                var empl = db.Employees.ToList().Find(x => x.email == User.Identity.Name);
                client.empID = empl.empID;
                client.status = "Active";
                client.date_reg = DateTime.Now;
                db.Clients.Add(client);
                db.SaveChanges();

                
                var userManager = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                var user = new ApplicationUser();
                user.UserName = client.email;
                user.Email = client.email;
                string password = "Password01";
                var checkUser = userManager.Create(user, password);
                TempData["AlertMessage"] = "Client Added Succesfully.";
                return RedirectToAction("RecruiterDashboard", "Home");
            }

            ViewBag.empID = new SelectList(db.Employees, "empID", "fname", client.empID);
            ViewBag.pID = new SelectList(db.Packages, "pID", "type", client.pID);
            return View(client);
        }

        public ActionResult updateStatus(string lid)
        {
            Lead ld = db.Leads.ToList().Find(x => x.lID == lid);
            if(ld!=null)
            {
                ld.status = "Approved";
                db.SaveChanges();
                TempData["AlertMessage"] = "Lead Status Updated Succesfully.";
                return RedirectToAction("RecruiterDashboard", "Home");

            }
            return RedirectToAction("RecruiterDashboard", "Home");

        }

        public ActionResult Decline(string lid)
        {
            Lead ld = db.Leads.ToList().Find(x => x.lID == lid);
            if (ld != null)
            {
                ld.status = "Declined";
                db.SaveChanges();
                TempData["AlertMessage"] = "Lead Status Declined Succesfully.";
                return RedirectToAction("RecruiterDashboard", "Home");

            }
            return RedirectToAction("RecruiterDashboard", "Home");

        }

        [HttpPost]
        public ActionResult Deactivate(string id)
        {
            var user = db.Clients.Find(id);
            if (user != null)
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
            var user = db.Clients.Find(id);
            if (user != null)
            {
                user.status = "Active";
                db.SaveChanges();
            }
            TempData["AlertMessage"] = "User Activated Succesfully.";
            return RedirectToAction("ManagerDashboard", "Home");
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.empID = new SelectList(db.Employees, "empID", "fname", client.empID);
            ViewBag.pID = new SelectList(db.Packages, "pID", "type", client.pID);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clientID,clientTitle,clientFname,clientLname,type,passportno,IDno,postalAddress,postalCode,hometel,cellno,email,requirepersonal,empID,date_reg,pID")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.empID = new SelectList(db.Employees, "empID", "fname", client.empID);
            ViewBag.pID = new SelectList(db.Packages, "pID", "type", client.pID);
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
