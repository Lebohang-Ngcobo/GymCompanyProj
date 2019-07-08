using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GymCompany.Models
{
    public class NextOfKin
    {
        [Key]
        public string nID { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string tel { get; set; }
        public string assoc { get; set; }
        public DateTime date_reg { get; set; }
        public Client Client { get; set; }
        public string clientID { get; set; }

    }
}