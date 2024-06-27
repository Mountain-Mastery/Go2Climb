using Shared;

namespace Activity.API.Domain.Services.Communication;

public class ActivityResponse : BaseResponse<Models.Activity>
{
    public ActivityResponse(string message) : base(message)
    {

    }
    //HAPPY
    public ActivityResponse(Models.Activity resource) : base(resource)
    {

    }
}
