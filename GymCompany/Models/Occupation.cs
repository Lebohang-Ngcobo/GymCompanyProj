using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GymCompany.Models
{
    public class Occupation
    {
        [Key]
        public string oID { get; set; }
        public string type { get; set; }
        public string tel { get; set; }
        public string adress { get; set; }
        public DateTime date_updated { get; set; }
        public Client Clinet { get; set; }
        public string clientID { get; set; }

    }
}