using System;
using CommonServiceLocator;
using showTracker.BusinessLayer.Interfaces;
using showTracker.Model.API.Dto;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Constants = showTracker.Model.Constants;

namespace showTracker.ViewModel.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowContainerItem : Grid
    {
        public static readonly BindableProperty ShowProperty =
            BindableProperty.Create(nameof(Show), typeof(ShowDto), typeof(ShowContainerItem),
                propertyChanged: (bindable, value, newValue) =>
                {
                    var logger = ServiceLocator.Current.GetInstance<ISTLogger>();
                    logger.Log("Show set: ");
                    logger.LogWithSerialization(newValue);
                });

        public ShowDto Show
        {
            get => (ShowDto) GetValue(ShowProperty);
            set => SetValue(ShowProperty, value);
        }

        public static readonly BindableProperty ItemHeightProperty = 
            BindableProperty.Create(nameof(ItemHeight), typeof(double), typeof(ShowContainerItem), Constants.DefaultShowContainerItemHeight);

        public double ItemHeight
        {
            get => (double) GetValue(ItemHeightProperty);
            set => SetValue(ItemHeightProperty, value);
        }

        public string FavouriteIcon => Constants.FavouriteIconResourceId;
        public string Rating => Show?.Rating != null ? $"{Show?.Rating}/10" : "??/10";
        public DateTime Premiered => Show?.Premiered ?? DateTime.MinValue;

        public ShowContainerItem ()
		{
			InitializeComponent ();
        }
	}
}