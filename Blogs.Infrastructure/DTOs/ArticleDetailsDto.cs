using System;
using System.Collections.Generic;
using System.Text;
using static Blogs.Data.Enums.Enums;

namespace Blogs.Infrastructure.DTOs
{
    public class ArticleDetailsDto
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ContentDto> Contents { get; set; } = new();
    }
}
