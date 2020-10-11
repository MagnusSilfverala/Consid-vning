using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace ConsidÖvning.Models
{
	public class Employees
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "First Name")]
		public string	FirstName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		public string	LastName { get; set; }

		[Required]
		public decimal Salary { get; set; }


		[Required]
		[Display(Name = "Is CEO?")]
		public bool IsCEO { get; set; }

		[Required]
		[Display(Name = "Is Manager?")]
		public bool IsManager { get; set; }

		[Display(Name = "Manager ID")]
		public int? ManagerId { get; set; }
	}
}