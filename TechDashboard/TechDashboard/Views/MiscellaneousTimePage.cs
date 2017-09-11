using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using TechDashboard.ViewModels;
using TechDashboard.Models;
using TechDashboard.Tools;

namespace TechDashboard.Views
{
	public class MiscellaneousTimePage : ContentPage
	{

		TimePicker startTimePicker = new TimePicker();
		TimePicker endTimePicker = new TimePicker();
		DatePicker transactionDatePicker = new DatePicker();
		Picker earningCodePicker = new Picker();
		MiscellaneousTimePageViewModel _vm;
        Xamarin.Forms.Label _labelTitle;
        Dictionary<string, string> earningCodeToDesc = new Dictionary<string, string>();

        static Color asbestos = Color.FromHex("#7F8C8D");
        Color emerald = Color.FromHex("#2ECC71");
        Color alizarin = Color.FromHex("#E74C3C");
        Color peterriver = Color.FromHex("#3498DB");

        class TimeEntryCell : ViewCell
		{
			public TimeEntryCell()
			{
				int rowindex = 1;
				if (!Application.Current.Properties.ContainsKey("timerowindex"))
				{
					Application.Current.Properties["timerowindex"] = rowindex;
				}
				rowindex = Convert.ToInt32(Application.Current.Properties["timerowindex"]);
				Color rowcolor = Color.FromHex("#FFFFFF");
				if (rowindex % 2 == 0)
				{
					rowcolor = Color.FromHex("#ECF0F1");
				} else {
					rowcolor = Color.FromHex("#FFFFFF");
				}
				rowindex = rowindex + 1;
				Application.Current.Properties["timerowindex"] = rowindex;
				Color textColor = Color.FromHex("#95A5A6");

				// schedule date/time
				Xamarin.Forms.Label labelTransactionDate = new Xamarin.Forms.Label();
				labelTransactionDate.FontFamily = Device.OnPlatform("OpenSans-Regular",null,null);
				labelTransactionDate.TextColor = textColor;
				labelTransactionDate.SetBinding(Xamarin.Forms.Label.TextProperty, "TransactionDate", stringFormat: "{0:MM/dd/yyyy}");

				// name/location/phone
				Xamarin.Forms.Label labelStartTime = new Xamarin.Forms.Label();
				labelStartTime.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
				labelStartTime.TextColor = textColor;
				labelStartTime.SetBinding(Xamarin.Forms.Label.TextProperty, "StartTime");

				Xamarin.Forms.Label labelEndTime = new Xamarin.Forms.Label();
				labelEndTime.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
				labelEndTime.TextColor = textColor;
				labelEndTime.SetBinding(Xamarin.Forms.Label.TextProperty, "EndTime");

				View = new StackLayout()
				{
					Orientation = StackOrientation.Horizontal,
					Spacing = 10,
					BackgroundColor = rowcolor,
					Children =
					{
						labelTransactionDate,
						labelStartTime,
						labelEndTime
					}
					};
			}
		}

		public MiscellaneousTimePage()
		{
			Title = "Miscellaneous Time";
			Color asbestos = Color.FromHex("#7F8C8D");

			BackgroundColor = Color.White;
			_vm = new MiscellaneousTimePageViewModel();

            //  Create a label for the technician list
            _labelTitle = new Xamarin.Forms.Label();
            _labelTitle.Text = "MISCELLANEOUS TIME";
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

			Label durationTextCell = new Label {

				FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
				TextColor = asbestos,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

			foreach (var item in _vm.EarningsCode) {
                earningCodeToDesc.Add(item.EarningsCode + " - " + item.EarningsDeductionDesc, item.EarningsCode);
				earningCodePicker.Items.Add(item.EarningsCode + " - " + item.EarningsDeductionDesc);
			}

			Button buttonAccept = new Button() {
				Text = "ACCEPT",
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.FromHex("#2ECC71"),
			};
			buttonAccept.Clicked += ButtonAccept_Clicked;
			Button buttonCancel = new Button() {
				Text = "CANCEL",
				FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
				TextColor = Color.White,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.FromHex("#E74C3C")
			};
			buttonCancel.Clicked += ButtonCancel_Clicked;
 			
 			startTimePicker.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
                {
                    // Calculate Hours
                    durationTextCell.Text = CalcHours();
                }
            };
            
            endTimePicker.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == TimePicker.TimeProperty.PropertyName)
                {
                    // Calculate Hours
                    durationTextCell.Text = CalcHours();
                }
            };
            
            ListView timeEntries = new ListView()
            {
                ItemsSource = _vm.TimeEntries,
                SeparatorVisibility = SeparatorVisibility.None,
                ItemTemplate = new DataTemplate(typeof(TimeEntryCell)),
                Header = new Xamarin.Forms.Label
                {
                    Text = "Date - Start Time - End Time",
                    FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                    TextColor = Color.FromHex("#FFFFFF"),
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.Center,
                    BackgroundColor = peterriver
                }
            };

            Grid topGrid = new Grid();
            topGrid.RowDefinitions.Add(new RowDefinition { Height = 20 });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = 20 });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = 20 });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = 20 });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = 20 });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = 20 });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 300 });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 150 });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 250 });

            topGrid.Children.Add(new Label
            {
                Text = "EMPLOYEE NO",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos,
                HorizontalOptions = LayoutOptions.FillAndExpand
            }, 0, 0);
            topGrid.Children.Add(new Label
            {
                Text = _vm.AppTechnician.TechnicianNo,
                FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
                TextColor = asbestos,
                HorizontalOptions = LayoutOptions.FillAndExpand
            }, 1, 0);
            topGrid.Children.Add(new Label
            {
                Text = _vm.AppTechnician.FirstName + " " + _vm.AppTechnician.LastName,
                FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null),
                TextColor = asbestos,
                HorizontalOptions = LayoutOptions.FillAndExpand
            }, 2, 0);

            topGrid.Children.Add(new Label
            {
                Text = "TRANSACTION DATE",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos,
                HorizontalOptions = LayoutOptions.FillAndExpand
            }, 0, 1);
            topGrid.Children.Add(transactionDatePicker, 1, 1);

            topGrid.Children.Add(new Label
            {
                Text = "START TIME",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos,
                HorizontalOptions = LayoutOptions.FillAndExpand
            }, 0, 2);
            topGrid.Children.Add(startTimePicker, 1, 2);

            topGrid.Children.Add(new Label
            {
                Text = "END TIME",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos,
                HorizontalOptions = LayoutOptions.FillAndExpand
            }, 0, 3);
            topGrid.Children.Add(endTimePicker, 1, 3);
            
            topGrid.Children.Add(new Label
            {
                Text = "HOURS WORKED",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos,
                HorizontalOptions = LayoutOptions.FillAndExpand
            }, 0, 4);
            topGrid.Children.Add(durationTextCell, 1, 4);

            topGrid.Children.Add(new Label
            {
                Text = "EARNINGS CODE",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos,
                HorizontalOptions = LayoutOptions.FillAndExpand
            }, 0, 5);
            topGrid.Children.Add(earningCodePicker, 1, 5);
            Grid.SetColumnSpan(earningCodePicker, 2);

            Content = new StackLayout() {
				Padding = 30,
				Children = {
                    titleLayout,
                    topGrid,
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Children = {
							buttonAccept,
							buttonCancel
						}
					},
					timeEntries
				}
			};			
		}

        private string CalcHours()
        {
            // Calculate and display hours worked
            string sHours = "0";
            TimeSpan durStart = new TimeSpan(startTimePicker.Time.Hours, startTimePicker.Time.Minutes, 0);
            TimeSpan durEnd = new TimeSpan(endTimePicker.Time.Hours, endTimePicker.Time.Minutes, 0);
            TimeSpan diff = durEnd.Subtract(durStart);
            double dHours = diff.Hours;
            double dMinutes = Math.Round((double)diff.Minutes / 60, 2, MidpointRounding.AwayFromZero);
            sHours = (dHours + dMinutes).ToString();
            return sHours;
        }

        protected async void ButtonCancel_Clicked (object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		protected async void ButtonAccept_Clicked (object sender, EventArgs e)
		{
			JT_DailyTimeEntry newTimeEntry = new JT_DailyTimeEntry();
			JT_Technician currentTechnician = App.Database.GetCurrentTechnicianFromDb();

			newTimeEntry.DepartmentNo = currentTechnician.TechnicianDeptNo;
			newTimeEntry.EmployeeNo = currentTechnician.TechnicianNo;
			newTimeEntry.EndTime = endTimePicker.Time.ToSage100TimeString();
			newTimeEntry.IsModified = true;
			newTimeEntry.StartTime = startTimePicker.Time.ToSage100TimeString();
			newTimeEntry.TransactionDate = transactionDatePicker.Date;
			newTimeEntry.WTNumber = currentTechnician.CurrentWTNumber;
			newTimeEntry.WTStep = currentTechnician.CurrentWTStep;
            newTimeEntry.EarningsCode = earningCodeToDesc[earningCodePicker.Items[earningCodePicker.SelectedIndex]];

			_vm.DailyTimeEntry = newTimeEntry;
            _vm.SaveDailyTimeEntry(Convert.ToDouble(CalcHours()));

			await Navigation.PopAsync();

		}
	}
}

