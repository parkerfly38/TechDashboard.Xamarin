using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

using TechDashboard.ViewModels;

namespace TechDashboard.Views
{
    public partial class TechnicianPage : ContentPage
    {
        TechnicianPageViewModel _vm;
        App _application;

        public TechnicianPage(TechnicianPageViewModel viewModel)
        {
            InitializeComponent();

            _vm = viewModel;
            BindingContext = _vm.TechnicianGroup;
        }

        public TechnicianPage(App application)
        {
            InitializeComponent();

            _vm = new TechnicianPageViewModel();
            BindingContext = _vm.TechnicianGroup;
        }

        public void lvTechniciansList_OnItemTapped(object o, ItemTappedEventArgs e)
        {
            // puke
            var technician = e.Item as Models.JT_Technician;
            if (technician != null)
            {
                DisplayAlert("Aha!", String.Format("You selected {0} {1}", technician.FirstName, technician.LastName), "OK");
                _vm.SetTechnicianForApplication(technician);

                Navigation.PushAsync(new MainDashboard());
            }
        }
    }
}
