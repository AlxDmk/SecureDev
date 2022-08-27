using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtSample
{
    internal class UserService
    {
        private IDictionary<string, string> _users = new Dictionary<string, string>()
        {
            {"user", "test" },
            {"user1", "test" },
            {"user2", "test" }
        };

        private const string SecretKey = "My very secret key";

        public string Authenticate(string user, string password)
        {
            if(string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }

            int i = 0;
            foreach(KeyValuePair<string, string> pair in _users)
            {                
                i++;
                if(string.CompareOrdinal(pair.Key, user) == 0 && string.CompareOrdinal(pair.Value, password) == 0)
                {
                    return GenerateJwtToken(i);
                }                
            }
            return String.Empty;
        }

        private string GenerateJwtToken(int id)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, id.ToString())
                    }),            
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(securityToken);


        }

    }
}
