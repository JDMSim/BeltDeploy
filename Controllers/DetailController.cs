using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace WeddingPlanner.Controllers
{
    public class DetailController : Controller
    {
        private readonly WeddingContext _context;

        public DetailController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGetAttribute]
        [RouteAttribute("Detail")]
        public IActionResult Index()
        {
            List<Guest> guests = _context.Guests.Where(gst => gst.WeddingId == (int)TempData["WeddingID"])
                                                                                .Include(gst => gst.User)
                                                                                .Include(gst => gst.Wedding)
                                                                                .ToList();
            ViewBag.Guests = guests; 
            ViewBag.FrstWed = TempData["FirstWedder"];
            ViewBag.ScndWed = TempData["SecondWedder"];
            ViewBag.Date = TempData["Date"];
            ViewBag.Address = TempData["Address"];
            return View("Details");
        }
    }
}