using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrolmentsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        // Connect the controller to the database.
        public EnrolmentsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/Enrolments
        // Retrieves all enrolments.
        [HttpGet]
        public async Task<IActionResult> GetEnrolments()
        {
            var enrolments = await _context.Enrolments.ToListAsync();
            return Ok(enrolments);
        }

        // GET: api/Enrolments/1
        // Retrieves one enrolment by its ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnrolment(int id)
        {
            var enrolment = await _context.Enrolments.FindAsync(id);

            if (enrolment == null)
            {
                return NotFound("Enrolment not found.");
            }

            return Ok(enrolment);
        }

        // POST: api/Enrolments
        // Creates a new enrolment.
        [HttpPost]
        public async Task<IActionResult> CreateEnrolment(Enrolment enrolment)
        {
            _context.Enrolments.Add(enrolment);
            await _context.SaveChangesAsync();

            return Ok(enrolment);
        }

        // PUT: api/Enrolments/1
        // Updates an existing enrolment.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnrolment(int id, Enrolment enrolment)
        {
            var existingEnrolment = await _context.Enrolments.FindAsync(id);

            if (existingEnrolment == null)
            {
                return NotFound("Enrolment not found.");
            }

            _context.Entry(existingEnrolment).CurrentValues.SetValues(enrolment);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Enrolments/1
        // Deletes an enrolment.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrolment(int id)
        {
            var enrolment = await _context.Enrolments.FindAsync(id);

            if (enrolment == null)
            {
                return NotFound("Enrolment not found.");
            }

            _context.Enrolments.Remove(enrolment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}