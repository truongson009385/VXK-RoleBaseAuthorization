using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoleBaseAuthorization.Dtos.Comment;
using RoleBaseAuthorization.Extensions;
using RoleBaseAuthorization.Interfaces;
using RoleBaseAuthorization.Mappers;
using RoleBaseAuthorization.Models;

namespace RoleBaseAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController(
        ICommentRepository commentRepo,
        UserManager<AppUser> userManager
    ) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByNameAsync(User.GetUsername());
            var commentModel = createDto.ToCommentFromCreateDto();

            commentModel.AppUserId = user.Id;
            await commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await commentRepo.GetAllAsync();
            var commentDtos = comments.Select(c => c.ToCommentDto()).ToList();

            return Ok(commentDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await commentRepo.GetByIdAsync(id);

            if (comment == null)
                return NotFound("Comment not found");

            return Ok(comment.ToCommentDto());
        }
    }
}
