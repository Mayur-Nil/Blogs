using System;
using System.Collections.Generic;
using System.Text;
using static Blogs.Data.Enums.Enums;

namespace Blogs.Infrastructure.DTOs
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public Status Status { get; set; }
    }
}
