using CommonServiceLocator;
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
		    var unityContainer = new UnityContainer();

		    BusinessLayer.DependencyInjectionRegister.Register(unityContainer);
		    ViewModel.DependencyInjectionRegister.Register(unityContainer);

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
