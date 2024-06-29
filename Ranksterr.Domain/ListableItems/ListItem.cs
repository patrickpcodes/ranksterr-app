using Ranksterr.Domain.Abstractions;

namespace Ranksterr.Domain.ListableItems;

public abstract class ListItem : Entity
{
    protected Guid Id { get; set; }
    public string Title { get; set; }
    public ICollection<ListItemAssignment> ListItemAssignments { get; set; }
}