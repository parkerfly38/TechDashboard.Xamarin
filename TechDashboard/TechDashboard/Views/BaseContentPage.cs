using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace TechDashboard.Views
{
	public class BaseContentPage : ContentPage
	{
        public static string[] TechnicianStatuses =
        {
            "PUKE",
            "I ain't doin this!",
            "OK, I've got it.",
            "I just got here",
            "All done!"
        };

        App _application;
        protected App MainApp
        {
            get { return _application; }
        }

		public BaseContentPage (App application) : base()
		{
            _application = application;
			BackgroundColor = Color.White;
			Content = new StackLayout {
				Children = {
					new Xamarin.Forms.Label { Text = "Hello BaseContentPage" }
				}
			};
		}
    }
}
