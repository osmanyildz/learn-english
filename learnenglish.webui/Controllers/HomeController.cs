using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnenglish.webui.Models;
using Microsoft.AspNetCore.Mvc;

namespace learnenglish.webui.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index(){
            return View();
        }
    }
}