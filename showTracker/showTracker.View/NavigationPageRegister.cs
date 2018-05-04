using System;
using System.Collections.Generic;
using CommonServiceLocator;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.Enum;

namespace showTracker.ViewModel
{
    public static class NavigationPageRegister
    {
        public static void RegisterPages()
        {
            var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
            navigationService.PageDictionary = new Dictionary<ApplicationPageEnum, Type>
            {
                {ApplicationPageEnum.Unknown, null },
                {ApplicationPageEnum.MainPage, typeof(MainPage.MainPage) },
                {ApplicationPageEnum.AboutPage, typeof(AboutPage.AboutPage) },
                {ApplicationPageEnum.FavouritiesPage, typeof(FavouritiesPage.FavouritiesPage) },
                {ApplicationPageEnum.SearchPage, typeof(SearchPage.SearchPage) },
                {ApplicationPageEnum.TodayPage, typeof(TodayPage.TodayPage) },
                {ApplicationPageEnum.FavouritiesSchedulePage, typeof(FavouritiesSchedulePage.FavouritiesSchedulePage) },
                {ApplicationPageEnum.ShowPage, typeof(ShowPage.ShowPage) }
            };
        }
    }
}
