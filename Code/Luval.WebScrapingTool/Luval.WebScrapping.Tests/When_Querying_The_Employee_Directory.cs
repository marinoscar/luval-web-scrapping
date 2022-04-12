using Luval.WebScraping.Parser;
using System.Linq;
using Xunit;

namespace Luval.WebScrapping.Tests
{
    public class When_Querying_The_Employee_Directory
    {
        [Fact]
        public void It_Should_Get_A_Valid_Entry_For_A_Code_Search()
        {
            var code = "jmw8zs";
            var search = new PeopleSearch();
            var entries = search.GetEntriesAsync(code).Result;
            Assert.NotNull(entries);
            Assert.True(entries.Any());
            Assert.True(entries.Count() == 1);
            Assert.True(entries.First().Code == code);
        }

        [Fact]
        public void It_Should_Get_A_Valid_Entries_For_A_Name_Search()
        {
            var code = "oscar";
            var search = new PeopleSearch();
            var entries = search.GetEntriesAsync(code).Result;
            Assert.NotNull(entries);
            Assert.True(entries.Any());
            Assert.True(entries.Count() > 1);
        }
    }
}