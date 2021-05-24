using System;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Technician
    {
		public int TechnicianID { get; set; }	   

		[Required]
		public string Name { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[MaxLength(15), RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}",
		 ErrorMessage = "Phone Number must be in the (999)999-9999 format")]
		public string Phone { get; set; }
	}
}
