using Report.API.Domain.Models;

namespace Report.API.Domain.Models
{
    public class Report
    {
        public int Id { get; set; }
        
        //Relationships
        public int ServiceId { get; set; } 
        public Service Service { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        public string Date { get; set; }
        public string Comment { get; set; } 
    }
}