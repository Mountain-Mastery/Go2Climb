using Shared;

namespace Subscription.API.Domain.Services.Communication;

public class SubscriptionResponse : BaseResponse<Models.Subscription>
{
    public SubscriptionResponse(string message) : base(message)
    {

    }
    //HAPPY
    public SubscriptionResponse(Models.Subscription resource) : base(resource)
    {

    }
}
