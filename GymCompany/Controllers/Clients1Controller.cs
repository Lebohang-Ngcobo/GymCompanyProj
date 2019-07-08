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
    public class Clients1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clients1
        public ActionResult Index()
        {
            var clients = db.Clients.Include(c => c.Employee).Include(c => c.Package);
            return View(clients.ToList());
        }

        // GET: Clients1/Details/5
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

        // GET: Clients1/Create
        public ActionResult Create()
        {
            ViewBag.empID = new SelectList(db.Employees, "empID", "fname");
            ViewBag.nID = new SelectList(db.NextOfKins, "nID", "Fname");
            ViewBag.pID = new SelectList(db.Packages, "pID", "type");
            return View();
        }

        // POST: Clients1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "clientID,clientTitle,clientFname,clientLname,type,passportno,IDno,postalAddress,postalCode,hometel,cellno,email,requirepersonal,status,empID,date_reg,nID,pID")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.empID = new SelectList(db.Employees, "empID", "fname", client.empID);
            ViewBag.pID = new SelectList(db.Packages, "pID", "type", client.pID);
            return View(client);
        }

        // GET: Clients1/Edit/5
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

        // POST: Clients1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "clientID,clientTitle,clientFname,clientLname,type,passportno,IDno,postalAddress,postalCode,hometel,cellno,email,requirepersonal,status,empID,date_reg,nID,pID")] Client client)
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

        // GET: Clients1/Delete/5
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

        // POST: Clients1/Delete/5
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
