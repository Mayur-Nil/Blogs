using System;
using System.Collections.Generic;
using System.Text;
using static Blogs.Data.Enums.Enums;

namespace Blogs.Infrastructure.DTOs
{
    public class ArticleReportDto
    {
        public int ArticleId { get; set; }
        public DateTime ArticleCreatedAt { get; set; }
        public int AuthorId { get; set; }
        public string Author { get; set; } = string.Empty;
        public Language Language { get; set; }
    }
}
