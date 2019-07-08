using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GymCompany.Models;

namespace GymCompany.Models
{
    public class Logic
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public List<Employee> getEmployees ()
        {
            return db.Employees.ToList();
        }

        public List<Package> getPackages()
        {
            return db.Packages.ToList();
        }

        public List<Client> getClients()
        {
            return db.Clients.ToList();
        }

        public List<Lead> getLeads()
        {
            return db.Leads.ToList();
        }

        public List<Lead> myLeads(string name)
        {
            var user = db.Employees.ToList().Find(x => x.email == name);
            return db.Leads.Where(x=>x.empID==user.empID).ToList();
        }

        public List<Client> myClients(string name)
        {
            var user = db.Employees.ToList().Find(x => x.email == name);
            return db.Clients.Where(x => x.empID == user.empID).ToList();
        }

        public List<NextOfKin> kin()
        {
            return db.NextOfKins.ToList();
        }

       public Client getById(string nokid)
        {
            return db.Clients.ToList().Find(x=>x.clientID==nokid);
        }


    }
}