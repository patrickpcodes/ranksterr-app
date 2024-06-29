namespace Ranksterr.Domain.ListableItems;

public class ItemList
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<ListItemAssignment> ListItemAssignments { get; set; }}