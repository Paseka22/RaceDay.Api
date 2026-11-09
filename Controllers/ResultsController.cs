using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        // Connect the controller to the database.
        public ResultsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/Results
        // Retrieves all race results.
        [HttpGet]
        public async Task<IActionResult> GetResults()
        {
            var results = await _context.Results.ToListAsync();
            return Ok(results);
        }

        // GET: api/Results/1
        // Retrieves one result by its ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetResult(int id)
        {
            var result = await _context.Results.FindAsync(id);

            if (result == null)
            {
                return NotFound("Result not found.");
            }

            return Ok(result);
        }

        // POST: api/Results
        // Creates a new race result.
        [HttpPost]
        public async Task<IActionResult> CreateResult(Result result)
        {
            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        // PUT: api/Results/1
        // Updates an existing race result.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResult(int id, Result result)
        {
            var existingResult = await _context.Results.FindAsync(id);

            if (existingResult == null)
            {
                return NotFound("Result not found.");
            }

            _context.Entry(existingResult).CurrentValues.SetValues(result);

            // Preserve the original result ID.
            existingResult.ResultId = id;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Results/1
        // Deletes a race result.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var result = await _context.Results.FindAsync(id);

            if (result == null)
            {
                return NotFound("Result not found.");
            }

            _context.Results.Remove(result);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}