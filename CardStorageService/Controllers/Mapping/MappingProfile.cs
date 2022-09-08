using AutoMapper;
using CardStorageService.Controllers.Models.Requests;
using CardStorageService.Data;
using ClientServiceProtos;

namespace CardStorageService.Controllers.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Card, CardDto>();
        CreateMap<CreateCardRequest, Card>();

        CreateMap<CreateClientRequest, Client>();

        CreateMap<Client, ClientServiceProtos.CreateClientRequest>();
        CreateMap<ClientServiceProtos.CreateClientRequest, Client>();
        
    }
}