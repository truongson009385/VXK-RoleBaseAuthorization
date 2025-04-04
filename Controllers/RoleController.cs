using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleBaseAuthorization.Extensions;
using RoleBaseAuthorization.Models;

namespace RoleBaseAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(
        RoleManager<IdentityRole> roleManager,
        UserManager<AppUser> userManager
    ) : ControllerBase
    {
        [HttpGet("getUserRoles")]
        [Authorize]
        public async Task<IActionResult> GetUserRoles()
        {
            var appUser = await userManager.FindByNameAsync(User.GetUsername());
            var userRoles = await userManager.GetRolesAsync(appUser);

            return Ok(userRoles);
        }

        [HttpGet("getAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var userRoles = await roleManager.Roles.ToListAsync();

            return Ok(userRoles);
        }

        [HttpGet("getRoleByName/{nameRole}")]
        public async Task<IActionResult> GetRoleByName(string nameRole)
        {
            var role = await roleManager.FindByNameAsync(nameRole);
            if (role == null)
                return NotFound(nameRole + " not found!");
            return Ok(role);
        }

        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole([FromBody] string nameRole)
        {
            if (await roleManager.RoleExistsAsync(nameRole))
                return BadRequest(nameRole + " already exists!");

            var newRole = await roleManager.CreateAsync(new IdentityRole(nameRole));

            if (!newRole.Succeeded)
                return BadRequest(newRole.Errors);

            return Ok();
        }

        [HttpDelete("deleteRole")]
        public async Task<IActionResult> DeleteRole([FromBody] string nameRole)
        {
            var role = await roleManager.FindByNameAsync(nameRole);
            if (role == null)
                return NotFound(nameRole + " not found!");
            var result = await roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return NoContent();
        }

        [HttpPost("addRoleToUser")]
        [Authorize]
        public async Task<IActionResult> addRoleToUser([FromBody] string nameRole)
        {
            var role = await roleManager.FindByNameAsync(nameRole);

            if (role == null)
                return NotFound(nameRole + " not found!");

            var appUser = await userManager.FindByNameAsync(User.GetUsername());
            var result = await userManager.AddToRoleAsync(appUser, role.Name);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpGet("testAdmin")]
        [Authorize(Roles = "Admin")]
        public IActionResult TestAdmin()
        {
            return Ok("Bạn ở role Admin");
        }

        [HttpGet("testUser")]
        [Authorize(Roles = "User")]
        public IActionResult TestUser()
        {
            return Ok("Bạn ở role User");
        }
    }
}
