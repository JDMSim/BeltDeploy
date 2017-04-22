using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfessionalProfile.Models;
using Microsoft.EntityFrameworkCore;

namespace ProfessionalProfile.Controllers
{
    public class ProfileController : Controller
    {
        private readonly MainContext _context;

        public ProfileController(MainContext connect)
        {
            _context = connect;
        }

        [HttpGetAttribute]
        [RouteAttribute("Professional_Profile")]    
        public IActionResult Index()
        {
            if (HttpContext.Session == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                User CurrentUser = _context.Users.SingleOrDefault(usr => usr.UserId == (int)HttpContext.Session.GetInt32("UserID"));
                ViewBag.Name = CurrentUser.Name;
                ViewBag.Desc = CurrentUser.Description;
                ViewBag.UserID = CurrentUser.UserId;

                List<Network> InNetworkUsers = _context.Networks.Where(ntwrk => ntwrk.UserFollowedId == CurrentUser.UserId && ntwrk.Status == 1)
                                                                                                            .Include(ntwrk => ntwrk.Follower)
                                                                                                            .ToList();
                ViewBag.InNetworkUsers = InNetworkUsers;

                 List<Network> PendingNetworkUsers = _context.Networks.Where(ntwrk => ntwrk.UserFollowedId == CurrentUser.UserId && ntwrk.Status == 3)
                                                                                                            .Include(ntwrk => ntwrk.Follower)
                                                                                                            .ToList();
                ViewBag.PendingNetworkUsers = PendingNetworkUsers;
                return View("Profile");
            }
        }

         [HttpGetAttribute]
        [RouteAttribute("Accept")]
        public IActionResult Accept()
        {
            return RedirectToAction("Index");
        }

        [HttpGetAttribute]
        [RouteAttribute("Accept/{id}")]
        public IActionResult Accept(int id)
        {
            Network CurrentNetwork = _context.Networks.SingleOrDefault(ntwrk => ntwrk.NetworkId == id);
            CurrentNetwork.Status = 1;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGetAttribute]
        [RouteAttribute("Ignore/{id}")]
        public IActionResult Ignore(int id)
        {
            Network CurrentNetwork = _context.Networks.SingleOrDefault(ntwrk => ntwrk.NetworkId == id);
            CurrentNetwork.Status = 0;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}