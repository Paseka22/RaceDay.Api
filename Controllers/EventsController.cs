using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Data;
using RaceDay.Api.Models;

namespace RaceDay.Api.Controllers
{
    // Handles requests related to race events.
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        // Receives the database context through dependency injection.
        public EventsController(RaceDayDbContext context)
        {
            _context = context;
        }

        // GET: api/events
        // Returns all events stored in the database.
        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _context.Events.ToListAsync();

            return Ok(events);
        }

        // GET: api/events/5
        // Returns one event using its ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            var eventItem = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == id);

            // Returns 404 if the requested event does not exist.
            if (eventItem == null)
            {
                return NotFound("Event not found.");
            }

            return Ok(eventItem);
        }
        // POST: api/events
        // Allows an organiser to create a new race event.
        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event eventItem)
        {
            // Adds the new event to the database.
            _context.Events.Add(eventItem);

            // Saves the new event permanently.
            await _context.SaveChangesAsync();

            // Returns the created event with its new ID.
            return CreatedAtAction(
                nameof(GetEvent),
                new { id = eventItem.EventId },
                eventItem
            );
        }
        // PUT: api/events/5
        // Updates an existing event using its ID.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event eventItem)
        {
            // Find the event that needs to be updated.
            var existingEvent = await _context.Events.FindAsync(id);

            // Return 404 if the event does not exist.
            if (existingEvent == null)
            {
                return NotFound("Event not found.");
            }

            // Copy the updated information into the existing event.
            _context.Entry(existingEvent).CurrentValues.SetValues(eventItem);

            // Keep the original event ID.
            existingEvent.EventId = id;

            // Save the changes to the database.
            await _context.SaveChangesAsync();

            // Confirm that the update was successful.
            return NoContent();
        }

        // DELETE: api/events/5
        // Deletes an event using its ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            // Find the event in the database.
            var eventItem = await _context.Events.FindAsync(id);

            // Return 404 if the event does not exist.
            if (eventItem == null)
            {
                return NotFound("Event not found.");
            }

            // Remove the event from the database.
            _context.Events.Remove(eventItem);

            // Save the deletion.
            await _context.SaveChangesAsync();

            // Confirm that the event was deleted.
            return NoContent();
        }
    }

}