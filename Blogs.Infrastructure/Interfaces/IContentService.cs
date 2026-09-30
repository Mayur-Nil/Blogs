using Blogs.Data.Entities;
using Blogs.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Infrastructure.Interfaces
{
    public interface IContentService
    {
        Task<ContentDto?> GetByIdAsync(int id);
        Task<IEnumerable<ContentDto>> GetAllAsync();
        Task<ContentDto> CreateAsync(Content content);
        Task<ContentDto?> UpdateAsync(int id, Content content);
        Task<bool> DeleteAsync(int id);
    }
}
