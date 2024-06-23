using Newtonsoft.Json.Linq;
using Ranksterr.Domain.Abstractions;

namespace Ranksterr.Domain.Listables;

public abstract class Listable : Entity
{
    public string Title { get; set; }
    public string ImageUrl { get; set; }
}