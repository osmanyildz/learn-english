using System;
using System.Threading.Tasks;
using learnenglish.webui.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using learnenglish.webui.EmailServices;
using learnenglish.webui.Models;
using System.Diagnostics.CodeAnalysis;
using learnenglish.data.Abstract;
using Microsoft.AspNetCore.Mvc.Formatters;


namespace learnenglish.webui.Controllers
{
    // [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        private ILevelRepository _levelRepository;
        private ILessonRepository _lessonRepository;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, ILevelRepository levelRepository, ILessonRepository lessonRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _levelRepository = levelRepository;
            _lessonRepository = lessonRepository;
        }
        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // var user = await _userManager.FindByNameAsync(model.UserName);
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Bu kullanıcı adı ile daha önce hesap oluşturulmamış");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Lütfen email hesabınıza gelen link ile üyeliğinizi onaylayınız.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "~/");
            }

            ModelState.AddModelError("", "Girilen kullanıcı adı veya parola yanlış");
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new User()
            {
                LevelId = 1,
                IsBeginner = 1,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                // generate token
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = user.Id.ToString(),
                    token = code.ToString()
                });


                // email
                await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız.", $"Lütfen email hesabınızı onaylamak için linke <a href='http://localhost:5063{url}'>tıklayınız.</a>");

                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Bilinmeyen hata oldu lütfen tekrar deneyiniz.");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                // CreateMessage("Geçersiz token.","danger");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    System.Console.WriteLine("Hesabınız onaylandı");
                    // CreateMessage("Hesabınız onaylandı.","success");
                    ViewBag.Message = "Hesabınız Başarılı Bir Şekilde Onaylandı.";
                    return View();
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ViewBag.Message = "Hesabınız Onaylanamadı. Hata: " + item.Description;
                        System.Console.WriteLine(item.Description);
                    }
                }
            }
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (String.IsNullOrEmpty(Email))
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return View();
            }
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var url = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id.ToString(),
                token = code.ToString()
            });

            await _emailSender.SendEmailAsync(Email, "Parola Yenileme Linki", $"Parolanızı yenilemek için linke <a href='http://localhost:5063{url}'>tıklayınız.</a>");
            return View();
        }
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                System.Console.WriteLine("User null geldi");
            }
            else
            {
                var email = await _userManager.GetEmailAsync(user);
                System.Console.WriteLine("Email-->" + email);
                var model = new ResetPasswordModel()
                {
                    Token = token,
                    Email = email
                };
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);

            var userName = user.UserName;
            var profileUrl = user.ProfileUrl;
            var levelName = _levelRepository.GetLevelById(user.LevelId);


            var lastLessonName = _lessonRepository.GetLessonsByLevelId(user.LevelId).Where(l => l.Id == user.LastLessonId).FirstOrDefault();


            var lessonsNames = _lessonRepository.GetAllLessons().Where(a => a.LevelId == user.LevelId).Select(l => l.LessonTitle).ToList();

            var model = new ProfileModel()
            {
                UserName = userName,
                LevelName = levelName,
                LastLessonName = lastLessonName.LessonTitle,
                LessonsNames = lessonsNames,
                Email = user.Email,
                ProfileUrl = profileUrl
            };

            // System.Console.WriteLine("1.parametre: " + userName);
            // System.Console.WriteLine("2.parametre: " + lastLessonName.LessonTitle);
            // System.Console.WriteLine("3.parametre: " + levelName);
            // System.Console.WriteLine("4.parametre: " + lessonsNames.ElementAt(0));
            return View(model);
        }
        public IActionResult AccessDenied()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileModel model, IFormFile ProfileFile)
        {
            var user = await _userManager.GetUserAsync(User);
            if (model != null)
            {
                System.Console.WriteLine("---------------------------------------");
                System.Console.WriteLine(model.UserName);
                System.Console.WriteLine(model.Email);
                System.Console.WriteLine("----------------------------------------");

                //UserName Güncellemesi
                user.UserName = model.UserName;
                await _userManager.UpdateAsync(user);

               

                // if(model.ProfileUrl.CompareTo(user.ProfileUrl)!=0 && user.ProfileUrl==null){
                //     user.ProfileUrl=model.ProfileUrl;
                //    await _userManager.UpdateAsync(user);
                // }
                if (ProfileFile != null)
                {
                    //     if(profileFile.FileName.CompareTo(user.ProfileUrl)!=0){
                    //     user.ProfileUrl=model.ProfileUrl;
                    //    await _userManager.UpdateAsync(user);
                    //     }else{
                    //         return RedirectToAction("Profile");
                    //     }
                    var extension1 = Path.GetExtension(ProfileFile.FileName);
                    var randomName1 = string.Format($"{Guid.NewGuid()}{extension1}");
                    var imgPath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomName1);
                    user.ProfileUrl = randomName1;
                    await _userManager.UpdateAsync(user);
                    using (var stream = new FileStream(imgPath1, FileMode.Create))
                    {
                        await ProfileFile.CopyToAsync(stream);
                    }
                }
                else
                {
                    System.Console.WriteLine("-----------------------------");
                    System.Console.WriteLine("profileFile değiştirilmedi");
                    System.Console.WriteLine("-----------------------------");
                }
                 //Email Güncellemesi
                if (model.Email.CompareTo(user.Email) != 0)
                {//Yani email değiştiyse
                    user.Email = model.Email;
                    user.EmailConfirmed = false;
                    await _userManager.UpdateAsync(user);
                    
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail", "Account", new
                    {
                        userId = user.Id.ToString(),
                        token = code.ToString()
                    });

                    // email
                    await _emailSender.SendEmailAsync(model.Email, "Hesabınızı onaylayınız.", $"Lütfen email hesabınızı onaylamak için linke <a href='http://localhost:5063{url}'>tıklayınız.</a>");

                    RedirectToAction("Logout");
                }
            }
            return RedirectToAction("Profile");
        }

    }
}