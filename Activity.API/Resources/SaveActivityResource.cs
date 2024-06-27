using System.ComponentModel.DataAnnotations;

namespace Activity.API.Resources;

public class SaveActivityResource
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required]
    [MaxLength(300)]
    public string Description { get; set; }
}
