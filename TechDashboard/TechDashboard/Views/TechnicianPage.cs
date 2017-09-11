using System;

using Xamarin.Forms;
using TechDashboard.Models;
using TechDashboard.Tools;
using TechDashboard.ViewModels;

namespace TechDashboard.Views
{
    public class TechnicianPage : ContentPage
    {
        TechnicianPageViewModel _vm;
        Label _labelTitle;
        Label _labelTechnicianNo;
        Label _labelFirstName;
        Label _labelLastName;
        BindablePicker _pickerTechnicianStatus;
        Button _buttonOK;

        public TechnicianPage()
        {
            // Set the page title.
            Title = "Technician";

			BackgroundColor = Color.White;
            //  Create a label for the technician list
            _labelTitle = new Label();
            _labelTitle.Text = "TECHNICIAN DETAILS";
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

            _vm = new TechnicianPageViewModel();

            Color asbestos = Color.FromHex("#7f8c8d");
            this.BindingContext = _vm.Technician;

            _labelTechnicianNo = new Label();
            _labelTechnicianNo.TextColor = asbestos;
            _labelTechnicianNo.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
            _labelTechnicianNo.SetBinding(Xamarin.Forms.Label.TextProperty, "FormattedTechnicianNumber");

            _labelFirstName = new Label();
            _labelFirstName.TextColor = asbestos;
            _labelFirstName.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
            _labelFirstName.SetBinding(Xamarin.Forms.Label.TextProperty, "FirstName");

            _labelLastName = new Label();
            _labelLastName.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
            _labelLastName.TextColor = asbestos;
            _labelLastName.SetBinding(Xamarin.Forms.Label.TextProperty, "LastName");

            _pickerTechnicianStatus = new BindablePicker { Title = "Technician Status", ItemsSource = _vm.TechnicianStatusList };
            _pickerTechnicianStatus.SetBinding(BindablePicker.DisplayPropertyProperty, "StatusDescription");
            SetPickerTechnicianStatus();

            _buttonOK = new Button();
            _buttonOK.Text = "OK";
            _buttonOK.VerticalOptions = LayoutOptions.Fill;
            _buttonOK.BackgroundColor = Color.FromHex("#3498DB");
            _buttonOK.TextColor = Color.White;
            _buttonOK.Clicked += ButtonOK_Clicked;

            Content = new StackLayout
            {
                Padding = 30,
                Children = {
                    titleLayout,
     //               new Xamarin.Forms.Label 
					//{ Text = "TECHNICIAN DETAILS", FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null), TextColor = asbestos },
                    _labelTechnicianNo,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,

                        Children =
                        {
                            _labelFirstName,
                            new Xamarin.Forms.Label { Text = ", ",FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null), TextColor = asbestos },
                            _labelLastName,
                        }
                    },
                    _pickerTechnicianStatus,
                    _buttonOK
                }
            };
        }

        protected async void ButtonOK_Clicked(object sender, EventArgs e)
        {
            JT_TechnicianStatus selectedStatus = _pickerTechnicianStatus.SelectedItem as JT_TechnicianStatus;
            if (selectedStatus.StatusCode != _vm.Technician.CurrentStatus)
            {
                // update the status code
                _vm.UpdateTechnicianStatus(selectedStatus);
            }

            if (App.Database.GetCurrentWorkTicket() == null)
            {
                await Navigation.PopAsync();
            }
            else
            {
                await Navigation.PushAsync(new TicketDetailsPage());
            }
        }

        protected void SetPickerTechnicianStatus()
        {
            foreach (JT_TechnicianStatus status in _pickerTechnicianStatus.ItemsSource)
            {
                if (status.StatusCode == _vm.Technician.CurrentStatus)
                {
                    _pickerTechnicianStatus.SelectedItem = status;
                    return;
                }
            }
        }
    }
}
