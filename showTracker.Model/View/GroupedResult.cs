using System.Collections.ObjectModel;

namespace showTracker.Model.View
{
    public class GroupedResult<T>
    {
        public string GroupName { get; set; }
        public ObservableCollection<T> Results { get; set; }

        public static string GroupNameString => nameof(GroupName);
        public static string ResultsString => nameof(Results);
    }
}
