using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
           var imagePath = _blogService.GetImagePath(post.ImageFile);

            try
            {
                await _blogService.AddPostAsync(new Models.Post
                {
                    Title = post.Title,
                    Subtitle = post.Subtitle,
                    Content = post.Content,
                    ImagePath = imagePath, 
                    Author = post.Author,
                    IsPublished = true
                });
                TempData["SuccessMessage"] = "Post created successfully.";
                return RedirectToAction("Index", "Home");
            }catch(Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the post.";
                ModelState.AddModelError(string.Empty, "An error occurred while uploading the image: " + ex.Message);
                return View("CreateForm", post);
            }

        }

        public async Task<IActionResult> Details(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            var viewModel = new PostViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Subtitle = post.Subtitle,
                Content = post.Content,
                ImagePath = post.ImagePath,
                Author = post.Author,
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
                var post = await _blogService.GetPostByIdAsync(id);
                if(post== null)
                {
                    return NotFound();
                }
                var viewModel = new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Subtitle = post.Subtitle,
                    Content = post.Content,
                    ImagePath = post.ImagePath,
                    Author = post.Author,
                };
                return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostViewModel post)
        {
            var existingPost = await _blogService.GetPostByIdAsync(post.Id);
            if(existingPost == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    existingPost.Title = post.Title;
                    existingPost.Subtitle = post.Subtitle;
                    existingPost.Content = post.Content;
                    existingPost.ImagePath = _blogService.GetImagePath(post.ImageFile) ?? existingPost.ImagePath;

                    await _blogService.UpdatePostAsync(existingPost);
                    TempData["SuccessMessage"] = "Post updated successfully.";
                    return RedirectToAction("Details", new { id = post.Id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _blogService.PostExistAsync(post.Id))
                    {
                        return NotFound();
                    }else
                    {
                        throw;
                    }
                }
            }
            return View(post);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _blogService.GetPostByIdAsync(id);
            if(post == null)
            {
                return NotFound();
            }
            await _blogService.DeletePostAsync(id);
            TempData["SuccessMessage"] = "Post deleted successfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}
