using CRS.Data;
using CRS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Collections.Generic;
using System.Data.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRS.Controllers
{
    public class ReservationController : Controller
    {
        private CRSDbContext db;
        private SignInManager<IdentityUser> signInManager;
        private UserManager<IdentityUser> userManager;
        public ReservationController(CRSDbContext _db , SignInManager<IdentityUser> _signInManager , UserManager<IdentityUser> _userManager) 
        { 
            db = _db; 
            signInManager = _signInManager;
            userManager = _userManager;
        }
        public IActionResult Index(int? id)
        {
            if (id == null) 
            {
                return RedirectToAction("NotFound");
            }
            if (ModelState.IsValid) 
            {
                var Movie = db.Movies.Find(id);
                if (Movie == null) 
                {
					return RedirectToAction("NotFound");
                }
                /*for(int i = 0; i < 7; i++)
                {
                    ViewBag.ShowTime[i] = db.ShowsTimes.Where(y => y.DateAndTime.Date == DateTime.Now.AddDays(i).Date && y.MovieName == Movie.MovieTitle).ToList();

				}*/
				ViewBag.ShowTime0 = db.ShowsTimes.Where(y => y.DateAndTime.Date == DateTime.Now.Date && y.MovieName == Movie.MovieTitle).ToList();
				ViewBag.ShowTime1 = db.ShowsTimes.Where(y => y.DateAndTime.Date == DateTime.Now.AddDays(1).Date && y.MovieName == Movie.MovieTitle).ToList();
				ViewBag.ShowTime2 = db.ShowsTimes.Where(y => y.DateAndTime.Date == DateTime.Now.AddDays(2).Date && y.MovieName == Movie.MovieTitle).ToList();
				ViewBag.ShowTime3 = db.ShowsTimes.Where(y => y.DateAndTime.Date == DateTime.Now.AddDays(3).Date && y.MovieName == Movie.MovieTitle).ToList();
				ViewBag.ShowTime4 = db.ShowsTimes.Where(y => y.DateAndTime.Date == DateTime.Now.AddDays(4).Date && y.MovieName == Movie.MovieTitle).ToList();
				ViewBag.ShowTime5 = db.ShowsTimes.Where(y => y.DateAndTime.Date == DateTime.Now.AddDays(5).Date && y.MovieName == Movie.MovieTitle).ToList();
				ViewBag.ShowTime6 = db.ShowsTimes.Where(y => y.DateAndTime.Date == DateTime.Now.AddDays(6).Date && y.MovieName == Movie.MovieTitle).ToList();

				return View(Movie);
            }
            return View();
        }
        public IActionResult NotFound() 
        {
            return View();
        }
		public IActionResult ChooseYourSeat(DateTime DT, string MN, string hallType)
		{
            if (!signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }
            
            var st = db.ShowsTimes.Where(o=>o.MovieName == MN && o.DateAndTime==DT && o.HallType==hallType).SingleOrDefault();
			
			var seatsMap = db.SeatsMaps.Where(y => y.HallName == st.HallName && y.MovieName == st.MovieName && y.DateAndTime == st.DateAndTime).ToList();
            
            ViewBag.ShowTimeId = st.Id;
            ViewBag.SeatNumber = 0;
            
            return View(seatsMap);
			
		}
        public IActionResult ReserveSeat(DateTime DT, string MN, string hallType, int SeatNumber)
        {
			var st = db.ShowsTimes.Where(o => o.MovieName == MN && o.DateAndTime == DT && o.HallType == hallType).SingleOrDefault();
			var seatsMap = db.SeatsMaps.Where(y => y.HallName == st.HallName && y.MovieName == st.MovieName && y.DateAndTime == st.DateAndTime).ToList();
			ViewBag.SeatNumber = SeatNumber;
            
			return View("ChooseYourSeat", seatsMap);
        }
        [HttpGet]
        public IActionResult ResInfo(int SN,string MN,DateTime DT,string HN)
        {
            ViewBag.SN = SN;
            ViewBag.DT = DT;
            ViewBag.HN = HN;
            ViewBag.MN = MN;
            ViewBag.UserEmail = User.Identity.Name;
            return View();
        }
        [HttpPost]
        public IActionResult ResInfo(Reservation Res)
        {

            if (Res == null)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Reservations.Add(Res);
                var map=db.SeatsMaps.Where((y) => y.HallName == Res.HallName && y.MovieName == Res.MovieName && y.DateAndTime == Res.DateAndTime).ToList();
                for (int i = 0; i < 100; i++) 
                {
                    if (i == Res.SeatNumber-1)
                    {
                        map[i].TheSeat = Seat.SeatState.Reserved;
                    }    
                }
                db.SaveChanges();
            }
            return RedirectToAction("ResInfoWithLogin");
        }
        
        public IActionResult ResInfoWithLogin()
        {
            var Reservations = db.Reservations.Where(y => y.ClientEmail == User.Identity.Name);
            if(Reservations.IsNullOrEmpty())
            {
                return RedirectToAction("NoRes");
            }
            return View(Reservations);
        }

        /*public IActionResult ResInfoWithLogin(string Email)
        {
            var Reservations = db.Reservations.Where(y=>y.ClientEmail == Email);

            return View();
        }*/
        public IActionResult NoRes()
        {
            return View();
        }
    }
}
