using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Text;

using Xamarin.Forms;

namespace TechDashboard.Views
{
	public class SmsMessagePage : ContentPage
	{
		public SmsMessagePage ()
        {
            // Set the page title.
            Title = "Messages"; 

            Xamarin.Forms.Button buttonSendSms = new Button() { Text = "Send SMS" };
            buttonSendSms.Clicked += ButtonSendSms_Click;

			BackgroundColor = Color.White;
            Content = new StackLayout {
                Children = {
                    new Xamarin.Forms.Label { Text = "Hello ContentPage" },
                    buttonSendSms
				}
			};
		}

        protected void ButtonSendSms_Click(object sender, EventArgs e)
        {
            DisplayAlert("Texting!", "A text would normally be sent here.", "OK");
        }
	}
}
