using Blog.Models;

namespace Blog.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post?> GetPostByIdAsync(int id);
        Task AddPostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(int id);
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task AddCommentAsync(Comment comment);
        Task<User?> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task<bool> PostExistAsync(int id);

        string GetImagePath(IFormFile? imageFile);
    }
}
