using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ConsidÖvning.Models;
using ConsidÖvning.Models.ViewModels;
using DataLibrary.Models;
using DataLibrary.Models.Logic;

namespace ConsidÖvning.Service
{
	public class mapping
	{

		// Mapp Library items
		public static LibraryItemVM MappOneLibraryItemToVM(LibraryItem item) {
			var model = new LibraryItemVM();
			var category = new Category();
			if (item.CategoryId > 0) {
				category = new Category() { Id = CategoryProcessor.EditCategory(item.CategoryId).Id, CategoryName = CategoryProcessor.EditCategory(item.CategoryId).CategoryName };
			}
			
			model.Author = item.Author;
			model.BorrowDate = item.BorroweDate;
			model.Borrower = item.Borrower;
			model.Category = category;
			model.Id = item.Id;
			model.IsBorrowable = item.IsBorrowable;
			model.Pages = item.Pages;
			model.RunTimeMinutes = item.RunTimeMinutes;
			model.Title = item.Title;
			model.Type = item.Type;

			return model;
		}

		public static List<LibraryItemVM> MappLibraryItemToVM( List<DataLibrary.Models.LibraryItemModel> input) {
			var output = new List<LibraryItemVM>();

			foreach (var item in input) {
				var model = new LibraryItemVM();
				var category = new Category();
				if (item.CategoryId > 0) {
					category = new Category() { Id = CategoryProcessor.EditCategory(item.CategoryId).Id, CategoryName = CategoryProcessor.EditCategory(item.CategoryId).CategoryName };
				}
				model.Author = item.Author;
				model.BorrowDate = item.BorrowDate;
				model.Borrower = item.Borrower;
				model.Category = category;
				model.Id = item.Id;
				model.IsBorrowable = item.IsBorrowable;
				model.Pages = item.Pages;
				model.RunTimeMinutes = item.RunTimeMinutes;
				model.Title = item.Title;
				model.Type = item.Type;
				output.Add(model);
			}


			return output;
		}

		public static LibraryItemModel MappToLibraryItemModelFromVM(LibraryItemVM item) {
			var model = new LibraryItemModel();

			var category = CategoryProcessor.GetCategoryByName(item.Category.CategoryName);

			model.Author = item.Author;
			model.BorrowDate = item.BorrowDate;
			model.Borrower = item.Borrower;
			model.CategoryId = category.Id;
			model.Id = item.Id;
			model.IsBorrowable = item.IsBorrowable;
			model.Pages = item.Pages;
			model.RunTimeMinutes = item.RunTimeMinutes;
			model.Title = item.Title;
			model.Type = item.Type;

			return model;
		}

		public static LibraryItemVM MappForEditToVM(DataLibrary.Models.LibraryItemModel item) {
			var model = new LibraryItemVM();
			var category = new Category();
			if (item.CategoryId > 0) {
				category = new Category() { Id = CategoryProcessor.EditCategory(item.CategoryId).Id, CategoryName = CategoryProcessor.EditCategory(item.CategoryId).CategoryName };
			}

			model.Author = item.Author;
			model.BorrowDate = item.BorrowDate;
			model.Borrower = item.Borrower;
			model.Category = category;
			model.Id = item.Id;
			model.IsBorrowable = item.IsBorrowable;
			model.Pages = item.Pages;
			model.RunTimeMinutes = item.RunTimeMinutes;
			model.Title = item.Title;
			model.Type = item.Type;

			return model;
		}


		// Mapp Employees
		public static List<EmployeeVM> MappEmployeeToVM(List<DataLibrary.Models.EmployeesModel> input) {
			var output = new List<EmployeeVM>();

			foreach (var item in input) {
				var model = new EmployeeVM();
				model.FirstName = item.FirstName;
				model.LastName = item.LastName;
				model.Id = item.Id;
				model.ManagerId = item.ManagerId;
				if(model.ManagerId > 0) {
					var manager = EmployeeProcessor.GetManager((int)model.ManagerId);
					var sb = new StringBuilder();
					sb.Append(manager.FirstName);
					sb.Append(" ");
					sb.Append(manager.LastName);
					model.DisplayName = sb.ToString();
				}
				model.IsManager = item.IsManager;
				model.IsCEO = item.IsCEO;
				model.Salary = item.Salary;
				model = LogicClass.setRole(model);
				output.Add(model);
			}
			return output;
		}

		public static EmployeesModel MappToEmployeeModelFromVM(EmployeeVM inModel) {

			var outModel = new EmployeesModel();
			outModel.Id = inModel.Id;
			outModel.FirstName = inModel.FirstName;
			outModel.LastName = inModel.LastName;
			outModel.Salary = inModel.Salary;
			outModel.IsCEO = inModel.IsCEO;
			outModel.IsManager = inModel.IsManager;
			outModel.ManagerId = inModel.ManagerId;

			return outModel;
		}
	}
}