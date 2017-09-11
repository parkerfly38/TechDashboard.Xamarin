using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using TechDashboard.Models;
using TechDashboard.ViewModels;
using TechDashboard.Tools;

namespace TechDashboard.Views
{

	public class ClockInPage : ContentPage
	{
        ClockInPageViewModel _vm;
        TimePicker _pickerArriveTime;
        BindablePicker _pickerTechnicianStatus;
        BindablePicker _pickerTicketStatus;
        Xamarin.Forms.Label _labelTitle;

        public ClockInPage()
        {
            _vm = new ClockInPageViewModel();
            InitializePage();
        }

        public ClockInPage(TechDashboard.Models.App_ScheduledAppointment scheduleDetail)
        {
            _vm = new ClockInPageViewModel(scheduleDetail);
            InitializePage();
        }

        protected void InitializePage()
        {
            // Set the page title.
            Title = "Clock In";

			BackgroundColor = Color.White;
			Color asbestos = Color.FromHex("#7f8c8d");

            //  Create a label for the technician list
            _labelTitle = new Xamarin.Forms.Label();
            _labelTitle.Text = "CLOCK IN";
            _labelTitle.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
            _labelTitle.FontSize = 22;
            _labelTitle.TextColor = Color.White;
            _labelTitle.HorizontalTextAlignment = TextAlignment.Center;
            _labelTitle.VerticalTextAlignment = TextAlignment.Center;

            Grid titleLayout = new Grid()
            {
                BackgroundColor = Color.FromHex("#2980b9"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 80
            };
            titleLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            titleLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            titleLayout.Children.Add(_labelTitle, 0, 0);

            //Xamarin.Forms.Label mainLabel = new Xamarin.Forms.Label { Text = "CLOCK IN", FontFamily = Device.OnPlatform("OpenSans-Bold",null,null), TextColor = asbestos };

            _pickerArriveTime = new TimePicker { Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second) };
            if (App.Database.GetApplicationSettings().TwentyFourHourTime)
                _pickerArriveTime.Format = "HH:mm";


            _pickerTechnicianStatus = new BindablePicker { Title = "Technician Status", ItemsSource = _vm.TechnicianStatusList };
            _pickerTechnicianStatus.SetBinding(BindablePicker.DisplayPropertyProperty, "StatusDescription");
            for (int i = 0; i < _pickerTechnicianStatus.Items.Count; i++)
            {
                if(_pickerTechnicianStatus.Items[i] == _vm.DefaultArriveStatusCodeDescription)
                {
                    _pickerTechnicianStatus.SelectedIndex = i;
                    break;
                }
            }

            _pickerTicketStatus = new BindablePicker { Title = "Serivce Ticket Status", ItemsSource = _vm.ServiceTicketStatusList };
            _pickerTicketStatus.SetBinding(BindablePicker.DisplayPropertyProperty, "Description");
            for (int i = 0; i < _pickerTicketStatus.Items.Count; i++)
            {
                if (_pickerTicketStatus.Items[i] == _vm.DefaultServiceTicketArriveStatusCode + " - " + _vm.DefaultServiceTicketArriveStatusCodeDescription)
                {
                    _pickerTicketStatus.SelectedIndex = i;
                    break;
                }
            }


            Xamarin.Forms.Button buttonClockIn = new Button() { 
				Text = "CLOCK IN",
				FontFamily = Device.OnPlatform("OpenSans-Bold",null,null),
				TextColor = Color.White,
				BackgroundColor = asbestos
			};            
            buttonClockIn.Clicked += ButtonClockIn_Clicked;

            Content = new StackLayout {
                Padding = 30,
                Children = {
                    titleLayout,
                    _pickerArriveTime,
                    _pickerTechnicianStatus,
                    _pickerTicketStatus,
                    buttonClockIn              
				}
			};
		}

        protected async void ButtonClockIn_Clicked(object sender, EventArgs e)
        {
            if (_pickerTechnicianStatus.SelectedIndex < 0)
            {
                await DisplayAlert("Status", "Select a technician status.", "OK");
                return;
            }
            if (_pickerTicketStatus.SelectedIndex < 0)
            {
                await DisplayAlert("Status", "Select a ticket status.", "OK");
                return;
            }

            JT_TechnicianStatus selectedTechnicianStatus = _pickerTechnicianStatus.SelectedItem as JT_TechnicianStatus;
            JT_MiscellaneousCodes selectedTicketStatus = _pickerTicketStatus.SelectedItem as JT_MiscellaneousCodes;

            

            _vm.ClockIn(_pickerArriveTime.Time, selectedTechnicianStatus, selectedTicketStatus);
            await Navigation.PopToRootAsync();
        }
    }
}
