using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SportsPro.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Incidents = new HashSet<Incident>();
        }

        public int CustomerID { get; set; }
        [Required, MaxLength(50), Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, MaxLength(50), Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required, MaxLength(50),]
        public string Address { get; set; }
        [Required, MaxLength(50)]
        public string City { get; set; }
        [Required, MaxLength(50)]
        public string State { get; set; }
        [Required, MaxLength(20), Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        [Required, Display(Name = "Country")]
        public string CountryID { get; set; }

        [MaxLength(15), RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}",
         ErrorMessage = "Phone Number must be in the (999)999-9999 format")]
        public string Phone { get; set; }
        [MaxLength(50), EmailAddress]
        public string Email { get; set; }
        public string FullName => FirstName + " " + LastName; // read only property

        public virtual Country Country { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}