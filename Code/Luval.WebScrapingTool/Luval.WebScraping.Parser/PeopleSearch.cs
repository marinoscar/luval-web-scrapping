using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Luval.WebScraping.Parser
{
    public class PeopleSearch
    {
        //https://publicsearch.people.virginia.edu/search?combine=4342432839

        public async Task<IEnumerable<Entry>> GetEntriesAsync(string keyword)
        {
            var url = string.Format("https://publicsearch.people.virginia.edu/search?combine={0}", HttpUtility.UrlEncode(keyword));
            return (await (new WebParser()).RunSelectorAsync(url, i => i.HasClass("person") && i.HasClass("search-result")))
                .Select(i => MapItem(i)).Where(i => i != null);
        }

        private Entry MapItem(HtmlNode htmlNode)
        {
            if (htmlNode == null) return null;
            var name = GetName(htmlNode);
            var code = GetCode(name);
            return new Entry()
            {
                Code = code.Replace("(", "").Replace(")", ""),
                Name = name.Replace(code, "").Trim(),
                Email = GetEmail(htmlNode),
                Title = GetDepartment(htmlNode),
                Phone = GetPhone(htmlNode)
            };
        }


        private string GetCode(string name)
        {
            var match = Regex.Match(name, "\\(.*\\)");
            return match.Success ? match.Value : string.Empty;
        }


        private HtmlNode GetNodeByClass(HtmlNode node, string clsName) => node.Descendants().FirstOrDefault(i => i.HasClass(clsName));

        private string GetValue(HtmlNode node)
        {
            if (node == null) return string.Empty;
            if (node.Attributes["value"] != null) return node.Attributes["value"].Value;
            if (!string.IsNullOrWhiteSpace(node.InnerText)) return node.InnerText.Trim();
            return string.Empty;
        }

        private string GetValueByClass(HtmlNode node, string clsName)
        {
            var item = GetNodeByClass(node, clsName);
            return GetValue(item);
        }

        private string GetName(HtmlNode node)
        {
            var item = node.Descendants().Where(i => i.Name == "a").FirstOrDefault();
            return GetValue(item);
        }

        private string GetEmail(HtmlNode node)
        {
            var item = GetNodeByClass(node, "email").SelectSingleNode("a");
            return GetValue(item);
        }

        private string GetDepartment(HtmlNode node)
        {
            return GetValueByClass(node, "department");
        }

        private string GetPhone(HtmlNode node)
        {
            return GetValueByClass(node, "phone");
        }
    }
}
