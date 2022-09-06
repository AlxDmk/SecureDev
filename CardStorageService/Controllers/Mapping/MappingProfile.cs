using AutoMapper;
using CardStorageService.Controllers.Models.Requests;
using CardStorageService.Data;

namespace CardStorageService.Controllers.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Card, CardDto>();
        CreateMap<CreateCardRequest, Card>();

        CreateMap<CreateClientRequest, Client>();
        
    }
}