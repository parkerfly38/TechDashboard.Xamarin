using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechDashboard.ViewModels;

using Xamarin.Forms;
using TechDashboard.Models;

namespace TechDashboard.Views
{
	public class HistoryPage : ContentPage
	{


		Color asbestos = Color.FromHex("#7F8C8D");
		Color emerald = Color.FromHex("#2ECC71");
		Color alizarin = Color.FromHex("#E74C3C");
		Color peterriver = Color.FromHex("#3498DB");

		Picker _pickerWorkTicket;

		public HistoryPage ()
        {
            // Set the page title.
            Title = "History";

			BackgroundColor = Color.White;
			StackLayout pageLayout = new StackLayout();

			_pickerWorkTicket = new Picker();
			_pickerWorkTicket = new Xamarin.Forms.Picker { 
				Title = "WORK TICKET",
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			foreach (App_ScheduledAppointment appt in App.Database.GetScheduledAppointments())
			{
                App_WorkTicket _workTicket = App.Database.GetWorkTicket2(appt.ServiceTicketNumber);
                if (_workTicket != null) {
                    if (_workTicket.DtlRepairItemCode != null && _workTicket.DtlMfgSerialNo != null) {
                        if (!_pickerWorkTicket.Items.Contains(appt.ServiceTicketNumber)) {
                            _pickerWorkTicket.Items.Add(appt.ServiceTicketNumber);
                        }
                    }
                }
			}            
			//_pickerWorkTicket.SelectedIndexChanged += _pickerWorkTicket_SelectedIndexChanged;
			Button buttonWorkTicket = new Button() {
				Text = "OPEN TICKET HISTORY",
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				BackgroundColor = emerald,
				TextColor = Color.White
			};
			buttonWorkTicket.Clicked += ButtonWorkTicket_Clicked;

			pageLayout = new StackLayout {
					Padding = 30,
					Children = {
						new StackLayout {
							BackgroundColor = peterriver,
							Padding = 50,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							VerticalOptions = LayoutOptions.Start,
							Children = {
								new Xamarin.Forms.Label { 
									Text = "HISTORY",
									FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
									TextColor = Color.White,
									HorizontalOptions = LayoutOptions.CenterAndExpand,
									VerticalOptions = LayoutOptions.Center
								}
							}
						},
						new StackLayout {
							Padding = 50,
							HorizontalOptions = LayoutOptions.FillAndExpand,
							VerticalOptions = LayoutOptions.Start,
							Children = {
								_pickerWorkTicket,
								new Label() {
									Text = "PLEASE SELECT A TICKET FIRST",
									FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
									TextColor = alizarin,
									HorizontalOptions = LayoutOptions.CenterAndExpand,
									VerticalOptions = LayoutOptions.Center
								},
								buttonWorkTicket
							}
						}
					}
				};

			if (App.Database.GetCurrentWorkTicket() != null)
			{
				_pickerWorkTicket.SelectedIndex = _pickerWorkTicket.Items.IndexOf(App.Database.GetCurrentWorkTicket().FormattedTicketNumber);
			}

			Content = pageLayout;

		}

		async void ButtonWorkTicket_Clicked (object sender, EventArgs e)
		{
			string selectedTicketNumber = _pickerWorkTicket.Items.ElementAt(_pickerWorkTicket.SelectedIndex);
			await Navigation.PushAsync(new HistoryPageDetail(selectedTicketNumber));
		}


	}
}
