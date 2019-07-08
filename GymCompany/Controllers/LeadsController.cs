using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GymCompany.Models;

namespace GymCompany.Controllers
{
    public class LeadsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Leads
        public ActionResult Index()
        {
            var leads = db.Leads.Include(l => l.Employee);
            return View(leads.ToList());
        }

        // GET: Leads/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lead lead = db.Leads.Find(id);
            if (lead == null)
            {
                return HttpNotFound();
            }
            return View(lead);
        }

        // GET: Leads/Create
        public ActionResult Create()
        {
            ViewBag.empID = new SelectList(db.Employees, "empID", "fname");
            return View();
        }

        // POST: Leads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "lID,fname,lanme,cno,priority,status,empID,dateupdated")] Lead lead)
        {
            if (ModelState.IsValid)
            {
                var emp = db.Employees.ToList().Find(x => x.email == User.Identity.Name);
                lead.empID = emp.empID;
                lead.lID = Guid.NewGuid().ToString();
                lead.priority = 5;
                lead.status = "Pending";
                lead.dateupdated = DateTime.Now;
                db.Leads.Add(lead);
                db.SaveChanges();
                TempData["AlertMessage"] = "Lead Client Added Succesfully.";
                return RedirectToAction("RecruiterDashboard", "Home");
            }

            ViewBag.empID = new SelectList(db.Employees, "empID", "fname", lead.empID);
            return View(lead);
        }

        // GET: Leads/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lead lead = db.Leads.Find(id);
            if (lead == null)
            {
                return HttpNotFound();
            }
            ViewBag.empID = new SelectList(db.Employees, "empID", "fname", lead.empID);
            return View(lead);
        }

        // POST: Leads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "lID,fname,lanme,cno,priority,status,empID,dateupdated")] Lead lead)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lead).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.empID = new SelectList(db.Employees, "empID", "fname", lead.empID);
            return View(lead);
        }

        // GET: Leads/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lead lead = db.Leads.Find(id);
            if (lead == null)
            {
                return HttpNotFound();
            }
            return View(lead);
        }

        // POST: Leads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Lead lead = db.Leads.Find(id);
            db.Leads.Remove(lead);
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
