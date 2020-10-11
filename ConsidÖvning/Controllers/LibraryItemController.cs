using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsidÖvning.Models;
using ConsidÖvning.Models.ViewModels;
using ConsidÖvning.Service;
using DataLibrary.Models.Logic;

namespace ConsidÖvning.Controllers
{
    public class LibraryItemController : Controller
    {
        public ActionResult Index(string sortOrder)
        {

            var output = mapping.MappLibraryItemToVM( LibraryItemProcessor.LoadLibraryItem());
            output = LogicClass.SetAcronym(output);

            if(sortOrder == null && Session["sortOrderSession"] != null) {
                sortOrder = Session["sortOrderSession"].ToString();

            }

            switch (sortOrder) {
                case "type":
                    Session["sortOrderSession"] = sortOrder;
                    ViewBag.orderState = "type";
                    return View(output.OrderBy(x => x.Type ));
                case "category":
                    Session["sortOrderSession"] = null;
                    ViewBag.orderState = "category";
                    return View(output.OrderBy(x => x.Category.CategoryName));
                default:
                    return View(output.OrderBy(x => x.Category.CategoryName));
            }
        }

        public ActionResult Create() {
            var output = mapping.MappOneLibraryItemToVM(new LibraryItem());

            List<SelectListItem> listItemsCategory = new List<SelectListItem>();
			foreach (var item in output.CategoryList) {
                listItemsCategory.Add(new SelectListItem {
                    Text = item.CategoryName,
                    Value = item.CategoryName
                });
            }

            List<SelectListItem> listItemsTypes = new List<SelectListItem>();
            foreach (var item in output.TypeList) {
                listItemsTypes.Add(new SelectListItem {
                    Text = item,
                    Value = item
                });
            }


            ViewData["listItemsCategory"] = listItemsCategory;
            ViewData["listItemsTypes"] = listItemsTypes;
            return View(output);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LibraryItemVM model) {

            if(model.CategoryName != "" || model.CategoryName != null) {
                model = LogicClass.CleanCategory(model);
            }

            if (ModelState.IsValid) {
                model = LogicClass.SetIsBorrowable(model);
                LibraryItemProcessor.CreateLibraryItem(mapping.MappToLibraryItemModelFromVM(model));

                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        public ActionResult Delete(int id) {
            LibraryItemProcessor.DeleteItem(id);
           
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id) {
            var output = LibraryItemProcessor.EditLibraryItem(id);
            var newOutput = mapping.MappForEditToVM(output);
            newOutput.Category = LogicClass.GetCategoryById(output.CategoryId);

            List<SelectListItem> listItemsCategory = new List<SelectListItem>();
            foreach (var item in newOutput.CategoryList) {
                if (item.CategoryName == newOutput.Category.CategoryName) {
                    listItemsCategory.Add(new SelectListItem {
                        Text = item.CategoryName,
                        Value = item.CategoryName,
                        Selected = true
                    });
				} else {
                    listItemsCategory.Add(new SelectListItem {
                        Text = item.CategoryName,
                        Value = item.CategoryName
                    });
                }

               
            }

            List<SelectListItem> listItemsTypes = new List<SelectListItem>();
            foreach (var item in newOutput.TypeList) {
				if (item == newOutput.Type) {
                    listItemsTypes.Add(new SelectListItem {
                        Text = item,
                        Value = item,
                        Selected = true
                    });
				} else {
                    listItemsTypes.Add(new SelectListItem {
                        Text = item,
                        Value = item
                    });
                }
               
            }


            ViewData["listItemsCategory"] = listItemsCategory;
            ViewData["listItemsTypes"] = listItemsTypes;

            return View(newOutput);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LibraryItemVM model) {

            if (ModelState.IsValid) {
                model = LogicClass.SerCategoryFromVM(model);
                model = LogicClass.SetBorrowDate(model);

                var outModel = mapping.MappToLibraryItemModelFromVM(model);
                LibraryItemProcessor.SaveLibraryItem(outModel);
            }



            return RedirectToAction("Index");

        }
    }
}