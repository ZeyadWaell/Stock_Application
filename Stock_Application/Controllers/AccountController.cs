using Api.Dto;
using Api.ResponseModule;
using AutoMapper;
using Core.Entites.Identity;
using Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace Api.Controllers
{

    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _usermanger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> usermanger, SignInManager<AppUser> signInManager, ITokenServices tokenServices, IMapper mapper)
        {
            _usermanger = usermanger;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet(Name = "GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _usermanger.FindByEmailAsync(email);
            if (user is null)
                return NotFound(new ApiResponse(404));

            return new UserDto
            {
                Email = email,
                DisplayName = user.DisplayName,
                Token = _tokenServices.CreateToken(user),
            };

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _usermanger.FindByEmailAsync(login.Email);

            if (user is null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenServices.CreateToken(user),

            };
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var email = await _usermanger.FindByEmailAsync(registerDto.Email);
            if (email != null)
                return BadRequest();
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };
            var result = await _usermanger.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest();

            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
               // Token = _tokenServices.CreateToken(user),

            };
        }
        [HttpGet("emailexist")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync([FromQuery] string email)
            => await _usermanger.FindByEmailAsync(email) !=null;

        [Authorize]
        [HttpGet("GetCurrentUserAddress")]
        public async Task<ActionResult<AddressDto>> GetCurrrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _usermanger.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
            var mappedAddress =  _mapper.Map<AddressDto>(user.Address);

            return Ok(_mapper.Map<AddressDto>(user.Address));
        }
    }
}
