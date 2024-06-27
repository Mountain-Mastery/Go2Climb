using Activity.API.Resources;
using AutoMapper;

namespace Activity.API.Mapping;

public class ResourceToModelActivity : Profile
{
    public ResourceToModelActivity()
    {

        CreateMap<SaveActivityResource, Domain.Models.Activity>();
        CreateMap<UpdateActivityRequest, Domain.Models.Activity>()
            .ForAllMembers(options => options.Condition(
                (source, Target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                    return true;
                }));
    }
}
