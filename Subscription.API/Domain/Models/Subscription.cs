using System.Text.Json.Serialization;

namespace Subscription.API.Domain.Models;

public class Subscription
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IList<Service> Services { get; set; }
}