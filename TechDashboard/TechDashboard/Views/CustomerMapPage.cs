using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;


using TechDashboard.Models;
using TechDashboard.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TechDashboard.Views
{
    public class CustomerMapPage : ContentPage
    {
        protected CustomerMapPageViewModel _vm;
        protected Map _map;

        public CustomerMapPage(Pin customerLocationPin)
        {
            _vm = new CustomerMapPageViewModel(customerLocationPin);
            Initialize();
        }

        public CustomerMapPage()
        {
            _vm = new CustomerMapPageViewModel();
            Initialize();
        }

        protected void Initialize()
        {
            // Set the page title.
            Title = "Customer Map";

			BackgroundColor = Color.White;
            // puke
            // Create the map
            _map = new Map()
            { 
                IsShowingUser = true,
                MapType = MapType.Street,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            // Add the customer location pin 
            _map.Pins.Add(_vm.CustomerLocationPin);
            _vm.CustomerLocationPin.Clicked += CustomerLocationPin_Clicked;
            // center on the new pin
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(_vm.CustomerLocationPin.Position, Distance.FromMiles (0.5)));

            // add to screen contents for drawing
            Content = new StackLayout
            {
                Spacing = 0,
                Children =
                {
                    _map
                }
            };
        }  

        /// <summary>
        /// Click event for the customer location pin on the map.  This event
        /// will open the native map and provide directions from the user's 
        /// current location to the address represented by the pin.
        /// </summary>
        /// <param name="sender">Pin representing destination address</param>
        /// <param name="e">Empty event args (not used)</param>
        protected void CustomerLocationPin_Clicked(object sender, EventArgs e)
        {
            string address = (sender as Pin).Address;

            switch (Device.OS)
            {
                case TargetPlatform.iOS:
                    Device.OpenUri(
                      new Uri(string.Format("http://maps.apple.com/?q={0}", WebUtility.UrlEncode(address))));
                    break;
                case TargetPlatform.Android:
                    Device.OpenUri(
                      new Uri(string.Format("geo:0,0?q={0}", WebUtility.UrlEncode(address))));
                    break;
                case TargetPlatform.Windows:
                case TargetPlatform.WinPhone:
                    Device.OpenUri(
                      new Uri(string.Format("bingmaps:?where={0}", Uri.EscapeDataString(address))));
                    break;
            }
        }
    }
}
