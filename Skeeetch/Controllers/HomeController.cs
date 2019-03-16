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
            SkeeetchContext db = new SkeeetchContext();
            var hi = db.Categories.FirstOrDefault();
            return View();
        }
    }
}
