using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Luval.WebScraping.Parser
{
    /// <summary>
    /// Provides the functionality to parse an HTML page
    /// </summary>
    public class WebParser
    {
        /// <summary>
        /// Parses a web page into HTML nodes
        /// </summary>
        /// <param name="url">The url to navidate</param>
        /// <returns>The <see cref="HtmlNode"/></returns>
        public async Task<HtmlNode> ParseAsync(string url)
        {
            var webPage = new HtmlWeb();
            var doc = await webPage.LoadFromWebAsync(url);
            return doc.DocumentNode;
        }

        /// <summary>
        /// Parses the elements on an xPath expression
        /// </summary>
        /// <param name="url">The url to navidate</param>
        /// <param name="xPath">The xPath expression to use</param>
        /// <returns>A <see cref="IEnumerable{HtmlNode}"/> with the results</returns>
        public async Task<IEnumerable<HtmlNode>> RunSelectorAsync(string url, string xPath)
        {
            var doc = await ParseAsync(url);
            return doc.SelectNodes(xPath);
        }

        /// <summary>
        /// Parses the elements on an xPath expression
        /// </summary>
        /// <param name="url">The url to navidate</param>
        /// <param name="filter"></param>
        /// <returns>A <see cref="IEnumerable{HtmlNode}"/> with the results</returns>
        public async Task<IEnumerable<HtmlNode>> RunSelectorAsync(string url, Expression<Func<HtmlNode, bool>> filter)
        {
            var doc = await ParseAsync(url);
            return doc.Descendants()
                .Where(filter.Compile());
        }


    }
}
