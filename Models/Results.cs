
namespace RaceDay.Api.Models;

// Stores the finishing result for a participant's enrolment.
public class Result
{
    // Primary key that uniquely identifies each result.
    public int ResultId { get; set; }

    // Foreign key linking the result to an enrolment.
    public int EnrolmentId { get; set; }

    // Navigation property linking to the enrolment.
    public Enrolment? Enrolment { get; set; }

    // The time taken by the participant to finish the event.
    public TimeSpan FinishTime { get; set; }

    // The participant's finishing position in the event.
    public int FinishingPosition { get; set; }
}

