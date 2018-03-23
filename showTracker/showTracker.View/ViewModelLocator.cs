﻿using System;
using System.Collections.Generic;
using System.Text;
using CommonServiceLocator;
using showTracker.ViewModel.MainPage;

namespace showTracker.ViewModel
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel => (MainViewModel) ServiceLocator.Current.GetInstance(typeof(MainViewModel));
    }
}