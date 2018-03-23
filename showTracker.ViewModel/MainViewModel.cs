using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using showTracker.BusinessLayer.ShowService;
using Xamarin.Forms;

namespace showTracker.ViewModel
{
    public class MainViewModel
    {
        private readonly IShowService _productsService;
        public IEnumerable Shows { get; set; }
        public MainViewModel()
        {
            _productsService = new ShowService();
            DownloadProducts();
        }
        public void DownloadProducts()
        {
            Shows = _productsService.GetShows();
        }
    }
}
