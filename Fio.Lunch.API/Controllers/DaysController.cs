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
    [Route("api/v1/Days")]
    public class DaysController : Controller
    {
        private readonly FioLunchAPIContext _context;

        public DaysController(FioLunchAPIContext context)
        {
            _context = context;
        }

        // GET: api/Days
        [HttpGet]
        public IEnumerable<Day> GetDay()
        {
            return _context.Day;
        }

        // GET: api/Days/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDay([FromRoute] DateTime id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var day = await _context.Day.SingleOrDefaultAsync(m => m.Date == id);

            if (day == null)
            {
                return NotFound();
            }

            return Ok(day);
        }

        // PUT: api/Days/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDay([FromRoute] DateTime id, [FromBody] Day day)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != day.Date)
            {
                return BadRequest();
            }

            _context.Entry(day).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DayExists(id))
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

        // POST: api/Days
        [HttpPost]
        public async Task<IActionResult> PostDay([FromBody] Day day)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            RefreshMeals(day);

            _context.Day.Add(day);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DayExists(day.Date))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDay", new { id = day.Date }, day);
        }

        // DELETE: api/Days/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDay([FromRoute] DateTime id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var day = await _context.Day.SingleOrDefaultAsync(m => m.Date == id);
            if (day == null)
            {
                return NotFound();
            }

            _context.Day.Remove(day);
            await _context.SaveChangesAsync();

            return Ok(day);
        }

        private bool DayExists(DateTime id)
        {
            return _context.Day.Any(e => e.Date == id);
        }

        private void RefreshMeals(Day day)
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
}