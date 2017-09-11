using System;
using System.Collections.Generic;
using System.Linq;
using XLabs.Ioc;
using HockeyApp.iOS;

using Foundation;
using UIKit;

namespace TechDashboard.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			SetupIocContainer();
			global::Xamarin.Forms.Forms.Init();
			// http://developer.xamarin.com/guides/xamarin-forms/working-with/maps/
			global::Xamarin.FormsMaps.Init(); // puke... needed for maps
            DevExpress.Mobile.Forms.Init();

            //HockeyApp integration
            var manager = BITHockeyManager.SharedHockeyManager;
            manager.Configure("083c55bd919f4f32b8620511a73a920c");
            manager.StartManager();
            manager.Authenticator.AuthenticateInstallation();
            //this implemented crash reporting

            manager.StartManager();

			LoadApplication(new TechDashboard.App());

			return base.FinishedLaunching(app, options);
		}

		private void SetupIocContainer()
		{
			var resolverContainer = new SimpleContainer();
			resolverContainer.Register<IHud>(t => t.Resolve<IHud>());

			Resolver.SetResolver(resolverContainer.GetResolver());
		}
	}
}
