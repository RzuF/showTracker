using System.Collections.Generic;

namespace showTracker.Model.View
{
    public class GroupedResult<T>
    {
        public string GroupName { get; set; }
        public IEnumerable<T> Results { get; set; }

        public static string GroupNameString => nameof(GroupName);
        public static string ResultsString => nameof(Results);
    }
}
