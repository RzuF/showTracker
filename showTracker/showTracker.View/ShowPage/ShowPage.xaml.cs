﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace showTracker.ViewModel.ShowPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowPage : TabbedPage
    {
        public static readonly BindableProperty ViewModelProperty =
            BindableProperty.Create(nameof(ViewModel), typeof(ShowViewModel), typeof(ShowPage));

        public ShowViewModel ViewModel
        {
            get => (ShowViewModel) GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        public ShowPage ()
        {
            ViewModel = new ViewModelLocator().ShowViewModel;
            InitializeComponent();
        }
    }
}