namespace Ranksterr.Domain.ListableItems;

public class ListItemAssignment
{
    public Guid ListId { get; set; }
    public ItemList ItemList { get; set; }

    public Guid ListItemId { get; set; }
    public ListItem ListItem { get; set; }
}
