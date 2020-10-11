using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using DataLibrary.Models.Logic;

namespace ConsidÖvning.Models.ViewModels
{
	public class LibraryItemVM
	{
		public int Id { get; set; }
		public Category Category { get; set; }
		public string CategoryName { get; set; }

		[Required]
		public string Title { get; set; }

		[Required]
		public string Author { get; set; }
		public int? Pages { get; set; }

		[Display(Name = "Runtime minutes")]
		public int? RunTimeMinutes { get; set; }

		[Display(Name = "Can be borrowed")]
		public bool IsBorrowable { get; set; }
		public string Borrower { get; set; }

		[Display(Name = "Borrow date")]
		public DateTime? BorrowDate { get; set; }
		public string Type { get; set; }

		public List<string> TypeList { get; set; }
		public List<Category> CategoryList { get; set; }
		public string Acronym { get; set; }

		public LibraryItemVM() {
			TypeList = new List<string>() { "Book", "Reference book", "DVD", "Audio book" }; 
			CategoryList = GetCategories();
			IsBorrowable = true;


		}

		private List<Category> GetCategories() {

			var output = new List<Category>();
			var models = CategoryProcessor.LoadCategories();
			foreach (var item in models) {
				var mod = new Category();
				mod.CategoryName = item.CategoryName;
				mod.Id = item.Id;
				output.Add(mod);
			}
			
			return output;
		}


		 


	}
}