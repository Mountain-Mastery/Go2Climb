using System.ComponentModel.DataAnnotations;

namespace Subscription.API.Resources;

public class SaveSubscriptionResource
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required]
    [MaxLength(300)]
    public string Description { get; set; }
}
