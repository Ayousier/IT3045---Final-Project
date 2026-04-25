using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IT3045___Final_Project.Data;
using IT3045___Final_Project.Models;

namespace IT3045___Final_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteGameController : ControllerBase
    {
        private readonly AppDbContext _context;
        public FavoriteGameController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteGame>>> Get(int? id)
        {
            if (id.HasValue && id > 0)
            {
                var game = await _context.FavoriteGames.FindAsync(id);
                return game == null ? NotFound() : Ok(game);
            }

            var games = await _context.FavoriteGames.Take(5).ToListAsync();
            return Ok(games);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FavoriteGame game)
        {
            await _context.FavoriteGames.AddAsync(game);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = game.Id }, game);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FavoriteGame game)
        {
            if (id != game.Id) return BadRequest();

            _context.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _context.FavoriteGames.FindAsync(id);
            if (game == null) return NotFound();

            _context.FavoriteGames.Remove(game);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
