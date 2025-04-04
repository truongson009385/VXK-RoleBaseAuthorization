using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleBaseAuthorization.Dtos.Account;
using RoleBaseAuthorization.Extensions;
using RoleBaseAuthorization.Interfaces;
using RoleBaseAuthorization.Models;

namespace RoleBaseAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly ITokenService tokenService;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService,
            SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    Email = registerDto.Email,
                    UserName = registerDto.Username,
                };

                var createUser = await userManager.CreateAsync(appUser, registerDto.Password);

                if (createUser.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(appUser, "Admin");

                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUserDto
                        {
                            Email = appUser.Email,
                            Username = appUser.UserName,
                            Token = await tokenService.CreateTokenAsync(appUser),
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Invalid username!");

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Invalid password!");

            return Ok(new NewUserDto
            {
                Email = user.Email,
                Username = user.UserName,
                Token = await tokenService.CreateTokenAsync(user),
            });
        }

        
    }
}

