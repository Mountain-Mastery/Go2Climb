using Agency.API.Resources;
using AutoMapper;

namespace Agency.API.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {

        CreateMap<SaveAgencyResource, Domain.Models.Agency>();
        CreateMap<UpdateAgencyRequest, Domain.Models.Agency>()
            .ForAllMembers(options => options.Condition(
                (source, Target, property) =>
                {
                    if (property == null) return false;
                    if (property.GetType() == typeof(string) && string.IsNullOrEmpty((string)property)) return false;
                    return true;
                }));
    }
}
