using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using TechDashboard.Models;
using TechDashboard.ViewModels;

namespace TechDashboard.Views
{
    public class CustomerContactViewCell : ViewCell
    {
        public CustomerContactViewCell()
        {

			Color asbestos = Color.FromHex("#7f8c8d");
            Xamarin.Forms.Label labelContactCode = new Xamarin.Forms.Label();
            labelContactCode.SetBinding(Xamarin.Forms.Label.TextProperty, "ContactCode");
			labelContactCode.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
			labelContactCode.TextColor = asbestos;

            Xamarin.Forms.Label labelContactName = new Xamarin.Forms.Label();
            labelContactName.SetBinding(Xamarin.Forms.Label.TextProperty, "ContactName");
			labelContactName.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
			labelContactName.TextColor = asbestos;

            View = new StackLayout()
            {
                Children =
                {

                    labelContactCode,
                    labelContactName
                }
            };
        }
    }

	public class CustomerDetailsPage : ContentPage
	{
        CustomerDetailsPageViewModel _vm;
        Xamarin.Forms.Label _labelTitle;
        ListView _listViewContacts;

        public CustomerDetailsPage(App_Customer customer, App_WorkTicket workTicket)
        {
            _vm = new CustomerDetailsPageViewModel(customer, workTicket);

            SetPageDisplay();
        }

        /*public CustomerDetailsPage()
        {
            _vm = new CustomerDetailsPageViewModel();

            SetPageDisplay();
        }*/

        protected void SetPageDisplay()
        {
            // Set the page title.
            Title = "Customer Details";
			Color asbestos = Color.FromHex("#7f8c8d");

			BackgroundColor = Color.White;
            //  Create a label for the technician list
            _labelTitle = new Xamarin.Forms.Label();
            _labelTitle.Text = "CUSTOMER DETAILS";
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

            // create a stack layout to hold all the customer data
            Xamarin.Forms.StackLayout stackLayoutCustomerData = new StackLayout();
			stackLayoutCustomerData.Padding = 30;

            Grid topGrid = new Grid();
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150, GridUnitType.Absolute) });
            topGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            // add customer number
            Xamarin.Forms.Label labelCustomerNumber = new Xamarin.Forms.Label();
            labelCustomerNumber.Text = _vm.FormattedCustomerNumber;
			labelCustomerNumber.TextColor = asbestos;
			labelCustomerNumber.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
            //stackLayoutCustomerData.Children.Add(labelCustomerNumber);

            topGrid.Children.Add(new Xamarin.Forms.Label
            {
                Text = "Customer No",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 0);
            topGrid.Children.Add(labelCustomerNumber, 1, 0);

            // add customer name
            Xamarin.Forms.Label labelCustomerName = new Xamarin.Forms.Label();
            labelCustomerName.Text = _vm.CustomerName;
			labelCustomerName.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
			labelCustomerName.TextColor = asbestos;
            //stackLayoutCustomerData.Children.Add(labelCustomerName);

            topGrid.Children.Add(new Xamarin.Forms.Label
            {
                Text = "Customer Name",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 1);
            topGrid.Children.Add(labelCustomerName, 1, 1);

            // add address line 1
            Xamarin.Forms.Label labelAddressLine1 = new Xamarin.Forms.Label();
            labelAddressLine1.Text = _vm.AddressLine1;
			labelAddressLine1.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
			labelAddressLine1.TextColor = asbestos;
            //stackLayoutCustomerData.Children.Add(labelAddressLine1);

            topGrid.Children.Add(new Xamarin.Forms.Label
            {
                Text = "Address",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 2);
            topGrid.Children.Add(labelAddressLine1, 1, 2);

            // add address line 2 if it exists
            if (_vm.AddressLine2 != null)
            {
                Xamarin.Forms.Label labelAddressLine2 = new Xamarin.Forms.Label();
                labelAddressLine2.Text = _vm.AddressLine2;
				labelAddressLine2.TextColor = asbestos;
				labelAddressLine2.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
                //stackLayoutCustomerData.Children.Add(labelAddressLine2);
                topGrid.Children.Add(labelAddressLine2, 1, 3);
            }

            // add address line 3 if it exists
            if (_vm.AddressLine3 != null)
            {
                Xamarin.Forms.Label labelAddressLine3 = new Xamarin.Forms.Label();
                labelAddressLine3.Text = _vm.AddressLine3;
				labelAddressLine3.TextColor = asbestos;
				labelAddressLine3.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
                //stackLayoutCustomerData.Children.Add(labelAddressLine3);
                topGrid.Children.Add(labelAddressLine3, 1, 4);
            }

            // add city/state/zip
            Xamarin.Forms.Label labelCityStateZip = new Xamarin.Forms.Label();
            labelCityStateZip.Text = _vm.City + ", " + _vm.State + "  " + _vm.ZipCode;
			labelCityStateZip.TextColor = asbestos;
			labelCityStateZip.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
            //stackLayoutCustomerData.Children.Add(labelCityStateZip);
            topGrid.Children.Add(new Xamarin.Forms.Label
            {
                Text = "City/State/Zip",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 5);
            topGrid.Children.Add(labelCityStateZip, 1, 5);

            // add phone number
            Xamarin.Forms.Label labelPhoneNumber = new Xamarin.Forms.Label();
            labelPhoneNumber.Text = _vm.TelephoneNo;
            if (_vm.TelephoneExt != null)
            {
                labelPhoneNumber.Text = labelPhoneNumber.Text + " ext. " + _vm.TelephoneExt;
            }
			labelPhoneNumber.TextColor = asbestos;
			labelPhoneNumber.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
            //stackLayoutCustomerData.Children.Add(labelPhoneNumber);
            topGrid.Children.Add(new Xamarin.Forms.Label
            {
                Text = "Phone",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 6);
            topGrid.Children.Add(labelPhoneNumber, 1, 6);

            // add contact
            string contactCodeName = "";
            foreach(App_CustomerContact cont in _vm.CustomerContacts)
            {
                contactCodeName = string.Format("{0} - {1}", cont.ContactCode, cont.ContactName);
            }

            Xamarin.Forms.Label labelContact = new Xamarin.Forms.Label();
            labelContact.Text = contactCodeName;
            labelContact.TextColor = asbestos;
            labelContact.FontFamily = Device.OnPlatform("OpenSans-Regular","sans-serif", null);
            topGrid.Children.Add(new Xamarin.Forms.Label
            {
                Text = "Contact",
                FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
                TextColor = asbestos
            }, 0, 7);
            topGrid.Children.Add(labelContact, 1, 7);

            stackLayoutCustomerData.Children.Add(topGrid);

            // Add a button to show the map
            Xamarin.Forms.Button buttonShowMap = new Xamarin.Forms.Button();
            buttonShowMap.Text = "MAP";
			buttonShowMap.TextColor = Color.White;
			buttonShowMap.BackgroundColor = asbestos;
			buttonShowMap.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
            buttonShowMap.Image = "map-marker-64.PNG";
			buttonShowMap.HeightRequest = 80;
            buttonShowMap.Clicked += ButtonShowMap_Clicked;
            stackLayoutCustomerData.Children.Add(buttonShowMap);

            // Create the actual list for contacts

            // Create a template to display each contact in the list
            //stackLayoutCustomerData.Children.Add(
            //    new Xamarin.Forms.Label()
            //    {
            //        Text = "Contacts",
            //        FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null),
            //        HorizontalTextAlignment = TextAlignment.Center,
            //        BackgroundColor = asbestos,
            //        TextColor = Color.White
            //    });

            //var dataTemplateCustomerContact = new DataTemplate(typeof(CustomerContactViewCell));

            //_listViewContacts = new ListView()
            //{
            //    HasUnevenRows = true,
            //    HorizontalOptions = LayoutOptions.Fill,
            //    SeparatorVisibility = SeparatorVisibility.None,
            //    BackgroundColor = Color.White,

            //    ItemsSource = _vm.CustomerContacts,
            //    ItemTemplate = dataTemplateCustomerContact
            //};
            //_listViewContacts.BindingContext = _vm.CustomerContacts;
            //stackLayoutCustomerData.Children.Add(_listViewContacts);

            // On to customer contacts!
            // Create a template to display each customer contact in a list
            //var dataTemplateCustomerContact = new DataTemplate(typeof(CustomerContactViewCell));

            // Create the actual list
            //        Xamarin.Forms.ListView listViewCustomerContacts = new ListView()
            //        {
            //            HasUnevenRows = true,
            //            ItemsSource = _vm.CustomerContacts,
            //            ItemTemplate = dataTemplateCustomerContact,
            //SeparatorColor = asbestos
            //        };
            //        listViewCustomerContacts.BindingContext = _vm.CustomerContacts;

            // put it all together
            Content = new StackLayout()
            {
                Padding = 30,
                Children =
                {
                    titleLayout,
                    stackLayoutCustomerData,
                    //new Xamarin.Forms.Label() { Text = "Contacts", FontFamily = Device.OnPlatform("OpenSans-Bold",null,null), TextColor = asbestos },
                    //listViewCustomerContacts
                }
            };
        }

        protected async void ButtonShowMap_Clicked(object sender, EventArgs e)
        {
            // puke
            //await Navigation.PushAsync(new CustomerMapPage());
            Geocoder geoCoder = new Geocoder();
            StringBuilder customerAddress = new StringBuilder();
            Pin pin = new Pin();

            customerAddress.Append(_vm.AddressLine1);
            customerAddress.Append(", ");
            customerAddress.Append(_vm.City);
            customerAddress.Append(", ");
            customerAddress.Append(_vm.State);
            customerAddress.Append(" ");
            customerAddress.Append(_vm.ZipCode);

            try
            {
                var location = await geoCoder.GetPositionsForAddressAsync(customerAddress.ToString());
                //var address = await geoCoder.GetAddressesForPositionAsync(new Position(39.909606, -76.299061));
                List<Position> approximateLocation = location.ToList();
                pin.Position = approximateLocation[0];
                pin.Label = _vm.CustomerName;
                pin.Address = customerAddress.ToString();

                await Navigation.PushAsync(new CustomerMapPage(pin));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                await DisplayAlert("Invalid Location", "Address cannot be mapped.", "OK");
                return;
            }
        }

    }
}
