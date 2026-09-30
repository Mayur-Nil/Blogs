using Blogs.Data.ApplicationDbContext;
using Blogs.Infrastructure.DTOs;
using Blogs.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Blogs.Data.Enums.Enums;

namespace Blogs.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly BlogsDbContext _context;

        public ReportService(BlogsDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorReportDto>> GetAuthorsWithArticlesAsync()
        {
            var users = await _context.Users
                .ToListAsync();

            var contents = await _context.Contents
                .ToListAsync();

            var data = users
                .Select(u => new AuthorReportDto
                {
                    AuthorId = u.Id,
                    Author = u.Username,
                    ArticleIds = contents
                        .Where(c => c.Author == u.Id)
                        .Select(c => c.ArticleId)
                        .Distinct()
                        .ToList()
                })
                .ToList();

            return data;
        }

        public async Task<List<ArticleReportDto>> GetRecentArticlesAsync(
            Language language)
        {
            var threeMonthsAgo = DateTime.UtcNow.AddMonths(-3);
            var fourMonthsAgo = DateTime.UtcNow.AddMonths(-4);

            return await (
                from article in _context.Articles
                join content in _context.Contents
                    on article.Id equals content.ArticleId
                join user in _context.Users
                    on content.Author equals user.Id
                where article.CreatedAt >= threeMonthsAgo
                      && user.CreatedAt >= fourMonthsAgo
                      && content.Language == language
                select new ArticleReportDto
                {
                    ArticleId = article.Id,
                    ArticleCreatedAt = article.CreatedAt,
                    AuthorId = user.Id,
                    Author = user.Username,
                    Language = content.Language
                }
            )
            .Distinct()
            .ToListAsync();
        }
    }
}
