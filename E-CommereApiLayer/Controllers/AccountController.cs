using AutoMapper;
using DomainLayer.Entities.Identity;
using DomainLayer.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceisLayer;
using System.Security.Claims;
using Talabat.API.Dtos;
using Talabat.API.Errors;
using Talabat.API.Extentions;

namespace Talabat.API.Controllers
{

    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAuthService authService,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("{login}")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user =await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result =await _signInManager.CheckPasswordSignInAsync(user, model.Password,false);
            if (result.Succeeded is false)
                return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto
            {
                Name = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreatTokenAsync(user, _userManager)
            });
        }


        [HttpPost("SignUp")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExists(model.email).Result.Value)
                return BadRequest(new ApiValidationResponse() { Errors = new string[] {"this email is already in user "}});
            var user = new AppUser
            {
                DisplayName = model.dispalyName,
                PhoneNumber = model.phoneNumber,
                Email = model.email,
                UserName = model.email.Split("@")[0],
            };
            var result=await _userManager.CreateAsync(user, model.password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto
            {
                Email= user.Email,
                Name= user.DisplayName,
                Token= await _authService.CreatTokenAsync(user, _userManager),
            });
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>>GetCurrentUser()
        {
            var Email =User.FindFirstValue(ClaimTypes.Email);
            var user =await _userManager.FindByEmailAsync(Email);
            return Ok(new UserDto
            {
                Name = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreatTokenAsync(user, _userManager),
            });
        }
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user =await _userManager.FindAddressByEmailAsync(User);
            var addressResult=_mapper.Map<AddressDto>(user.Address);
            return Ok(addressResult);    
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            var addressResult = _mapper.Map<Address>(addressDto);
            var user = await _userManager.FindAddressByEmailAsync(User);
            // to avoid delete and insert  becouse of the addressDto  don't have Id 
            addressResult.Id=user.Address.Id;
            user.Address = addressResult;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return   BadRequest(new ApiResponse(400));
            return Ok(addressDto);
        }
       
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

    }
}
