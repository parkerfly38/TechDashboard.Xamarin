using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TechDashboard.Views
{
	public partial class MainDashboard : MasterDetailPage
	{
		public MainDashboard ()
		{
			InitializeComponent ();

            this.Master = new MainMenu();
            this.Detail = new NavigationPage(new TestDetailPage());
		}

        public void lvMainMenu_OnItemTapped(object o, ItemTappedEventArgs e)
        {
            DisplayAlert("Aha!", "You tapped something!", "OK");

            //App.Technician = e.Item as Models.JT_Technician;

            //if (App.Technician == null)
            //{
            //    DisplayAlert("Select Technician!", "You need to select a technician!", "OK");
            //    return;
            //}

            
            Navigation.PushAsync(new TestDetailPage());
            // puke
            //var technician = e.Item as Models.JT_Technician;
            //if (technician != null)
            //{
            //    DisplayAlert("Aha!", String.Format("You selected {0} {1}", technician.FirstName, technician.LastName), "OK");
            //    _vm.SetTechnicianForApplication(technician);

            //    Navigation.PushAsync(new MainDashboard());
            //}
        }
    }
}
