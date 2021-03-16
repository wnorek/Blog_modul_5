using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Model;
using WebAPI.Wrappers;

namespace WebAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public IdentityController(UserManager<ApplicationUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            var userExist = await _userManager.FindByNameAsync(register.UserName);

            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
                {
                    Succeeded = false,
                    Message = "User already exist!"
                });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.UserName
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
                {
                    Succeeded = false,
                    Message = "User creation failed! Please check user details and try again.",
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            await _userManager.AddToRoleAsync(user, UserRoles.User);

            return Ok(new Response<bool> { Succeeded = true, Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id) ,
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var userRoles = await _userManager.GetRolesAsync(user);

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }


                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return Unauthorized();



        }

        [HttpPost]
        [Route("RegisterAdmmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterModel register)
        {
            var userExist = await _userManager.FindByNameAsync(register.UserName);

            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
                {
                    Succeeded = false,
                    Message = "User already exist!"
                });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.UserName
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
                {
                    Succeeded = false,
                    Message = "User creation failed! Please check user details and try again.",
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            return Ok(new Response<bool> { Succeeded = true, Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("RegisterSuperUser")]
        public async Task<IActionResult> RegisterSuperUser(RegisterModel register)
        {
            var userExist = await _userManager.FindByNameAsync(register.UserName);

            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
                {
                    Succeeded = false,
                    Message = "User already exist!"
                });
            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.UserName
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response<bool>
                {
                    Succeeded = false,
                    Message = "User creation failed! Please check user details and try again.",
                    Errors = result.Errors.Select(x => x.Description)
                });
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.SuperUser))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperUser));
            }

            await _userManager.AddToRoleAsync(user, UserRoles.SuperUser);

            return Ok(new Response<bool> { Succeeded = true, Message = "User created successfully!" });
        }

    }
}
