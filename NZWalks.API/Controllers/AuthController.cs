using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO requestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = requestDTO.Username,
                Email = requestDTO.Username
            };

           var identityResult = await userManager.CreateAsync(identityUser,requestDTO.Password);
            if(identityResult.Succeeded)
            {
                if(requestDTO.Roles !=null && requestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, requestDTO.Roles);
                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }
            return BadRequest("Something went wrong");

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO requestDTO)
        {
            var user =await userManager.FindByEmailAsync(requestDTO.Username);
            if(user != null)
            {
                var isValid = await userManager.CheckPasswordAsync(user, requestDTO.Password);

                if(isValid)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                      var jwtToken = tokenRepository.CreateJWTToken(user,roles.ToList());
                      var reponse = new LoginResponseDTO
                      {
                          JwtToken = jwtToken
                      };
                      return Ok(reponse);
                    }
                }

            }
            return BadRequest("Invalid login attempt");
        }

    }
}
