using AutoMapper;
using CardStorageService.Data;
using Grpc.Core;
using Microsoft.VisualBasic;
using System.Threading.Tasks;
using static ClientServiceProtos.ClientService;

namespace CardStorageService.Controllers.Services.Impl
{
    public class ClientService : ClientServiceBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

       
        public override Task<ClientServiceProtos.CreateClientResponse> Create(ClientServiceProtos.CreateClientRequest request, ServerCallContext context)
        {
            try
            {
                return Task.FromResult(new ClientServiceProtos.CreateClientResponse { 

                ClientId = _clientRepository.Create(_mapper.Map<Client>(request)),
                ErrorCode = 0,
                ErrorMessage = string.Empty
                
                });
            }
            catch
            {
                return Task.FromResult(new ClientServiceProtos.CreateClientResponse
                {
                    ClientId = -1,
                    ErrorCode = 101,
                    ErrorMessage = "Create Client Error"

                });
            }

        }
    }
}
