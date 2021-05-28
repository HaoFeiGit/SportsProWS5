using System;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Technician
    {
		public int TechnicianID { get; set; }	   

		[Required(ErrorMessage = "Maximum lenght exceeded"), MaxLength(50)]
		public string Name { get; set; }

		[Required(ErrorMessage = "This is not a valid email address")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[MaxLength(15), RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}",
		 ErrorMessage = "Phone Number must be in the (999)999-9999 format")]
		public string Phone { get; set; }
	}
}
