using Shared;

namespace Agency.API.Domain.Services.Communication;

public class AgencyResponse : BaseResponse<Models.Agency>
{
    public AgencyResponse(string message) : base(message)
    {

    }
    //HAPPY
    public AgencyResponse(Models.Agency resource) : base(resource)
    {

    }
}
