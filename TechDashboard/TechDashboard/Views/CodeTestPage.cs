using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace TechDashboard.Views
{
	public class CodeTestPage : BaseContentPage
	{
		public CodeTestPage (App application) : base(application)
		{

			BackgroundColor = Color.White;
			Content = new StackLayout {
				Children = {
					new Xamarin.Forms.Label { Text = "Hello CodeTestPage" }
				}
			};
		}
	}
}
