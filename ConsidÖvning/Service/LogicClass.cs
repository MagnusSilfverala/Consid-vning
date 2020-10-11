using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ConsidÖvning.Models;
using ConsidÖvning.Models.ViewModels;
using DataLibrary.Models.Logic;

namespace ConsidÖvning.Service
{
	public class LogicClass
	{

		//Category logic
		public static LibraryItemVM CleanCategory(LibraryItemVM model) {

			var category = new Category();
			var tempCategory = CategoryProcessor.GetCategoryByName(model.CategoryName);
			category.CategoryName = tempCategory.CategoryName;
			category.Id = tempCategory.Id;
			model.Category = category;
			return model;
		}
		public static Category GetCategoryById(int id) {

			var category = new Category();
			var tempCategory = CategoryProcessor.EditCategory(id);
			category.CategoryName = tempCategory.CategoryName;
			category.Id = tempCategory.Id;

			return category;
		}
		public static LibraryItemVM SerCategoryFromVM(LibraryItemVM model) {
			var category = model.CategoryName;
			if (model.Category.CategoryName != category) {
				foreach (var item in model.CategoryList) {
					if (item.CategoryName == category) {
						model.Category = item;
					}
				}

			}
			return model;
		}

		// Library item logic
		public static LibraryItemVM SetIsBorrowable(LibraryItemVM model) {

			if (model.Type == "Reference book") {
				model.IsBorrowable = false;
			}
			return model;
		}

		public static List<LibraryItemVM> SetAcronym(List<LibraryItemVM> models) {
			foreach (var model in models) {
				var acronym = string.Join(string.Empty,
			  model.Title.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s[0])
			  );
				if(acronym.Length > 1) {
					model.Acronym = acronym.ToUpper();
				}
				
			}
			

			return models;
		}

		public static LibraryItemVM SetBorrowDate(LibraryItemVM model) {
			if (model.Borrower != null ) {
				model.BorrowDate =  DateTime.Now;
			} else {
				model.BorrowDate = null;
			}
				

			return model;

		}

		// Employee logic
		public static EmployeeVM setRole(EmployeeVM model) {
			if (model.IsCEO) {
				model.Role = "CEO";
			} else if (model.IsManager) {
				model.Role = "Manager";
			} else {
				model.Role = "Employee";
			}

			return model;
		}

		public static EmployeeVM setDisplayName(EmployeeVM model) {
			if (model.ManagerList != null) {
				foreach (var item in model.ManagerList) {
					var sb = new StringBuilder();
					sb.Append(item.FirstName);
					sb.Append(" ");
					sb.Append(item.LastName);
					item.DisplayName = sb.ToString();
				}
			} 
			return model;
		}


		public static int CheckManager(EmployeeVM model) {

			if (model.SelectedManager != null) {
				int managerId = int.Parse(model.SelectedManager);
				var ceo = EmployeeProcessor.CheckForCEO();
				if(ceo.Id == managerId && model.IsManager == false) {
					return 0;
				}
				return managerId;
			}
			return -1;
		}

		public static int CheckManagerForDelete(EmployeeVM model) {

			if (model.IsManager) {
				int managerId = model.Id;

				var ceo = EmployeeProcessor.CheckForCEO();
				if (ceo.Id == managerId && model.IsManager == false) {
					return 0;
				}
				return managerId;
			}
			return -1;
		}

		public static string GetCEOErrorMessage(DataLibrary.Models.EmployeesModel currentCeo) {
			var sb = new StringBuilder();
			sb.Append("THERE CAN ONLY BE ONE!   ");
			sb.Append(currentCeo.FirstName);
			sb.Append(" ");
			sb.Append(currentCeo.LastName);
			sb.Append(" is already CEO");
			return sb.ToString();
		}

		public static decimal CalculateSalary(EmployeeVM model) {
			var rank = model.SalaryRank;
			double koef;
			if (model.IsCEO) {
				koef = 2.725;

			}else if (model.IsManager) {
				koef = 1.725;
			} else {
				koef = 1.125;
			}
			return (decimal) koef * rank;
		}

	}
}