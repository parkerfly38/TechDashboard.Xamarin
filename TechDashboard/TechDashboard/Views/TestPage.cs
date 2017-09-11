using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace TechDashboard.Views
{
	public class TestPage : ContentPage
	{
		public TestPage ()
		{
            Title = "Test Page Title";

			BackgroundColor = Color.White;
			Content = new StackLayout {
				Children = {
					new Xamarin.Forms.Label { Text = "Hello TestPage" }
				}
			};
		}
	}
}
