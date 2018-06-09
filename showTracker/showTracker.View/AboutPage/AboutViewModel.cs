using showTracker.Model.View;
using showTracker.Model;

namespace showTracker.ViewModel.AboutPage
{
    public class AboutViewModel: BaseViewModel
    {
        public AboutViewModel()
        {
            PageTitle = Constants.AboutPageTitle;
        }

        public string AppName => $"{Constants.ApplicationName} {string.Join(".", Constants.Version)}";
        public string LogoIcon => Constants.LogoIconResourceId;
        public string Description => Constants.AppDescription;
        public string Authors => string.Join("\n", Constants.Authors);
        public string AuthorsLabel => Constants.AuthorsLabel;
    }
}
