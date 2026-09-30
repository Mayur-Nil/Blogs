using Blogs.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Blogs.Data.Enums.Enums;

namespace Blogs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("authors")]
        public async Task<IActionResult> GetAuthors()
        {
            var result = await _reportService.GetAuthorsWithArticlesAsync();

            return Ok(result);
        }

        [HttpGet("articles")]
        public async Task<IActionResult> GetArticles(
            [FromQuery] Language language)
        {
            var result = await _reportService.GetRecentArticlesAsync(language);

            return Ok(result);
        }
    }
}
