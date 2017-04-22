using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfessionalProfile.Models;

namespace ProfessionalProfile.Controllers
{
    public class HomeController : Controller
    {
        private readonly MainContext _context;

        public HomeController(MainContext connect)
        {
            _context = connect;
        }

        [HttpGet]
        [Route("Main")]
        public IActionResult Index()
        { 
            return View("Index");
        } 

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterUserViewModel UserViewModel)
        {
            if (ModelState.IsValid)
            {                
                 User TestUser = _context.Users.SingleOrDefault(usr => usr.Email == UserViewModel.Email);

                if (TestUser != null)
                {
                    ViewBag.emailExists = "Email already exists...";
                    return View("Index");
                }
                else
                {
                    User NewUser = new User
                    {
                        Name = UserViewModel.Name,
                        Email = UserViewModel.Email,
                        Password = UserViewModel.Password,
                        Description = UserViewModel.Description
                    };
                    _context.Add(NewUser);
                    _context.SaveChanges();

                    User CurrentUser = _context.Users.SingleOrDefault(user => user.Email == UserViewModel.Email);
                 
                    HttpContext.Session.SetInt32("UserID", CurrentUser.UserId);
                    HttpContext.Session.SetString("FirstName", CurrentUser.Name);

                    return RedirectToAction("Index", "Profile");   
                }         
            }
            else
            {
                ViewBag.RegUser = UserViewModel;
                return View("Index", UserViewModel);
            }
        }

        [HttpGet]
        [Route("LoggedIn")]
        public IActionResult LoggedIn()
        {
            ViewBag.name = HttpContext.Session.GetString("Name");
            return View("Success");
        }

        [HttpPost]
        [Route("Login")]
         public IActionResult Login(LoginUserViewModel UserViewModel)
        {
            if (ModelState.IsValid)
            {
                User CurrentUser = _context.Users.SingleOrDefault(usr => usr.Email == UserViewModel.LogEmail);
                
                if (CurrentUser == null)
                {
                    ViewBag.LoginEml = "Email does not exist...";
                    return View("Index");
                }
                else
                {
                    if (CurrentUser.Password != UserViewModel.LogPassword)
                    {
                        ViewBag.LoginPwd = "Incorrect password...";
                    return View("Index");
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("UserID", CurrentUser.UserId);
                        HttpContext.Session.SetString("Name", CurrentUser.Name);                        
                        return RedirectToAction("Index", "Profile");
                    }
                }
            }
            else
            {
                ViewBag.LogUser = UserViewModel;
                return View("Index", UserViewModel);
            }              
        }

        [HttpGetAttribute]
        [RouteAttribute("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
    }         
}