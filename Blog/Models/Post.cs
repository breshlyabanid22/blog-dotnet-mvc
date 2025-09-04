using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class Post
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Title { get; set; }

        [MaxLength(200)]
        public string? Content { get; set; }

        public string Author { get; set; } = "Anonymous User";

        public DateTime PublishedDate { get; set; } = DateTime.Now;

        public ICollection<Comment>? Comments { get; set; }

        public bool IsPublished { get; set; }
    }
}
