using Skeeetch.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skeeetch.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
           var db = new SkeeetchContext();
          //  var category = db.Categories.FirstOrDefault();
           
          //  var categories = db.Categories;

            return View();



        }
    }
}
