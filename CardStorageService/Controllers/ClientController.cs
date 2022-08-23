using CardStorageService.Controllers.Services;
using CardStorageService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace CardStorageService.Controllers
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _repository;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientRepository repository, ILogger<ClientController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost("create")]    
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateClientRequest request)
        {
            try
            {
                var clientId = _repository.Create(new Client
                {
                    FirstName = request.FirstName,
                    Surname = request.Surname,
                    Patronymic  = request.Patronymic
                });
                return Ok(new CreateClientResponse
                {
                    ClientId = clientId
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create client error");
                return Ok(new CreateClientResponse
                {
                    ErrorCode = 101,
                    ErrorMessage = "Create client error"

                });
            }
            
        }
    }
}
