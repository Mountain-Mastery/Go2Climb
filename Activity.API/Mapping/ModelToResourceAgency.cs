using Activity.API.Resources;
using AutoMapper;
namespace Activity.API.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {

        CreateMap<Domain.Models.Activity, ActivityResource>();

    }
}