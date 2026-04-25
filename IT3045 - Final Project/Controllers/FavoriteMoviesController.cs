using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IT3045___Final_Project.Data;
using IT3045___Final_Project.Models;

namespace IT3045___Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteMoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoriteMoviesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteMovie>>> Get(int? id)
        {
            if (id == null || id == 0)
                return await _context.FavoriteMovies.Take(5).ToListAsync();

            var item = await _context.FavoriteMovies.FindAsync(id);
            if (item == null) return NotFound();

            return Ok (item);
        }

        [HttpPost]
        public async Task<ActionResult<FavoriteMovie>> Post(FavoriteMovie item)
        {
            _context.FavoriteMovies.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FavoriteMovie item)
        {
            if (id != item.Id) return BadRequest();

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.FavoriteMovies.FindAsync(id);
            if (item == null) return NotFound();

            _context.FavoriteMovies.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
