﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using showTracker.Model.Enum;
using Xamarin.Forms;

namespace showTracker.BusinessLayer.Interfaces
{
    public interface INavigationService
    {
        Dictionary<ApplicationPageEnum, Type> PageDictionary { get; set; }
        Task Navigate(ApplicationPageEnum pageType, object message = null);
        Task NavigateBack();
    }
}
