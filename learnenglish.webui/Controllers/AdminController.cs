using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using learnenglish.webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace learnenglish.webui.Controllers
{
    public class AdminController:Controller
    {
        private string content;
        [HttpGet]
        public IActionResult CreateContent(){
            return View();
        }
        [HttpPost]
        public IActionResult CreateContent(string deneme){
            content = deneme;
            System.Console.WriteLine(content);
             var d = new ShowModel(){
                contents=  content
            };
            return RedirectToAction("Showing",d);
        }
        public IActionResult Showing(ShowModel d){
            return View(d);
        }


    [HttpPost]
public IActionResult UploadImage(List<IFormFile> files)
{
    var filePath="";
    foreach(IFormFile photo in Request.Form.Files){
        string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", photo.FileName);
    using (var stream = new FileStream(imgPath,FileMode.Create))
    {
        photo.CopyTo(stream);
    }
    filePath=imgPath;
    }
    return RedirectToAction("CreateContent");
    }   
    [HttpGet]
    public IActionResult CreateQuiz(){
        return View();
    }
    [HttpPost]
    public IActionResult CreateQuiz(string question, int isCorrect){
        System.Console.WriteLine(question);
        System.Console.WriteLine(isCorrect);
        return View();

    }

    }
}