using Report.API.Resources;
using Report.API.Services.Resources;

namespace Report.API.Resources
{
    public class ReportResource
    {
        public int Id { get; set; }
        public int ServiceId { get; set; } 
        public int CustomerId { get; set; }
        public string Date { get; set; }
        public string Comment { get; set; }
        public ServiceResource Service { get; set; }
        public CustomerResource Customer { get; set; }
    }
}