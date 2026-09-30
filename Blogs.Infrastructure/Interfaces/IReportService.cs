using Blogs.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using static Blogs.Data.Enums.Enums;

namespace Blogs.Infrastructure.Interfaces
{
    public interface IReportService
    {
        Task<List<AuthorReportDto>> GetAuthorsWithArticlesAsync();

        Task<List<ArticleReportDto>> GetRecentArticlesAsync(
            Language language);
    }
}
