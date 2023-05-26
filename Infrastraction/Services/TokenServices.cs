using Core.Entites.Identity;
using Core.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.Data
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;

        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName,user.DisplayName)
            };
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var TOKENDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cred,
                Issuer = _configuration["Token:Issuer"],
                IssuedAt = DateTime.Now
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(TOKENDescription);
          
            return tokenhandler.WriteToken(token);

        }
    }
}
