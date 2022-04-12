using Luval.WebScraping.Parser;
using Microsoft.AspNetCore.Mvc;

namespace Luval.WebScrapingTool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebQueryController : ControllerBase
    {

        private readonly ILogger<WebQueryController> _logger;

        public WebQueryController(ILogger<WebQueryController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetEmployeeData")]
        public async Task<IEnumerable<Entry>> Get(string keyword)
        {
            var search = new PeopleSearch();
            var entries = await search.GetEntriesAsync(keyword);
            return entries;
        }

    }
}
