using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using learnenglish.data.Abstract;
using learnenglish.webui.Identity;
using learnenglish.webui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace learnenglish.webui.Controllers
{
    public class HomeController:Controller
    {
        ILessonRepository _lessonRepository;
        private UserManager<User> _userManager;
        
        public HomeController(ILessonRepository lessonRepository, UserManager<User> userManager)
        {
            _lessonRepository=lessonRepository;
            _userManager=userManager;
        }
        public async Task<IActionResult> Index(){
            var users = await _userManager.GetUsersInRoleAsync("User");
            var model=new IndexModel(){
                lessonQuantity=_lessonRepository.GetLessonQuantity(),
                userQuantity=users.Count
            };
            return View(model);
        }
    }
}