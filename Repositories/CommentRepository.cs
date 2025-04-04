using Microsoft.EntityFrameworkCore;
using RoleBaseAuthorization.Data;
using RoleBaseAuthorization.Interfaces;
using RoleBaseAuthorization.Models;

namespace RoleBaseAuthorization.Repositories
{
    public class CommentRepository(
        ApplicationDbContext context
    ) : ICommentRepository
    {
        public async Task<Comment> CreateAsync(Comment comment)
        {
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var comments = await context.Comments.Include(x => x.AppUser).ToListAsync();
            return comments;
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await context.Comments
                .Include(x => x.AppUser)
                .FirstOrDefaultAsync(x => x.Id == id);

            return comment;
        }
    }
}
