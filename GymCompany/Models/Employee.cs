using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace GymCompany.Models
{
    public class Employee
    {
        [Key]
        public string empID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string position { get; set; }
        public string status { get; set; }
        public DateTime date_reg { get; set; }
        public ICollection<Lead> Leads { get; set; }
        public ICollection<Client> Clients { get; set; }

    }
}