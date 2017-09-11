using System;
using System.Collections.Generic;
using Rkl.Erp.Sage.Sage100.TableObjects;
using TechDashboard.ViewModels;
using Xamarin.Forms;

namespace TechDashboard
{
    public partial class SyncDetails : ContentPage
    {
        SyncDetailsPageViewModel _vm;

        public SyncDetails()
        {
            _vm = new SyncDetailsPageViewModel();
            this.BindingContext = _vm;
            //TIDList.ItemsSource = _vm.TransactionImportDetails;

            InitializeComponent();
        }
    }
}
