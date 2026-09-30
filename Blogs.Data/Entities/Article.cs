using System;
using System.Collections.Generic;
using System.Text;
using static Blogs.Data.Enums.Enums;

namespace Blogs.Data.Entities
{
    public class Article
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
