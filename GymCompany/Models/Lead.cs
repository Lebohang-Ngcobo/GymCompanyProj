using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GymCompany.Models
{
    public class Lead
    {
        [Key]
        public string lID { get; set; }
        public string fname { get; set; }
        public string lanme { get; set; }
        public string cno { get; set; }
        public int priority { get; set; }
        public string status { get; set; }
        public Employee Employee { get; set; }
        public string empID { get; set; }
        public DateTime dateupdated { get; set; }

    }
}