using System.Text.Json.Serialization;

namespace Customer.API.Domain.model;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public string PasswordHash { get; set; }
    public string PhoneNumber { get; set; }
    public string Photo { get; set; }
}