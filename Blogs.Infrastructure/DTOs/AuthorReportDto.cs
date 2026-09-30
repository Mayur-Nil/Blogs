using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Infrastructure.DTOs
{
    public class AuthorReportDto
    {
        public int AuthorId { get; set; }
        public string Author { get; set; } = string.Empty;
        public List<int> ArticleIds { get; set; } = new();
    }
}
