using CardStorageService.Controllers.Models.Requests;
using CardStorageService.Controllers.Services;
using CardStorageService.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _repository;
        private readonly ILogger<CardController> _logger;

        public CardController(ICardRepository repository, ILogger<CardController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Create([FromForm] CreateCardRequest request)
        {
            try
            {
                var cardId = _repository.Create(new Card
                {
                    ClientId = request.ClientId,
                    CardNo = request.CardNo,
                    Name = request.Name,
                    ExpDate = request.ExpDate,
                    CVV2 =  request.CVV2
                });

                return Ok(new CreateCardResponse
                {
                    CardId = cardId.ToString()
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
                var cards = _repository.GetByClientId(clientId);
                return Ok(new GetByClientIdCardsResponse
                {
                    Cards = cards.Select(card => new CardDto
                    {
                        CardId = card.CardId.ToString(),
                        CardNo = card.CardNo,
                        CVV2 = card.CVV2,
                        Name = card.Name,
                        ExpDate = card.ExpDate.ToString("MM/yy")
                    }).ToList()
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
