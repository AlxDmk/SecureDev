using CardStorageService.Controllers.Models;
using CardStorageService.Controllers.Models.Requests;

namespace CardStorageService.Controllers.Services
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);

        public SessionInfo GetSessionInfo(string sessionToken);
        
    }
}
