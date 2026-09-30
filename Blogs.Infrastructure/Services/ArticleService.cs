using Blogs.Data.ApplicationDbContext;
using Blogs.Data.Entities;
using Blogs.Infrastructure.DTOs;
using Blogs.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Blogs.Data.Enums.Enums;

namespace Blogs.Infrastructure.Services
{
    public class ArticleService : IArticleService
    {
        private readonly BlogsDbContext _context;

        public ArticleService(BlogsDbContext context)
        {
            _context = context;
        }

        public async Task<ArticleDto?> GetByIdAsync(int id)
        {
            return await _context.Articles
                .Where(a => a.Id == id)
                .Select(a => new ArticleDto
                {
                    Id = a.Id,
                    Status = a.Status
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Article> CreateAsync(Article article)
        {
            article.CreatedAt = DateTime.UtcNow;

            _context.Articles.Add(article);

            await _context.SaveChangesAsync();

            return article;
        }

        public async Task<Article?> UpdateAsync(int id, Article article)
        {
            var existingArticle = await _context.Articles
                .FirstOrDefaultAsync(a => a.Id == id);

            if (existingArticle == null)
                return null;

            existingArticle.Status = article.Status;

            await _context.SaveChangesAsync();

            return existingArticle;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var article = await _context.Articles
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
                return false;

            _context.Articles.Remove(article);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ArticleDetailsDto?> GetDetailsAsync(int id)
        {
            var article = await _context.Articles
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
                return null;

            var contents = await _context.Contents
                .Where(c => c.ArticleId == id)
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

            return new ArticleDetailsDto
            {
                Id = article.Id,
                Status = article.Status,
                CreatedAt = article.CreatedAt,
                Contents = contents
            };
        }

        public async Task<PagedResultDto<ArticleDto>> GetPagedAsync(
            int page,
            int pageSize,
            Status? status,
            bool sortByTitle)
        {
            var query = _context.Articles.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(a => a.Status == status.Value);
            }

            var totalCount = await query.CountAsync();

            var articles = await query
                .Select(article => new
                {
                    Article = article,

                    EnglishContent = _context.Contents
                        .Where(c =>
                            c.ArticleId == article.Id &&
                            c.Language == Language.English)
                        .OrderBy(c => c.Id)
                        .Select(c => new
                        {
                            c.Title,
                            c.Author
                        })
                        .FirstOrDefault(),

                    FallbackContent = _context.Contents
                        .Where(c => c.ArticleId == article.Id)
                        .OrderBy(c => c.Id)
                        .Select(c => new
                        {
                            c.Title,
                            c.Author
                        })
                        .FirstOrDefault()
                })
                .ToListAsync();

            var sorted = sortByTitle
                ? articles.OrderBy(x =>
                    x.EnglishContent != null
                        ? x.EnglishContent.Title
                        : x.FallbackContent != null
                            ? x.FallbackContent.Title
                            : string.Empty)
                : articles.OrderByDescending(x => x.Article.CreatedAt);

            var items = sorted
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x =>
                {
                    var content = x.EnglishContent ?? x.FallbackContent;

                    return new ArticleDto
                    {
                        Id = x.Article.Id,
                        Title = content?.Title,
                        Author = content?.Author.ToString(),
                        Status = x.Article.Status
                    };
                })
                .ToList();

            return new PagedResultDto<ArticleDto>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(
                    totalCount / (double)pageSize)
            };
        }
    }
}
