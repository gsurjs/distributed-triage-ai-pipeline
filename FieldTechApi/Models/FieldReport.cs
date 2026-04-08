// in memory object for handling safety hazards or site updates

namespace FieldTechApi.Models;

public class FieldReport
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LoggedBy { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending"; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}