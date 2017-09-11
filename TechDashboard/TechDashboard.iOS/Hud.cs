using Xamarin.Forms;
using BigTed;

[assembly: Dependency(typeof(TechDashboard.iOS.Hud))]
namespace TechDashboard.iOS
{
	public class Hud : IHud
	{
		public Hud()
		{
		}

		public void Show()
		{
			BTProgressHUD.Show();
		}

		public void Show(string status)
		{
			BTProgressHUD.Show(status,-1,ProgressHUD.MaskType.Black);
		}

		public void Dismiss()
		{
			BTProgressHUD.Dismiss();
		}
	}
}

