using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRS.Data;
using CRS.Models;
using static CRS.Models.Seat;

namespace CRS.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ShowTimesController : Controller
    {
        private readonly CRSDbContext _context;

        public ShowTimesController(CRSDbContext context)
        {
            _context = context;
        }

        // GET: Administrator/ShowTimes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShowsTimes.ToListAsync());
        }

        // GET: Administrator/ShowTimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showTime = await _context.ShowsTimes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (showTime == null)
            {
                return NotFound();
            }

            return View(showTime);
        }

        // GET: Administrator/ShowTimes/Create
        public IActionResult Create()
        {
            var movies = _context.Movies.ToList();
            List<string> MoviesNames = new List<string>();
            foreach(var movie in movies)
            {
                MoviesNames.Add(movie.MovieTitle);
            }
            IEnumerable<SelectListItem> selectListMoviesNames = MoviesNames.Select(s => new SelectListItem
            {
                Value = s, // or set this to some unique identifier
                Text = s
            });
            ViewBag.MoviesNames = selectListMoviesNames;
            var halls = _context.Halls.ToList();
            List<string> HallsNames = new List<string>();
            foreach (var Hall in halls)
            {
                HallsNames.Add(Hall.HallName);
            }
            IEnumerable<SelectListItem> selectListHallsNames = HallsNames.Select(s => new SelectListItem
            {
                Value = s, // or set this to some unique identifier
                Text = s
            });
            ViewBag.HallsNames = selectListHallsNames;
            var model = new ShowTime { HallType = "imax" };
            return View(model);
            
        }

        // POST: Administrator/ShowTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShowTime showTime)
        {
            if (ModelState.IsValid)
            {
                //List<SeatState> seats = new List<SeatState>();




                var hall = _context.Halls.Where(o => showTime.HallName == o.HallName).First();
                showTime.HallType = hall.HallType;
                _context.Add(showTime);
                
                for (int i = 0; i < 100; i++)
                {
                    SeatsMap seatInfo = new SeatsMap();
                    seatInfo.HallName = showTime.HallName;
                    seatInfo.MovieName = showTime.MovieName;
                    seatInfo.DateAndTime = showTime.DateAndTime;
                    seatInfo.TheSeat = SeatState.Empty;
                    seatInfo.ShowTimeID = showTime.Id;
                    seatInfo.HallType = hall.HallType;
                    _context.SeatsMaps.Add(seatInfo);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(showTime);
        }

        // GET: Administrator/ShowTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showTime = await _context.ShowsTimes.FindAsync(id);
            if (showTime == null)
            {
                return NotFound();
            }
            return View(showTime);
        }

        // POST: Administrator/ShowTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,ShowTime showTime)
        {
            if (id != showTime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					var hall = _context.Halls.Where(o => showTime.HallName == o.HallName).First();
					showTime.HallType = hall.HallType;
					_context.Update(showTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowTimeExists(showTime.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(showTime);
        }

        // GET: Administrator/ShowTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showTime = await _context.ShowsTimes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (showTime == null)
            {
                return NotFound();
            }

            return View(showTime);
        }

        // POST: Administrator/ShowTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var showTime = await _context.ShowsTimes.FindAsync(id);
            var seatMaps = _context.SeatsMaps.ToList();
            var removeMap = (from y in seatMaps where y.HallName==showTime.HallName && y.MovieName==showTime.MovieName && y.DateAndTime==showTime.DateAndTime select y);
            var Res = _context.Reservations.Where(y => y.HallName == showTime.HallName && y.MovieName == showTime.MovieName && y.DateAndTime == showTime.DateAndTime);
            if (showTime != null)
            {
                _context.ShowsTimes.Remove(showTime);
                _context.SeatsMaps.RemoveRange(removeMap);
                _context.Reservations.RemoveRange(Res);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> ShowSeatsMap(int id)
        {
        var st = _context.ShowsTimes.Find(id);
        var seatsMap = _context.SeatsMaps.Where(y=> y.HallName == st.HallName && y.MovieName == st.MovieName && y.DateAndTime == st.DateAndTime).ToList();
        ViewBag.ShowTimeId = id;
        
        return View(seatsMap);
        }
        [HttpGet]
        public async Task<IActionResult> EditSeatsMap(int id)
        {
            var seat = _context.SeatsMaps.Find(id);
            return View(seat);
        }
        [HttpPost]
        public async Task<IActionResult> EditSeatsMap(SeatsMap sm)
        {
            var seat = _context.SeatsMaps.Update(sm);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ShowTimeExists(int id)
        {
            return _context.ShowsTimes.Any(e => e.Id == id);
        }
    }
}
