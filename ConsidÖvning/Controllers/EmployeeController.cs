using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ConsidÖvning.Models;
using ConsidÖvning.Models.ViewModels;
using ConsidÖvning.Service;
using DataLibrary.Models;
using DataLibrary.Models.Logic;

namespace ConsidÖvning.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index(string message)
        {
			if (message != null) {
				ViewData["ErrorMessage"] = message;
			}
			var tempList = EmployeeProcessor.LoadEmployees();
			var output = mapping.MappEmployeeToVM(tempList);

			return View(output.OrderBy(x => x.Role == "Employee").ThenBy( x => x.Role == "Manager").ThenBy(x => x.Role == "CEO" ));
			
		}
		

		public ActionResult AddEmployee(string currentCEO) {
			ViewBag.Message = "Add a Employee";

			if(currentCEO != null ) {
				ViewData["ErrorMessage"] = currentCEO;
			}
			var model = new EmployeeVM();
			model.ManagerList = mapping.MappEmployeeToVM(EmployeeProcessor.GetManagers());
			model = LogicClass.setDisplayName(model);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddEmployee(EmployeeVM model) {


			if (ModelState.IsValid) {

				if (model.IsCEO) {
					var currentCeo2 = EmployeeProcessor.CheckForCEO();
					if (currentCeo2 != null) {
						var currentCeo = LogicClass.GetCEOErrorMessage(currentCeo2);
						return RedirectToAction("AddEmployee", new { currentCEO = currentCeo });
					}
					model.ManagerId = null;
					
				}

				if (!model.IsCEO) {
					model.ManagerId = LogicClass.CheckManager(model);
				}
				

				if (!model.IsCEO && model.ManagerId < 0  ) {
					string message ="Every employee need to have a manager, please select Manager" ;
					return RedirectToAction("AddEmployee", new { currentCEO = message });
				}else if(!model.IsCEO && model.ManagerId == 0) {
					string message = "Only a manager can have the CEO as their manager";
					return RedirectToAction("AddEmployee", new { currentCEO = message });
				}

				model.Salary = LogicClass.CalculateSalary(model);

				var outModel = mapping.MappToEmployeeModelFromVM(model);
				EmployeeProcessor.CreateEmployee(outModel);


				return RedirectToAction("Index");
			}
			return RedirectToAction("AddEmployee"); 

		}

		public ActionResult Delete(int id) {
			if (EmployeeProcessor.GetEmployeeNumForManager(id) == 0) {
				EmployeeProcessor.DeleteEmployee(id);

				return RedirectToAction("Index");
			}
			string message = "You can't delete a manager that still is managing employees";
			return RedirectToAction("Index", new { message = message });


		}

		public ActionResult Edit(int id, string currentCEO) {
			var tempModel = mapping.MappEmployeeToVM(EmployeeProcessor.EditEmployee(id));

			var output = tempModel[0];
			output.ManagerList = mapping.MappEmployeeToVM(EmployeeProcessor.GetManagers());
			output = LogicClass.setDisplayName(output);

			if (currentCEO != null) {
				ViewData["ErrorMessage"] = currentCEO;
			}
			return View(output);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EmployeeVM model) {

			if (ModelState.IsValid) {

				if (model.IsCEO) {
					var currentCeo2 = EmployeeProcessor.CheckForCEO();
					if (currentCeo2 != null) {
						var currentCeo = LogicClass.GetCEOErrorMessage(currentCeo2);
						return RedirectToAction("Edit", new {id = model.Id, currentCEO = currentCeo });
					}
					model.ManagerId = null;

				}
				if (!model.IsCEO) {
					model.ManagerId = LogicClass.CheckManager(model);
				}
				if (!model.IsCEO && model.ManagerId < 0) {
					string message = "Every employee need to have a manager, please select Manager";
					return RedirectToAction("Edit", new { id = model.Id, currentCEO = message });
				} else if (!model.IsCEO && model.ManagerId == 0) {
					string message = "Only a manager can have the CEO as their manager";
					return RedirectToAction("Edit", new { id = model.Id, currentCEO = message });
				}

				var outModel = mapping.MappToEmployeeModelFromVM(model);

				EmployeeProcessor.SaveEmployee(outModel);
			}



			return RedirectToAction("Index");

		}
	}
}