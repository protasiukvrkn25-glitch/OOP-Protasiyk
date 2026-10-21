
namespace lab12v20;

public class Event : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
}
