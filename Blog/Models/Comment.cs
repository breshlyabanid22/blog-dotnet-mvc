using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; } = null!;
        public required string Author { get; set; }
        public string? Content { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.Now;
    }
}
