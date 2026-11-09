
namespace RaceDay.Api.Models;

public class Route
{
    public int RouteId { get; set; }

    public string RouteName { get; set; } = string.Empty;

    public string StartLocation { get; set; } = string.Empty;

    public string EndLocation { get; set; } = string.Empty;

    public decimal Distance { get; set; }

    public string RouteDescription { get; set; } = string.Empty;

    public int EventId { get; set; }

    public Event? Event { get; set; }
}

