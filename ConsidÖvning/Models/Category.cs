using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsidÖvning.Models
{
	public class Category
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "Category name")]
		public string CategoryName { get; set; }
	}
}