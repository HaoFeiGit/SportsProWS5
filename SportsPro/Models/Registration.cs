using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Models { 
    public class Registration
    {
        public int RegistrationID { get; set; }
        public int CustomerID { get; set; } // Foreign Key property
        public Customer Customer { get; set; } // Navigation property
        public int ProductID { get; set; } // Foreign Key property
        public Product Product { get; set; } // Navigation property

    }
}
