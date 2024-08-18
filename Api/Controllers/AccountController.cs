using AutoMapper;
using Core.DTOs;
using Core.Models;
using Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public AccountController(IUserRepository userRepository, UserManager<User> userManager, IConfiguration configuration, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                {
                    User user = new User();
                    user = mapper.Map<User>(userDto);


                    //create account in db
                    IdentityResult result = await userManager.CreateAsync(user, userDto.Password);
                    if (result.Succeeded)
                    {
                        return Ok("Account Created");
                    }
                    return BadRequest(result.Errors);
                }
            }
            return BadRequest(ModelState);
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                User userDb = await userManager.FindByNameAsync(userDto.UserName);
                if (userDb != null)
                {
                    bool found = await userManager.CheckPasswordAsync(userDb, userDto.Password);
                    if (found)
                    {
                        List<Claim> myclaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, userDb.UserName),
                            new Claim(ClaimTypes.NameIdentifier, userDb.Id),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var roles = await userManager.GetRolesAsync(userDb);
                        foreach (var role in roles)
                        {
                            myclaims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                        var signingCredentials = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: configuration["Jwt:Issuer"],
                            audience: configuration["Jwt:Audience"],
                            expires: DateTime.Now.AddDays(30),
                            claims: myclaims,
                            signingCredentials: signingCredentials
                        );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(myToken),
                            expired = myToken.ValidTo,
                            customerId = userDb.Id // Assuming userDb.Id is the unique identifier for the user
                        });
                    }
                }
                return Unauthorized("Invalid Account");
            }
            return BadRequest(ModelState);
        }
    }
}
