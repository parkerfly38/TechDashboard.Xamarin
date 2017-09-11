using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;

namespace TechDashboard.Droid
{
	[Activity (Label = "TechDashboard", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            DevExpress.Mobile.Forms.Init();

            // http://developer.xamarin.com/guides/xamarin-forms/working-with/maps/
            global::Xamarin.FormsMaps.Init(this, bundle); // puke... needed for maps page

            //C:\Program Files (x86)\Java\jdk1.7.0_55\bin > keytool - list - v - keystore "C:\Users\ghripto\AppData\Local\Xamarin\Mono for Android\debug.keystore" -alias androiddebugkey - storepass android - keypass android

            // puke... hide main app icon in navigation links at top of screen.
            //ActionBar.SetIcon(new Android.Graphics.Drawables.ColorDrawable(Resources.GetColor(Android.Resource.Color.Transparent)));

            //integrate hockeyapp crash logging
            CheckForUpdates();

            LoadApplication (new TechDashboard.App ());
		}

        private void CheckForUpdates()
        {
            // Remove this for store builds!
            UpdateManager.Register(this, "6d6fc0dd47bc4d8687e7f78645f8e62a");
        }

        private void UnregisterManagers()
        {
            UpdateManager.Unregister();
        }

        protected override void OnPause()
        {
            base.OnPause();
            UnregisterManagers();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterManagers();
        }
	}
}

