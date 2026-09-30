using Blogs.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Blogs.Data.ApplicationDbContext
{
    public class BlogsDbContext : DbContext
    {
        public BlogsDbContext(DbContextOptions<BlogsDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Content> Contents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Content>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.Author)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Content>()
                .HasOne<Article>()
                .WithMany()
                .HasForeignKey(x => x.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Content>()
                .Property(x => x.ContentText)
                .HasColumnName("Content");
        }
    }
}
