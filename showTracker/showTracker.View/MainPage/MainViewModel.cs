using System.Collections;
using CommonServiceLocator;
using showTracker.BusinessLayer.ShowService;

namespace showTracker.ViewModel.MainPage
{
    public class MainViewModel
    {
        private readonly IShowService _productsService;
        public IEnumerable Shows { get; set; }
        public MainViewModel(IShowService showService)
        {
            _productsService = showService;
            DownloadProducts();
        }
        public void DownloadProducts()
        {
            Shows = _productsService.GetShows();
        }
    }
}
