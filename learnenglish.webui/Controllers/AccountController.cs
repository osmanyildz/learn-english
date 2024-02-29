using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace learnenglish.webui.Controllers
{
    public class AccountController:Controller
    {
        public IActionResult Login(){
            return View();
        }
        public IActionResult Register(){
            return View();
        }
      
    }
}