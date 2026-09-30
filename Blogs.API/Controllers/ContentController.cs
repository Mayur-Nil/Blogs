using Blogs.Data.Entities;
using Blogs.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentsController : ControllerBase
    {
        private readonly IContentService _contentService;

        public ContentsController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var content = await _contentService.GetByIdAsync(id);

            if (content == null)
                return NotFound();

            return Ok(content);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contents = await _contentService.GetAllAsync();

            return Ok(contents);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Content content)
        {
            var created = await _contentService.CreateAsync(content);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Content content)
        {
            var updated = await _contentService.UpdateAsync(id, content);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _contentService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
