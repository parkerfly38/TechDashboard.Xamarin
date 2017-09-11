using System;
using Xamarin.Forms;

namespace TechDashboard
{
	public class LoadingPage : ContentPage
	{
		public LoadingPage()
		{
			Image image = new Image();
			image.Source = "td_sq.png";
			image.HorizontalOptions = LayoutOptions.CenterAndExpand;
			image.VerticalOptions = LayoutOptions.CenterAndExpand;

			Label labelVersion = new Label() {
				Text = "Version 1.2",
				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.End
			};

			Content = new StackLayout() { Padding = 10, Children = { image, labelVersion } };
		}
	}
}

