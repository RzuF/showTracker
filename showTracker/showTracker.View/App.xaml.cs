using CommonServiceLocator;
using showTracker.BusinessLayer;
using showTracker.BusinessLayer.ShowService;
using showTracker.View;
using showTracker.ViewModel.MainPage;
using Unity;
using Unity.ServiceLocation;
using Xamarin.Forms;

namespace showTracker.View
{
	public partial class App : Application
	{
		public App ()
		{
		    //DependencyInjectionRegister.Register();
		    var unityContainer = new UnityContainer();
		    unityContainer.RegisterType<IShowService, BusinessLayer.ShowService.ShowService>();
            unityContainer.RegisterInstance(typeof(MainViewModel));//optional

            var unityServiceLocator = new UnityServiceLocator(unityContainer);
		    ServiceLocator.SetLocatorProvider(() => unityServiceLocator);

            InitializeComponent();

			MainPage = new MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
