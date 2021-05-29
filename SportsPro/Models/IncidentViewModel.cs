using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Models
{//view model to pass data to the Add/Edit Incident page
    public class IncidentViewModel
    {
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Technician> Technicians { get; set; }
        public int IncidentID { get; set; }     // foreign key property
        public string AddOrDelete { get; set; }
    }
}
