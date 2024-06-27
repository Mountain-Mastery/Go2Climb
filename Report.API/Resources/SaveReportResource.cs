using System.ComponentModel.DataAnnotations;

namespace Report.API.Resources
{
    public class SaveReportResource
    {
        [Required]
        public int ServiceId { get; set; }
        
        [Required]
        public int CustomerId { get; set; }
        
        [Required]
        public string Date { get; set; }
        
        [Required(ErrorMessage = "Comment is required")]
        [MaxLength(200)]
        public string Comment { get; set; }
    }
}