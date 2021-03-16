using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class SortingHelper
    {
        public static KeyValuePair<string,string>[] GetSortField()
        {
            return new[] { SortField.Title, SortField.CreationDate };
        }
    }

    public class SortField
    {
        public static KeyValuePair<string, string> Title { get; } = new KeyValuePair<string, string>("title", "Title");
        public static KeyValuePair<string, string> CreationDate { get; } = new KeyValuePair<string, string>("creationdate", "Created");
    }
}
