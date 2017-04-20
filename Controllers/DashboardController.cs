using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
{
    public class DashboardController : Controller
    {
        private readonly WeddingContext _context;

        public DashboardController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGetAttribute]
        [RouteAttribute("Dashboard")]
        public IActionResult Index()
        {
            List<Wedding> AllWeddings = _context.Weddings
                                        .Include(wed => wed.Guests)
                                            // .ThenInclude(g => g.User)
                                        // .Include(wed => wed.User)
                                            .ToList();
                                                                                               
            ViewBag.Weddings = AllWeddings;
            ViewBag.UserID = HttpContext.Session.GetInt32("UserID");
            return View("Dashboard");
        }

        [HttpGetAttribute]
        [RouteAttribute("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGetAttribute]
        [RouteAttribute("NewWedding")]
        public IActionResult NewWedding()
        {
            if(HttpContext.Session == null)
            {
                return View("Index", "Home");
            }
            else
            {
                return View("NewWedding");
            }
        }

        [HttpPostAttribute]
        [RouteAttribute("Create")]
        public IActionResult AddWedding(WeddingViewModel weddingViewModel)
        {
            if (ModelState.IsValid)
            {
                Wedding NewWedding = new Wedding
                {
                    FirstWedder = weddingViewModel.WedderOne,
                    SecondWedder = weddingViewModel.WedderTwo,
                    WeddingDate = weddingViewModel.Date,
                    Address = weddingViewModel.Address,
                    UserId = (int)HttpContext.Session.GetInt32("UserID")
                };
                _context.Add(NewWedding);
                _context.SaveChanges();
                return  RedirectToAction("Index", "Deatail");
            }
            else
            {
                ViewBag.err = weddingViewModel;
                return View("NewWedding", weddingViewModel);
            }
        }

        [HttpGetAttribute]
        [RouteAttribute("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if(HttpContext.Session == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                List<Guest> CurrGuests = _context.Guests.Where(gst => gst.WeddingId == id).ToList();
                if (CurrGuests.Count > 0)
                {
                    foreach(Guest gst in CurrGuests)
                    {
                        _context.Remove(gst);
                        _context.SaveChanges();
                    }
                    
                }

                Wedding currWedding = _context.Weddings.SingleOrDefault(wed => wed.WeddingId == id);
                _context.Remove(currWedding);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpGetAttribute]
        [RouteAttribute("RSVP/{id}")]
        public IActionResult RSVP(int id)
        {
            Guest NewGuest = new Guest
            {
                UserId = (int)HttpContext.Session.GetInt32("UserID"),
                WeddingId = id
            };
            _context.Add(NewGuest);
            _context.SaveChanges();
            Wedding wdng = _context.Weddings.SingleOrDefault(wedding => wedding.WeddingId == id);
            TempData["FirstWedder"] = wdng.FirstWedder;
            TempData["SecondWedder"] = wdng.SecondWedder;
            TempData["Date"] = wdng.WeddingDate;
            TempData["Address"] = wdng.Address;
            TempData["WeddingID"] = id;
            return RedirectToAction("Index", "Detail");
        }

        [HttpGetAttribute]
        [RouteAttribute("Info/{id}")]
        public IActionResult Info(int id)
        {
            Wedding wdng = _context.Weddings.SingleOrDefault(wedding => wedding.WeddingId == id);
            TempData["FirstWedder"] = wdng.FirstWedder;
            TempData["SecondWedder"] = wdng.SecondWedder;
            TempData["Date"] = wdng.WeddingDate;
            TempData["Address"] = wdng.Address;
            TempData["WeddingID"] = id;
            return RedirectToAction("Index", "Detail");
        }

        [HttpGetAttribute]
        [RouteAttribute("UnRSVP/{id}")]
        public IActionResult UnRSVP(int id)
        {
            Guest uninvite = _context.Guests.SingleOrDefault(gst => gst.UserId == (int)HttpContext.Session.GetInt32("UserID") && gst.WeddingId == id);
            _context.Remove(uninvite);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}