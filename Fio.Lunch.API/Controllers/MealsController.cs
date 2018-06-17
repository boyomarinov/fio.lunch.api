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
    [Route("api/v1/Meals")]
    public class MealsController : Controller
    {
        private readonly FioLunchAPIContext _context;

        public MealsController(FioLunchAPIContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IEnumerable<Meal> GetMeals(string keyword = "", string description = "")
        {
            IQueryable<Meal> menuContext = _context.Meal;
            if (!string.IsNullOrEmpty(keyword))
            {
                menuContext = menuContext.Where(m => m.Keywords.IndexOf(keyword, StringComparison.InvariantCultureIgnoreCase) > -1);
            }

            if (!string.IsNullOrEmpty(description))
            {
                menuContext = menuContext.Where(m => m.Description.IndexOf(description, StringComparison.InvariantCultureIgnoreCase) > -1);
            }

            return menuContext;
        }

        // GET: api/Meals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMeal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meal = await _context.Meal.SingleOrDefaultAsync(m => m.Id == id);

            if (meal == null)
            {
                return NotFound();
            }

            return Ok(meal);
        }

        [HttpGet]
        [Route("/api/v1/days/{id}/meals")]
        public async Task<IActionResult> GetMealsByDay([FromRoute] DateTime id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meals = _context.Day.Include(d => d.Meals).SingleOrDefault(d => d.Date == id).Meals;

            if (meals == null)
            {
                return NotFound();
            }

            return Ok(meals);
        }


        // PUT: api/Meals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal([FromRoute] int id, [FromBody] Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meal.Id)
            {
                return BadRequest();
            }

            _context.Entry(meal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
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

        // POST: api/Meals
        [HttpPost]
        [Route("/api/v1/days/{id}/meals")]
        public async Task<IActionResult> PostMeal(DateTime id, [FromBody] Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Day.Include(d => d.Meals).SingleOrDefault(d => d.Date == id).Meals.Add(meal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeal", new { id = meal.Id }, meal);
        }

        [HttpPost]
        public async Task<IActionResult> PostMeal([FromBody] Meal meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Meal.Add(meal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeal", new { id = meal.Id }, meal);
        }


        // DELETE: api/Meals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var meal = await _context.Meal.SingleOrDefaultAsync(m => m.Id == id);
            if (meal == null)
            {
                return NotFound();
            }

            _context.Meal.Remove(meal);
            await _context.SaveChangesAsync();

            return Ok(meal);
        }

        private bool MealExists(int id)
        {
            return _context.Meal.Any(e => e.Id == id);
        }
    }
}