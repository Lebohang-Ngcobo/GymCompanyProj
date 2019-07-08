using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GymCompany.Models
{
    public class Package
    {
        [Key]
        public string pID { get; set; }
        public string type { get; set; }
        public string cliedescriptionntFname { get; set; }
        public double price { get; set; }

        public DateTime date_updated { get; set; }

        public ICollection<Client> Clients { get; set; }

    }
}