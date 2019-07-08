using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GymCompany.Models
{
    public class Manager
    {
        [Key]
        public string manID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public DateTime date_reg { get; set; }
    }
}