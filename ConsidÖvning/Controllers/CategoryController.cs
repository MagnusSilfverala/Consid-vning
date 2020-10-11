using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsidÖvning.Models;
using DataLibrary.Models.Logic;

namespace ConsidÖvning.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index(bool? flag)
        {
            var output = CategoryProcessor.LoadCategories();
            var models = new List<ConsidÖvning.Models.Category>();
			foreach (var item in output) {
                var cat = new Category();
                cat.CategoryName = item.CategoryName;
                cat.Id = item.Id;
                models.Add(cat);
			}

            if (flag == true) {
                ViewData["ErrorMessage"] = "You can't delete the category, references to this category exists";
            }

            return View(models);
        }

        public ActionResult Create(bool? flag) {
			if (flag == true) {
                ModelState.AddModelError("CategoryName", "Category already exists");
			}
            return View();
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model) {

            if (ModelState.IsValid) {
                var oldCategories = CategoryProcessor.LoadCategories();
                foreach (var item in oldCategories) {
                    if(item.CategoryName.ToUpper() == model.CategoryName.ToUpper()) {
                        return RedirectToAction("Create", new { flag = true });
                    }
                }

                 CategoryProcessor.CreateCategory(model.CategoryName);

                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        public ActionResult Delete(int id) {
            var canDelete = CategoryProcessor.checkReferences(id);
			if (canDelete) {
                CategoryProcessor.DeleteCategory(id);
			} else {
                return RedirectToAction("Index", new { flag = true });
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id) {
            var output = CategoryProcessor.EditCategory(id);
            var model = new ConsidÖvning.Models.Category();
            model.Id = output.Id;
            model.CategoryName = output.CategoryName;
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ConsidÖvning.Models.Category modelEdit) {
            if (ModelState.IsValid) {
                var outModel = new DataLibrary.Models.CategoryModel() {Id = modelEdit.Id, CategoryName = modelEdit.CategoryName };
                CategoryProcessor.SaveCategory(outModel);
            }

            return RedirectToAction("Index"); 
        }
    }
}