﻿using showTracker.ViewModel.CustomControls;
using showTracker.ViewModel.MainPage;
using showTracker.ViewModel.SearchPage;
using Unity;

namespace showTracker.ViewModel
{
    public static class DependencyInjectionRegister
    {
        public static void Register(UnityContainer unityContainer)
        {
            unityContainer.RegisterInstance(typeof(MainViewModel));
            unityContainer.RegisterInstance(typeof(ShowConatinerViewModel));
            unityContainer.RegisterInstance(typeof(SearchViewModel));
        }
    }
}
