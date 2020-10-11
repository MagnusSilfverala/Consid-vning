using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace ConsidÖvning.Models.ViewModels
{
	public class EmployeeVM
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "First name")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last name")]
		public string LastName { get; set; }

		[DisplayFormat(DataFormatString = "{0:0.0000}")]
		public decimal Salary { get; set; }

		[Display(Name = "Is CEO?")]
		public bool IsCEO { get; set; }

		[Display(Name = "Is Manager?")]
		public bool IsManager { get; set; }
		
		public int? ManagerId { get; set; }
		public string Role { get; set; }
		public List<EmployeeVM> ManagerList { get; set; }
		public List<EmployeeVM> ManagerList2 { get; set; }

		[Display(Name = "Manager")]
		public string SelectedManager { get; set; }
		public string DisplayName { get; set; }

		[Required]
		[Display(Name = "Salary rank 1 - 10")]
		public int SalaryRank { get; set; }

	}

	
	
}