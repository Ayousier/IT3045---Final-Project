using IT3045___Final_Project.Data;
using IT3045___Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IT3045___Final_Project.Controllers
{
    public class FavoriteFoodController
    {
        [ApiController]
        [Route("api/[controller]")]
        public class FavoriteFoodsController : ControllerBase
        {
            private readonly AppDbContext _context;

            public FavoriteFoodsController(AppDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<IActionResult> Get(int? id)
            {
                if (id.HasValue && id > 0)
                {
                    var food = await _context.FavoriteFoods.FindAsync(id);
                    return food == null ? NotFound() : Ok(food);
                }

                var foods = await _context.FavoriteFoods.Take(5).ToListAsync();
                return Ok(foods);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] FavoriteFood food)
            {
                await _context.FavoriteFoods.AddAsync(food);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = food.Id }, food);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] FavoriteFood food)
            {
                if (id != food.Id) return BadRequest();

                _context.Entry(food).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var food = await _context.FavoriteFoods.FindAsync(id);
                if (food == null) return NotFound();

                _context.FavoriteFoods.Remove(food);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}
//this is a test for a commit