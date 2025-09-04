using Blog.Models;
using System.ComponentModel.DataAnnotations;

namespace Blog.DTO
{
    public class PostDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        public string? Subtitle { get; set; }
        public string? Content { get; set; }

        public string? ImagePath { get; set; }

        public string Author { get; set; } = "Anonymous User";

        public DateTime PublishedDate { get; set; } = DateTime.Now;

        public bool IsPublished { get; set; }
    }
}
