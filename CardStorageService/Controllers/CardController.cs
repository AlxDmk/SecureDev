using CardStorageService.Controllers.Models.Requests;
using CardStorageService.Controllers.Services;
using CardStorageService.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _repository;
        private readonly ILogger<CardController> _logger;
        private readonly IValidator<CreateCardRequest> _createCardRequestValidator;
        private readonly IMapper _mapper;

        public CardController(ICardRepository repository,
            ILogger<CardController> logger,
            IValidator<CreateCardRequest> createCardRequestValidator,
            IMapper mapper)
            
        {
            _repository = repository;
            _logger = logger;
            _createCardRequestValidator = createCardRequestValidator;
            _mapper = mapper;
        }
        [HttpPost("create")]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Create([FromForm] CreateCardRequest request)
        {
            ValidationResult validationResult = _createCardRequestValidator.Validate(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());
            
            try
            {
               return Ok(new CreateCardResponse
                {
                    CardId = _repository.Create(_mapper.Map<Card>(request)).ToString()
                });               

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create CArd Error!");
                return Ok(new CreateCardResponse
                {
                    ErrorMessage = "Create card Error!",
                    ErrorCode = 913

                });
            }
        }

        [HttpPost("get-by-client-id")]
        [ProducesResponseType(typeof(GetByClientIdCardsResponse), StatusCodes.Status200OK)]
        public IActionResult GetByClientId([FromForm] int clientId)
        {
            try
            {
                return Ok(new GetByClientIdCardsResponse
                {
                    Cards = _mapper.Map<List<CardDto>>(_repository.GetByClientId(clientId))
                });
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Get Cars Error");
                return Ok(new GetByClientIdCardsResponse
                {
                    ErrorCode = 914,
                    ErrorMessage =  "Get Cards by ClientId Error"
                }) ;
            }            
        }
    }   
}
