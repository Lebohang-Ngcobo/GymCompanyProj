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
    public class NextOfKinsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NextOfKins
        public ActionResult Index()
        {
            var nextOfKins = db.NextOfKins.Include(n => n.Client);
            return View(nextOfKins.ToList());
        }

        // GET: NextOfKins/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextOfKin nextOfKin = db.NextOfKins.Find(id);
            if (nextOfKin == null)
            {
                return HttpNotFound();
            }
            return View(nextOfKin);
        }

        // GET: NextOfKins/Create
        public ActionResult Create()
        {
            ViewBag.clientID = new SelectList(db.Clients, "clientID", "clientTitle");
            return View();
        }

        // POST: NextOfKins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nID,Fname,Lname,tel,assoc,date_reg,clientID")] NextOfKin nextOfKin)
        {
            if (ModelState.IsValid)
            {
                nextOfKin.nID = Guid.NewGuid().ToString();
                nextOfKin.date_reg = DateTime.Now;
                db.NextOfKins.Add(nextOfKin);
                db.SaveChanges(); TempData["AlertMessage"] = "Next of Kin Added Succesfully.";
                return RedirectToAction("RecruiterDashboard", "Home");
            }

            ViewBag.clientID = new SelectList(db.Clients, "clientID", "clientTitle", nextOfKin.clientID);
            return View(nextOfKin);
        }

        // GET: NextOfKins/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextOfKin nextOfKin = db.NextOfKins.Find(id);
            if (nextOfKin == null)
            {
                return HttpNotFound();
            }
            ViewBag.clientID = new SelectList(db.Clients, "clientID", "clientTitle", nextOfKin.clientID);
            return View(nextOfKin);
        }

        // POST: NextOfKins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nID,Fname,Lname,tel,assoc,date_reg,clientID")] NextOfKin nextOfKin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nextOfKin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clientID = new SelectList(db.Clients, "clientID", "clientTitle", nextOfKin.clientID);
            return View(nextOfKin);
        }

        // GET: NextOfKins/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NextOfKin nextOfKin = db.NextOfKins.Find(id);
            if (nextOfKin == null)
            {
                return HttpNotFound();
            }
            return View(nextOfKin);
        }

        // POST: NextOfKins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NextOfKin nextOfKin = db.NextOfKins.Find(id);
            db.NextOfKins.Remove(nextOfKin);
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
