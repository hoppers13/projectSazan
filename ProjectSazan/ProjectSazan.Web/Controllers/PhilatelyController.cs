using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProjectSazan.Web.Controllers
{
    public class PhilatelyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Collection()
        {
            return View();
        }
    }
}