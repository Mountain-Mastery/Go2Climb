using Agency.API.Resources;
using AutoMapper;
namespace Agency.API.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {

        CreateMap<Domain.Models.Agency, AgencyResource>();
        CreateMap<Domain.Models.Agency, AuthenticateResponse>();

    }
}
