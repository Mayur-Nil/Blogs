using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using static Blogs.Data.Enums.Enums;

namespace Blogs.Data.Entities
{
    public class Content
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("content")]
        public string ContentText { get; set; } = string.Empty;

        public int Author { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public Language Language { get; set; }

        public int ArticleId { get; set; }
    }
}
