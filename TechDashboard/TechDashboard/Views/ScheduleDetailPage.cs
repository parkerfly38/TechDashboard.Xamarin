using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using TechDashboard.Models;

using Xamarin.Forms;
using TechDashboard.ViewModels;

namespace TechDashboard.Views
{
	public class ScheduleDetailPage : ContentPage
	{
		ScheduleDetailPageViewModel _vm;

		public ScheduleDetailPage (App_ScheduledAppointment scheduledAppointment)
		{
			_vm = new ScheduleDetailPageViewModel(scheduledAppointment);
			InitializePage ();
		}
	
		protected void InitializePage()
		{

			BackgroundColor = Color.White;
			this.Title = "Schedule Details";
			StackLayout stackLayout = new StackLayout ();
			//stackLayout.BackgroundColor = Color.FromHex ("#bcd5d1");
			stackLayout.Padding = 30;

            Color asbestos = Color.FromHex("#7f8c8d");

            Xamarin.Forms.Label labelModalTitle = new Xamarin.Forms.Label()
            {
                //FontAttributes = FontAttributes.Bold,
                FontSize = 22,
                Text = "SCHEDULE DETAILS",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };

            Grid titleLayout = new Grid()
            {
                BackgroundColor = Color.FromHex("#2980b9"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 80
            };
            titleLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            titleLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            titleLayout.Children.Add(labelModalTitle, 0, 0);
            stackLayout.Children.Add(titleLayout);

            Grid grid = new Grid ();
			grid.RowSpacing = 5;
			grid.ColumnSpacing = 5;
			grid.RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star) });
			grid.RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star) });
			grid.RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star) });
			grid.RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star) });
			grid.RowDefinitions.Add (new RowDefinition { Height = new GridLength (1, GridUnitType.Star) });
			grid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });
			grid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });
			grid.ColumnDefinitions.Add (new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) });

            Xamarin.Forms.Label labelScheduledTitle = new Xamarin.Forms.Label() { 
				//FontAttributes = FontAttributes.Bold,
				TextColor = asbestos,
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				Text = "SCHEDULED"
			};
			grid.Children.Add (labelScheduledTitle, 1, 0);

            Xamarin.Forms.Label labelActualTitle = new Xamarin.Forms.Label() {
				//FontAttributes = FontAttributes.Bold,
				TextColor = asbestos,
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				Text = "ACTUAL"
			};
			grid.Children.Add(labelActualTitle, 2, 0);

            Xamarin.Forms.Label labelDateTitle = new Xamarin.Forms.Label();
			labelDateTitle.Text = "Date";
			//labelDateTitle.FontAttributes = FontAttributes.Bold;
			labelDateTitle.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			labelDateTitle.TextColor = asbestos;
			grid.Children.Add (labelDateTitle, 0, 1);

            Xamarin.Forms.Label labelStartTimeTitle = new Xamarin.Forms.Label();
			labelStartTimeTitle.Text = "Start Time";
			//labelStartTimeTitle.FontAttributes = FontAttributes.Bold;
			labelStartTimeTitle.TextColor = asbestos;
			labelStartTimeTitle.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			grid.Children.Add (labelStartTimeTitle, 0, 2);

            Xamarin.Forms.Label labelEndTimetitle = new Xamarin.Forms.Label();
			labelEndTimetitle.Text = "End Time";
			//labelEndTimetitle.FontAttributes = FontAttributes.Bold;
			labelEndTimetitle.TextColor = asbestos;
			labelEndTimetitle.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			grid.Children.Add (labelEndTimetitle, 0, 3);

            Xamarin.Forms.Label labelDurationTitle = new Xamarin.Forms.Label();
			labelDurationTitle.Text = "Duration";
			labelDurationTitle.TextColor = asbestos;
			labelDurationTitle.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
			grid.Children.Add (labelDurationTitle, 0, 4);

            Xamarin.Forms.Label labelScheduledDate = new Xamarin.Forms.Label() {
				Text = _vm.ScheduleDetail.ScheduleDate.ToShortDateString(),
				TextColor = asbestos,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null)
			};
			grid.Children.Add(labelScheduledDate, 1, 1);

            Xamarin.Forms.Label labelScheduledStartTime = new Xamarin.Forms.Label() {
                // dch rkl 10/12/2016 show formatted time
                //Text = _vm.ScheduleDetail.StartTime,
                Text = _vm.ScheduleDetail.StartTimeFormatted,
                TextColor = asbestos,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null)
			};
			grid.Children.Add(labelScheduledStartTime, 1, 2);

            Xamarin.Forms.Label labelScheduledEndTime = new Xamarin.Forms.Label() {
                // dch rkl 10/12/2016 show formatted time
                //Text = "",
                Text = _vm.ScheduleDetail.EndTimeFormatted,
                TextColor = asbestos,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null)
			};
			grid.Children.Add(labelScheduledEndTime, 1, 3);

            Xamarin.Forms.Label labelActualDate = new Xamarin.Forms.Label() {
                Text = _vm.TechnicianScheduleDetail.ScheduleDate.ToShortDateString(),
				TextColor = asbestos,
				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null)
			};
			grid.Children.Add(labelActualDate, 2, 1);

            bool bStartTimeSet = false;     // dch rkl 10/14/2016 Get Start Time from the Technician Record

            if (_vm.TimeEntryDetail != null) {
				if (_vm.TimeEntryDetail.StartTime != null) {
					Xamarin.Forms.Label labelActualStartTime = new Xamarin.Forms.Label() {
                        Text = FormattedTime(_vm.TimeEntryDetail.StartTime),
						TextColor = asbestos,
						FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null)
					};
					grid.Children.Add(labelActualStartTime, 2, 2);
                    bStartTimeSet = true;    // dch rkl 10/14/2016 Get Start Time from the Technician Record
                }

                if (_vm.TimeEntryDetail.EndTime != null) {
					Xamarin.Forms.Label labelActualEndTime = new Xamarin.Forms.Label() {
                        Text = FormattedTime(_vm.TimeEntryDetail.EndTime),
						TextColor = asbestos,
						FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null)
					};
					grid.Children.Add(labelActualEndTime, 2, 3);
				}
			}

            // dch rkl 10/14/2016 Get Start Time from the Technician Record BEGIN
            if (bStartTimeSet == false)
            {
                JT_Technician tech = App.Database.GetCurrentTechnicianFromDb();
                if (tech.CurrentStartTime != null)
                {
                    Xamarin.Forms.Label labelActualStartTime = new Xamarin.Forms.Label()
                    {
                        Text = FormattedTime(tech.CurrentStartTime),
                        TextColor = asbestos,
                        FontFamily = Device.OnPlatform("OpenSans-Regular", "sans-serif", null)
                    };
                    grid.Children.Add(labelActualStartTime, 2, 2);
                }
            }
            // dch rkl 10/14/2016 Get Start Time from the Technician Record END


            //compute duration, if available

            stackLayout.Children.Add (grid);

			Button buttonCloseSchedule = new Button() {
				Text = "OK",
				TextColor = Color.White,
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				BackgroundColor = Color.FromHex("#2ecc71"),
				VerticalOptions = LayoutOptions.Fill
			};
			buttonCloseSchedule.Clicked += ButtonCloseSchedule_Clicked;
			stackLayout.Children.Add(buttonCloseSchedule);

			Content = stackLayout;
		}

		void ButtonCloseSchedule_Clicked (object sender, EventArgs e)
		{
			this.Navigation.PopModalAsync();
		}

        // dch rkl 10/14/2016 format the time - Note: This logic should be added to the JT_Technician model
        private string FormattedTime(string sTimeIn)
        {
            if (sTimeIn == null) { sTimeIn = ""; }

            string sTimeOut = sTimeIn;

            string sHour = "";
            string sMin = "";
            string sAMorPM = "";
            int iHour = 0;

            if (sTimeIn.Length == 4)
            {
                sHour = sTimeIn.Substring(0, 2);
                sMin = sTimeIn.Substring(2, 2);
            }
            else if (sTimeIn.Length == 3)
            {
                sHour = "0" + sTimeIn.Substring(0, 1);
                sMin = sTimeIn.Substring(1, 2);
            }

            int.TryParse(sHour, out iHour);
            if (iHour > 0)
            {
                if (iHour < 12) { sAMorPM = "AM"; }
                else { sAMorPM = "PM"; }

                App_Settings appSettings = App.Database.GetApplicationSettings();
                if (appSettings.TwentyFourHourTime == false && iHour > 12)
                {
                    iHour = iHour - 12;
                    sHour = iHour.ToString();
                    if (sHour.Length == 1) { sHour = "0" + sHour; }
                }

                sTimeOut = string.Format("{0}:{1} {2}", sHour, sMin, sAMorPM);
            }

            return sTimeOut;
        }
    }
}

