using Go2Climb.API.Domain.Services.Communication;
using Go2Climb.API.Reports.Domain.Models;

namespace Go2Climb.API.Reports.Domain.Services.Communication
{
    public class ReportResponse : BaseResponse<Report>
    {
        //UNHAPPY
        public ReportResponse(string message) : base(message)
        {
        }
        //HAPPY
        public ReportResponse(Report resource) : base(resource)
        {
        }
    }
}