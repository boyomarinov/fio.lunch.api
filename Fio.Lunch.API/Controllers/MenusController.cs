using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fio.Lunch.API.Models;

namespace Fio.Lunch.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/Menus")]
    public class MenusController : Controller
    {
        private readonly FioLunchAPIContext _context;

        public MenusController(FioLunchAPIContext context)
        {
            _context = context;
        }

        // GET: api/Menus
        [HttpGet]
        public IEnumerable<Menu> GetMenu()
        {
            return _context.Menu.Include(m => m.Days);
        }

        [HttpGet]
        [Route("current")]
        public Menu GetCurrentMenu()
        {
            return _context.Menu.Include(m=>m.Days).Where(m => m.Days.Min(d => d.Date.DayOfYear) <= DateTime.Now.DayOfYear && m.Days.Max(d => d.Date.DayOfYear) >= DateTime.Now.DayOfYear).First();
        }

        [HttpGet]
        [Route("active")]
        public IEnumerable<Menu> GetActiveMenus()
        {
            return _context.Menu.Include(m => m.Days).Where(m => m.IsActive);
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenu([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menu = await _context.Menu.Include(m => m.Days).SingleOrDefaultAsync(m => m.Id == id);

            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu);
        }

        // PUT: api/Menus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu([FromRoute] int id, [FromBody] Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menu.Id)
            {
                return BadRequest();
            }

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Menus
        [HttpPost]
        public async Task<IActionResult> PostMenu([FromBody] Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RefreshMenuDays(menu);
            RefreshMeals(menu);

            _context.Menu.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
        }

        private void RefreshMeals(Menu menu)
        {
            foreach (var day in menu.Days)
            {
                var meals = new List<Meal>();
                foreach (var meal in day.Meals)
                {
                    var mmm = _context.Meal.Where(m => m.Id == meal.Id).FirstOrDefault();
                    if (mmm == null)
                        mmm = meal;

                    meals.Add(mmm);
                }

                day.Meals = meals;
            }
        }

        private void RefreshMenuDays(Menu menu)
        {
            var menuDays = new List<Day>();
            foreach (var day in menu.Days)
            {
                var ddd = _context.Day.Where(d => d.Date == day.Date).FirstOrDefault();
                if (ddd == null)
                    ddd = day;

                menuDays.Add(ddd);

            }
            menu.Days = menuDays;
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menu = await _context.Menu.SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();

            return Ok(menu);
        }

        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.Id == id);
        }
    }
}