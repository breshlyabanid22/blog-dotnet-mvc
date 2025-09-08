using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Blog.Services
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _context;

        public BlogService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task AddCommentAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task AddPostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public Task AddUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeletePostAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }

        public Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return null;
            }
            return post;
        }

        public Task<User?> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostAsync(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
