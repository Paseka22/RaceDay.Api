
namespace RaceDay.Api.Models;

public class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public int EventId { get; set; }

    public Event? Event { get; set; }
}
