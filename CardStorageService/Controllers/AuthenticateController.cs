using System.Collections.Generic;
using CardStorageService.Controllers.Models;
using CardStorageService.Controllers.Models.Requests;
using CardStorageService.Controllers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using FluentValidation;
using FluentValidation.Results;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IValidator<AuthenticationRequest> _authenticationRequestValidator;

        public AuthenticateController(IAuthenticateService authenticateService,
            IValidator<AuthenticationRequest> authenticationRequestValidator)
        {
            _authenticateService = authenticateService;
            _authenticationRequestValidator = authenticationRequestValidator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        public IActionResult Login([FromBody]AuthenticationRequest authenticateRequest)
        {
            ValidationResult validationResult = _authenticationRequestValidator.Validate(authenticateRequest);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());
            
            AuthenticationResponse authenticationResponse = _authenticateService.Login(authenticateRequest);
            if(authenticationResponse.Status == Models.AuthenticationStatus.Success)
            {
                Response.Headers.Add("X-Session-Token", authenticationResponse.SessionInfo.SessionToken);
            }

            return Ok(authenticationResponse);
         
        }

        [HttpGet("session")]
        [ProducesResponseType(typeof(SessionInfo), StatusCodes.Status200OK)]
        public IActionResult GetSessionInfo()
        {
            var authorization = Request.Headers.Authorization;

            if(AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var sessionToken = headerValue.Parameter;
                if (string.IsNullOrEmpty(sessionToken))
                    return Unauthorized();
                
                SessionInfo sessionInfo = _authenticateService.GetSessionInfo(sessionToken);

                if(sessionInfo == null)
                    return Unauthorized();

                return Ok(sessionInfo);
            
            }
            return Unauthorized();
            
        }

    }
}
