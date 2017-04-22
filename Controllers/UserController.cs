using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfessionalProfile.Models;
using Microsoft.EntityFrameworkCore;

namespace ProfessionalProfile.Controllers
{
    public class UserController : Controller
    {
        private readonly MainContext _context;

        public UserController(MainContext connect)
        {
            _context = connect;
        }

        [HttpGetAttribute]
        [RouteAttribute("Users")]
        public IActionResult Index()
        {
            if (HttpContext.Session == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                List<User> AllUsers = _context.Users
                                            .Include(usr => usr.UserFollowed)
                                            .ToList();
                ViewBag.AllUsers = AllUsers;
                ViewBag.CurrentUser = HttpContext.Session.GetInt32("UserID");
                return View("Users");
            }
        }

        [HttpGetAttribute]
        [RouteAttribute("Connect/{id}")]
        public IActionResult Connect(int id)
        {
            Console.WriteLine(id);
            Network NewConnection = new Network{
                UserFollowedId = id,
                FollowerId = (int)HttpContext.Session.GetInt32("UserID"),
                Status = 3
            };
            _context.Add(NewConnection);
            _context.SaveChanges();
            return RedirectToAction("Index", "Profile");
        }
    }
}