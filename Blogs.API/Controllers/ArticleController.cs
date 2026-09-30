using Blogs.Data.Entities;
using Blogs.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Blogs.Data.Enums.Enums;

namespace Blogs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var article = await _articleService.GetByIdAsync(id);

            if (article == null)
                return NotFound();

            return Ok(article);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Article article)
        {
            var created = await _articleService.CreateAsync(article);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Article article)
        {
            var updated = await _articleService.UpdateAsync(id, article);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _articleService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var article = await _articleService.GetDetailsAsync(id);

            if (article == null)
                return NotFound();

            return Ok(article);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged(
            int page = 1,
            int pageSize = 10,
            Status? status = null,
            bool sortByTitle = false)
        {
            var result = await _articleService.GetPagedAsync(
                page,
                pageSize,
                status,
                sortByTitle);

            return Ok(result);
        }
    }
}
