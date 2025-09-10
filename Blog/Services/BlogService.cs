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

        public async Task DeletePostAsync(int id)
        {
            await _context.Posts.Where(p => p.Id == id).ExecuteDeleteAsync();
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

        public async Task UpdatePostAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> PostExistAsync(int id)
        {
            return await _context.Posts.AnyAsync(e => e.Id == id);
        }

        public  string GetImagePath(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
                return "/images/" + uniqueFileName;
            }
            return string.Empty;
        }

    }
}
