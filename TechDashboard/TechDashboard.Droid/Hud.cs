using System;
using Xamarin.Forms;
using Android.App;
using AndroidHUD;


[assembly: Xamarin.Forms.Dependency(typeof(TechDashboard.Droid.Hud))]
namespace TechDashboard.Droid
{
	public class Hud : IHud
	{
		public Hud()
		{
		}

		public void Show()
		{
			AndHUD.Shared.Show(Forms.Context);
		}

		public void Show(string status)
		{
			AndHUD.Shared.Show(Forms.Context, status);
		}

		public void Dismiss()
		{
			AndHUD.Shared.Dismiss();
		}
	}
}

