using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfessionalProfile.Models;
using Microsoft.EntityFrameworkCore;

namespace ProfessionalProfile.Controllers
{
    public class DetailController : Controller
    {
        private readonly MainContext _context;

        public DetailController(MainContext connect)
        {
            _context = connect;
        }

        [HttpGetAttribute]
        [RouteAttribute("Info/{id}")]
        public IActionResult Index(int id)
        {
            if (HttpContext.Session == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                User CurrentUser = _context.Users.SingleOrDefault(usr => usr.UserId == id);
                ViewBag.CurrentUser = CurrentUser;
                return View("Details");
            }
        }

        [HttpGetAttribute]
        [RouteAttribute("Home")]
        public IActionResult Home()
        {
            return RedirectToAction("Index", "Profile");
        }
    }
}