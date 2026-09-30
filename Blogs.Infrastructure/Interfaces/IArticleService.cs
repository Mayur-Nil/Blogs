using Blogs.Data.Entities;
using Blogs.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using static Blogs.Data.Enums.Enums;

namespace Blogs.Infrastructure.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleDto?> GetByIdAsync(int id);

        Task<Article> CreateAsync(Article article);

        Task<Article?> UpdateAsync(int id, Article article);

        Task<bool> DeleteAsync(int id);

        Task<ArticleDetailsDto?> GetDetailsAsync(int id);

        Task<PagedResultDto<ArticleDto>> GetPagedAsync(int page, int pageSize, Status? status, bool sortByTitle);
    }
}
