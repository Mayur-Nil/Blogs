using Blogs.Data.ApplicationDbContext;
using Blogs.Data.Entities;
using Blogs.Infrastructure.DTOs;
using Blogs.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Infrastructure.Services
{
    public class ContentService : IContentService
    {
        private readonly BlogsDbContext _context;

        public ContentService(BlogsDbContext context)
        {
            _context = context;
        }

        public async Task<ContentDto?> GetByIdAsync(int id)
        {
            return await _context.Contents
                .Where(c => c.Id == id)
                .Select(c => new ContentDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Content = c.ContentText,
                    Author = c.Author,
                    Status = c.Status,
                    CreatedAt = c.CreatedAt,
                    Language = c.Language,
                    ArticleId = c.ArticleId
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ContentDto>> GetAllAsync()
        {
            return await _context.Contents
                .Select(c => new ContentDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Content = c.ContentText,
                    Author = c.Author,
                    Status = c.Status,
                    CreatedAt = c.CreatedAt,
                    Language = c.Language,
                    ArticleId = c.ArticleId
                })
                .ToListAsync();
        }

        public async Task<ContentDto> CreateAsync(Content content)
        {
            content.CreatedAt = DateTime.UtcNow;

            _context.Contents.Add(content);

            await _context.SaveChangesAsync();

            return await GetByIdAsync(content.Id);
        }

        public async Task<ContentDto?> UpdateAsync(int id, Content content)
        {
            var existingContent = await _context.Contents
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingContent == null)
                return null;

            existingContent.Title = content.Title;
            existingContent.ContentText = content.ContentText;
            existingContent.Author = content.Author;
            existingContent.Status = content.Status;
            existingContent.Language = content.Language;
            existingContent.ArticleId = content.ArticleId;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var content = await _context.Contents
                .FirstOrDefaultAsync(c => c.Id == id);

            if (content == null)
                return false;

            _context.Contents.Remove(content);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
