namespace Blog.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Content { get; set; }
        public IFormFile? ImageFile { get; set; }

        public string? ImagePath { get; set; }
        public string? Author { get; set; }
        public bool IsPublished { get; set; } = false;

        public DateTime PublishedDate { get; set; } = DateTime.Now;
    }
}
