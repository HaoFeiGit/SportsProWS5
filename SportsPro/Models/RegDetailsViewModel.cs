using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsPro.Models
{
    //Custom view model to display the product registrations for a selected customer
    public class RegDetailsViewModel
    {
  
        public string CustomerName { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
