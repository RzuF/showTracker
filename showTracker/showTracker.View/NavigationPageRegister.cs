using System.Collections.Generic;
using CommonServiceLocator;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.Enum;
using Xamarin.Forms;

namespace showTracker.ViewModel
{
    public static class NavigationPageRegister
    {
        public static void RegisterPages()
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            navigationService.PageDictionary = new Dictionary<ApplicationPageEnum, Page>
            {
                {ApplicationPageEnum.Unknown, null },
                {ApplicationPageEnum.MainPage, new MainPage.MainPage() },
                {ApplicationPageEnum.AboutPage, new AboutPage.AboutPage() },
                {ApplicationPageEnum.FavouritiesPage, new FavouritiesPage.FavouritiesPage() },
                {ApplicationPageEnum.SearchPage, new SearchPage.SearchPage() },
                {ApplicationPageEnum.TodayPage, new TodayPage.TodayPage() }
            };
        }
    }
}
