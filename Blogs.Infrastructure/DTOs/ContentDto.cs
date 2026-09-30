using System;
using System.Collections.Generic;
using System.Text;
using static Blogs.Data.Enums.Enums;

namespace Blogs.Infrastructure.DTOs
{
    public class ContentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int Author { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Language Language { get; set; }
        public int ArticleId { get; set; }
    }
}
