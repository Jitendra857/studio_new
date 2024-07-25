using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soc_Management_Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // [AllowAnonymous]
        public ActionResult Header()
        {


            return View();
        }
        public ActionResult Footer()
        {


            return View();
        }
    }
}
