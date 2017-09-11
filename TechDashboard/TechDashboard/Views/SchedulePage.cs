using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using TechDashboard.ViewModels;
using TechDashboard.Models;

namespace TechDashboard.Views
{
	public class SchedulePage : ContentPage
	{
        #region Helper Classes

        /// <summary>
        /// Class to display schedule data in a cell for a list.
        /// </summary>
        class ScheduledAppointmentDataCell : ViewCell
        {
            public ScheduledAppointmentDataCell()
            {
				int rowindex = 1;
				if (!Application.Current.Properties.ContainsKey("srowindex"))
				{
					Application.Current.Properties["srowindex"] = rowindex;
				}
				rowindex = Convert.ToInt32(Application.Current.Properties["srowindex"]);
				Color rowcolor = Color.FromHex("#FFFFFF");
				if (rowindex % 2 == 0)
				{
					rowcolor = Color.FromHex("#ECF0F1");
				} else {
					rowcolor = Color.FromHex("#FFFFFF");
				}
				rowindex = rowindex + 1;
				Application.Current.Properties["srowindex"] = rowindex;
				Color textColor = Color.FromHex("#95A5A6");
				// ticket number
                Xamarin.Forms.Label labelServiceTicketNumber = new Xamarin.Forms.Label();
                labelServiceTicketNumber.FontSize = 20;
				labelServiceTicketNumber.FontFamily = Device.OnPlatform("OpenSans-Bold", null,null);
				labelServiceTicketNumber.TextColor = textColor;
                labelServiceTicketNumber.SetBinding(Xamarin.Forms.Label.TextProperty, "ServiceTicketNumber");

                // schedule date/time
                Xamarin.Forms.Label labelScheduleDateTime = new Xamarin.Forms.Label();
                labelScheduleDateTime.FontSize = 20;
                labelScheduleDateTime.FontFamily = Device.OnPlatform("OpenSans-Regular",null,null);
                labelScheduleDateTime.TextColor = textColor;
                labelScheduleDateTime.SetBinding(Xamarin.Forms.Label.TextProperty, "SchedDateStartTime", stringFormat: "{0:MM/dd/yyyy}");

                // name/location/phone
                Xamarin.Forms.Label labelNameLocPhone = new Xamarin.Forms.Label();
                labelNameLocPhone.FontSize = 20;
                labelNameLocPhone.TextColor = textColor;
                labelNameLocPhone.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
                labelNameLocPhone.SetBinding(Xamarin.Forms.Label.TextProperty, "NameLocPhone");

                // need a spot for the currently-clocked-into annotation
                Xamarin.Forms.Image imageClockedInCheckMark = new Image();
                imageClockedInCheckMark.SetBinding(Xamarin.Forms.Image.SourceProperty, "CurrentImageFileName");

                View = new StackLayout()
                {
                    Padding = 30,
                    Orientation = StackOrientation.Horizontal,
					BackgroundColor = rowcolor,
                    Children =
                    {
                        new StackLayout()
                        {
                            Children =
                            {
                                labelServiceTicketNumber,
                                new StackLayout()
                                {
                                    Orientation = StackOrientation.Vertical,
                                    Children =
                                    {
                                        labelScheduleDateTime,
                                        labelNameLocPhone
                                    }
                                }
                            }
                        },
                        imageClockedInCheckMark
                    }
                };
            }
        }

        #endregion

        SchedulePageViewModel _vm;
        Xamarin.Forms.Label _labelTitle;
        ListView _listViewScheduledAppointments;
        DatePicker filterStartDate;
        DatePicker filterEndDate;

        public SchedulePage ()
        {
            // Create the view model for this page
            _vm = new SchedulePageViewModel();

            this.BindingContext = _vm.ScheduleDetails;

			BackgroundColor = Color.White;
            // Create our screen objects

            //  Create a label for the technician list
            _labelTitle = new Xamarin.Forms.Label();
            _labelTitle.Text = "SCHEDULE";
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

            StackLayout stackDateFilter = new StackLayout();
            stackDateFilter.Padding = 30;
            stackDateFilter.Spacing = 10;

            filterStartDate = new DatePicker();
            filterEndDate = new DatePicker();

            Button buttonFilter = new Button()
            {
                FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null),
                TextColor = Color.White,
                Text = "FILTER TICKETS BY DATE",
                BackgroundColor = Color.FromHex("#2ECC71")
            };
            buttonFilter.Clicked += ButtonFilter_Clicked;

            App_Settings appSettings = App.Database.GetApplicationSettings();
            filterStartDate.Date = (DateTime.Now.AddDays(Convert.ToDouble(appSettings.ScheduleDaysBefore) * (-1))).Date;
            filterEndDate.Date = (DateTime.Now.AddDays(Convert.ToDouble(appSettings.ScheduleDaysAfter))).Date;

            stackDateFilter.Children.Add(filterStartDate);
            stackDateFilter.Children.Add(filterEndDate);
            stackDateFilter.Children.Add(buttonFilter);


            // Create a template to display each technician in the list
            var dataTemplateItem = new DataTemplate(typeof(ScheduledAppointmentDataCell));

            // Create the actual list
            _listViewScheduledAppointments = new ListView()
            {
                HasUnevenRows = true,
                BindingContext = _vm.ScheduleDetails,
                ItemsSource = _vm.ScheduleDetails,
                ItemTemplate = dataTemplateItem,
				SeparatorVisibility = SeparatorVisibility.None
            };
            _listViewScheduledAppointments.ItemTapped += ListViewScheduledAppointments_ItemTapped;

            Content = new StackLayout
            {
                BackgroundColor = Color.FromHex("#2980b9"),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    titleLayout,
                    stackDateFilter,
                    _listViewScheduledAppointments
                }
            };
        }

        private void ButtonFilter_Clicked(object sender, EventArgs e)
        {
            App_Settings appSettings = App.Database.GetApplicationSettings();
            DateTime lowerLimit = (DateTime.Now.AddDays(Convert.ToDouble(appSettings.ScheduleDaysBefore) * (-1))).Date;
            DateTime upperLimit = (DateTime.Now.AddDays(Convert.ToDouble(appSettings.ScheduleDaysAfter))).Date;


            if (lowerLimit > filterStartDate.Date || upperLimit < filterEndDate.Date)
            {
                DisplayAlert("Update Settings", "These dates exceed your application settings.  Please modify your range in there to import more data.", "OK");
            }

            _vm.filterScheduledAppointments(filterStartDate.Date, filterEndDate.Date);
        }

        private async void ListViewScheduledAppointments_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            TechDashboard.Models.App_ScheduledAppointment scheduledAppointment =
                e.Item as TechDashboard.Models.App_ScheduledAppointment;

            _listViewScheduledAppointments.SelectedItem = null; // so the item doesn't stay highlighted

            if (scheduledAppointment.IsCurrent)
            {
                // Just pop back to the root page which should already be showing what we want
                await Navigation.PopToRootAsync();
            }
            else
            {
                await Navigation.PushAsync(new TicketDetailsPage(scheduledAppointment));
            }
        }
    
	}
}
