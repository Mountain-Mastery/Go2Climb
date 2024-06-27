using Subscription.API.Resources;
using AutoMapper;
namespace Subscription.API.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {

        CreateMap<Domain.Models.Subscription, SubscriptionResource>();

    }
}