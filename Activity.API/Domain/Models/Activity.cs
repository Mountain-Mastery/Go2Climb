using System.Text.Json.Serialization;

namespace Activity.API.Domain.Models;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IList<Service> Services { get; set; }
}