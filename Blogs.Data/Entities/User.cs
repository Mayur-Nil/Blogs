using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Data.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
