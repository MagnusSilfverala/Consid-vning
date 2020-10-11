using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsidÖvning.Models;
using DataLibrary;
using DataLibrary.Models.Logic;

namespace ConsidÖvning.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index() {
			return View();
		}
		
	}
}