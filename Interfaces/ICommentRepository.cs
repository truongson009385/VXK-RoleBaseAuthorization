using RoleBaseAuthorization.Models;

namespace RoleBaseAuthorization.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment comment);
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
    }
}
