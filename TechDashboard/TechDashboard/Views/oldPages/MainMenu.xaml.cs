using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TechDashboard.Views
{
    public partial class MainMenu : ContentPage
    {

        private string[] _items;
        public string[] Items
        {
            get { return _items; }
        }

        public MainMenu()
        {
            _items = new string[] { "Schedule", "Misc Time", "Arrive", "Parts List", "Notes" }; // puke... how to expose values to list?

            InitializeComponent();

            lvMainMenu.ItemsSource = _items;
        }

        private void lvMainMenu_OnItemTapped(object o, ItemTappedEventArgs e)
        {
            string item = e.Item as string;
            DisplayAlert("Aha!", String.Format("You selected {0}.", item), "OK");

            switch(item.ToLower())
            {
                
                default:
                    break;
            }
        }
    }
}
