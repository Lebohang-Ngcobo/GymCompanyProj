using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GymCompany.Models
{
    public class Client
    {
        [Key]
        public string clientID { get; set; }
        public string clientTitle { get; set; }
        public string clientFname { get; set; }
        public string clientLname { get; set; }
        public string type { get; set; }
        public string passportno { get; set; }

        [StringLength(maximumLength:13,MinimumLength =13,ErrorMessage ="Invalid ID Number")]
        public string IDno { get; set; }
        public string postalAddress { get; set; }
        public string postalCode { get; set; }
        public string hometel { get; set; }
        public string cellno { get; set; }
        public string email { get; set; }
        public string personatrainer { get; set; }
        public string status { get; set; }

        public Employee Employee { get; set; }
        public string empID { get; set; }


        public DateTime date_reg { get; set; }
       
        public ICollection<NextOfKin> NextOfKins { get; set; }

        public Package Package { get; set; }
        public string pID { get; set; }



        public ICollection<Occupation> Occupations { get; set; }

    }
}