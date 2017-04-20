using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly WeddingContext _context;

        public HomeController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
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
                 User TestUser = _context.Users.SingleOrDefault(jae => jae.Email == UserViewModel.Email);

                if (TestUser != null)
                {
                    ViewBag.emailExists = "Email already exists...";
                    return View("Index");
                }
                else
                {
                    User NewUser = new User
                    {
                        FirstName = UserViewModel.FirstName,
                        LastName = UserViewModel.LastName,
                        Email = UserViewModel.Email,
                        Password = UserViewModel.Password,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _context.Add(NewUser);
                    _context.SaveChanges();

                    User CurrentUser = _context.Users.SingleOrDefault(user => user.Email == UserViewModel.Email);
                 
                    HttpContext.Session.SetInt32("UserID", CurrentUser.UserId);
                    HttpContext.Session.SetString("FirstName", CurrentUser.FirstName);

                    return RedirectToAction("Index", "Dashboard");   
                }         
            }
            else
            {
                ViewBag.RegUser = UserViewModel;
                return View("Index", UserViewModel);
            }
        }

        [HttpPost]
        [Route("Login")]
         public IActionResult Login(LoginUserViewModel UserViewModel)
        {
            if (ModelState.IsValid)
            {
                User CurrentUser = _context.Users.SingleOrDefault(jae => jae.Email == UserViewModel.LogEmail);
                
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
                        HttpContext.Session.SetString("FirstName", CurrentUser.FirstName);                        
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
            }
            else
            {
                ViewBag.LogUser = UserViewModel;
                return View("Index", UserViewModel);
            }              
        }
    }         
}