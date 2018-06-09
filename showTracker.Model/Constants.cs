namespace showTracker.Model
{
    public static class Constants
    {
        public const string ApplicationName = "showTracker";
        public static readonly int[] Version =
        {
            1,
            0,
            0
        };

        public const string ApiUrl = "http://api.tvmaze.com/";

        public const double DefaultShowContainerItemHeight = 100;
        public const double DefaultEpisodeContainerItemHeight = 100;

        public const int MaxRating = 10;

        public const string FavouriteIconResourceId = "showTracker.ViewModel.Resources.Heart.svg";
        public const string SearchIconResourceId = "showTracker.ViewModel.Resources.Search.svg";
        public const string AboutIconResourceId = "showTracker.ViewModel.Resources.About.svg";
        public const string TodayIconResourceId = "showTracker.ViewModel.Resources.Today.svg";
        public const string OkIconResourceId = "showTracker.ViewModel.Resources.Ok.svg";
        public const string FilterIconResourceId = "showTracker.ViewModel.Resources.Filter.svg";
        public const string FilterColoredIconResourceId = "showTracker.ViewModel.Resources.FilterColored.svg";
        public const string ClearIconResourceId = "showTracker.ViewModel.Resources.Clear.svg";
        public const string AscendingIconResourceId = "showTracker.ViewModel.Resources.Ascending.svg";
        public const string DescendingIconResourceId = "showTracker.ViewModel.Resources.Descending.svg";
        public const string LogoIconResourceId = "showTracker.ViewModel.Resources.Logo.svg";

        public const string FavouritesTileLabel = "Favourites";
        public const string SearchTileLabel = "Search";
        public const string AboutTileLabel = "About";
        public const string TodayTileLabel = "Today";

        public const string FavouritesPageTitle = "List of your favourite shows";
        public const string FavouritesSchedulePageTitle = "Schedule of your shows";
        public const string SearchPageTitle = "Search for shows";
        public const string ShowPageTitle = "Show is loading";
        public const string TodayPageTitle = "Episodes for selected date";
        public const string AboutPageTitle = "About showTracker";

        public const string GoToScheduler = "Go to the Scheduler";

        public const string SummaryTabLabel = "Summary";
        public const string CastTabLabel = "Cast";
        public const string EpisodesTabLabel = "Episodes";
        public const string Season = "Season";

        public const string NoItemsInCollection = "No items to display";
        public const string LoadMoreItems = "Load more items";

        public const string SettingsFavouritiesCollectionName = "FavouritiesShowCollection";

        public const string PopupAlertKey = "PopupAlert";
        public const string OkButtonText = "Ok";

        public const string NoInternetConnection = "No internet connection";
        public const string CheckYourInternetConnection = "Check our internet connection and try again";
        public const string UndefinedError = "Something went bad";
        public const string PleaseContactDeveloper = "Please contact developer if you encounter this message";
        public const string WrongQuery = "Wrong query";
        public const string ErrorDuringFetchingShow = "Error during fetching shows";
        public const string ErrorDuringFetchingEpisode = "Error during fetching episodes";
        public const string InvalidDateRangeTitle = "Invalid date range";
        public const string InvalidDateRangeMessage = "Selected date range cannot start after end date";

        public const string OrderByLabel = "Order By:";
        public const string MinRatingLabel = "Min. Rating:";
        public const string MinRuntimeLabel = "Min. Runtime:";
        public const string StatusLabel = "Status:";
        public const string GenreLabel = "Genre:";
        public const string GroupByLabel = "Group By:";

        public const string GenerateLabel = "Generate";
        public const string StartText = "Start:";
        public const string EndText = "End:";

        public const string EpisodeTitle = "Title:";
        public const string ShowName = "Show:";
        public const string Unknown = "Unknown";
        public const string MinutesShort = "min";

        public const string DefaultSearchPlaceholder = "Type search phrase...";

        public const string AppDescription = "ShowTracker is a simple app that allows you to access information about TV series and shows, store them as favourites and display shows scheduled for a given day.";
        public const string AuthorsLabel = "Authors:";
        public static readonly string[] Authors =
        {
            "Tomasz Błahut",
            "Bartosz Gniadek",
            "Daria Siemieniuk",
            "Paweł Berestka",
            "Olga Dobrzańska",
            "Przemysław Wardyński"
        };
    }
}
