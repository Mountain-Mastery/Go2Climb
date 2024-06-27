using Subscription.API.Resources;
using AutoMapper;

namespace Subscription.API.Mapping;

public class ResourceToModelSubscription : Profile
{
    public ResourceToModelSubscription()
    {

        CreateMap<SaveSubscriptionResource, Domain.Models.Subscription>();
        CreateMap<UpdateSubscriptionRequest, Domain.Models.Subscription>()
            .ForAllMembers(options => options.Condition(
                (source, Target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                    return true;
                }));
    }
}
