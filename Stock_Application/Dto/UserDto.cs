using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Api.Dto
{
    public class UserDto
    {
        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string Token { get; set; }   
    
    }

  
}
