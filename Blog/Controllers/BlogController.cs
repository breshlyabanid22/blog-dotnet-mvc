using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateForm()
        {
            return View();
        }

        public async Task<IActionResult> Create(PostViewModel post)
        {
            string? imagePath = null;

            if(post.ImageFile != null && post.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(post.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await post.ImageFile.CopyToAsync(fileStream);
                }
                imagePath = "/images/" + uniqueFileName;

            }
            await _blogService.AddPostAsync(new Models.Post
            {
                Title = post.Title,
                Subtitle = post.Subtitle,
                Content = post.Content,
                ImagePath = imagePath, 
                Author = post.Author,
                IsPublished = post.IsPublished 
            });

            return RedirectToAction("Index", "Home");
        }
    }
}
