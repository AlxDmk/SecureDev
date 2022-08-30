using CardStorageService.Controllers.Models;
using CardStorageService.Controllers.Models.Requests;
using CardStorageService.Controllers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace CardStorageService.Controllers
{
    [Authorize]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        public IActionResult Login([FromBody]AuthenticationRequest authenticateRequest)
        {
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
