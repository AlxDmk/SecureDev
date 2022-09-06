using CardStorageService.Controllers.Services;
using CardStorageService.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using AutoMapper;
using CardStorageService.Controllers.Models.Validations;
using FluentValidation;
using FluentValidation.Results;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _repository;
        private readonly ILogger<ClientController> _logger;
        private readonly IValidator<CreateClientRequest> _createClientRequestValidator;
        private readonly IMapper _mapper;

        public ClientController(IClientRepository repository,
            ILogger<ClientController> logger,
            IValidator<CreateClientRequest> createClientRequestValidator,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _createClientRequestValidator = createClientRequestValidator;
            _mapper = mapper;
        }

        [HttpPost("create")]    
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateClientRequest request)
        {
            ValidationResult validationResult = _createClientRequestValidator.Validate(request,
                    opt => opt.IncludeRuleSets("Names"));
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());
            
            try
            {
                return Ok(new CreateClientResponse
                {
                    ClientId = _repository.Create(_mapper.Map<Client>(request))
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
