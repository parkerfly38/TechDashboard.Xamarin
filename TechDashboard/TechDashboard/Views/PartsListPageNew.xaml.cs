using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TechDashboard.Models;
using TechDashboard.ViewModels;
using TechDashboard.Views;
using Xamarin.Forms;

namespace TechDashboard
{
    public partial class PartsListPageNew : ContentPage
    {

        PartsListPageViewModel _vm;
        App_ScheduledAppointment _scheduledAppointment;

        public PartsListPageNew(App_WorkTicket workTicket, App_ScheduledAppointment scheduledAppointment)
        {
            _vm = new PartsListPageViewModel(workTicket);
            _scheduledAppointment = scheduledAppointment;
            InitializeComponent();
            labelTitle.FontFamily = Device.OnPlatform("OpenSans-Bold", "sans-serif-black", null);
            BindingContext = _vm;
            if (workTicket.DtlWarrantyRepair == "Y") {
                switchWarrantyRepair.IsToggled = true;
            } else {
                switchWarrantyRepair.IsToggled = false;
            }

            // Parts Coverred on Warranty
            bool bIsChkd = false;
            if (workTicket.StatusDate != null && workTicket.RepairItem.MfgPartsWarrantyPeriod != null) {
                TimeSpan tsDateDiff = workTicket.RepairItem.MfgPartsWarrantyPeriod.Subtract(workTicket.StatusDate);
                if (tsDateDiff.TotalDays > 0 && workTicket.DtlWarrantyRepair == "Y") {
                    switchPartsCoveredOnWarranty.IsToggled = true;
                    bIsChkd = true;
                } else {
                    switchPartsCoveredOnWarranty.IsToggled = false;
                }
            }
            if (workTicket.StatusDate != null && workTicket.RepairItem.MfgPartsWarrantyPeriod != null) {
                TimeSpan tsDateDiff = workTicket.RepairItem.MfgPartsWarrantyPeriod.Subtract(workTicket.StatusDate);
                if (tsDateDiff.TotalDays > 0 && workTicket.DtlWarrantyRepair == "Y") {
                    switchPartsCoveredOnWarranty.IsToggled = true;
                    bIsChkd = true;
                } else {
                    switchPartsCoveredOnWarranty.IsToggled = false;
                }
            }

                    // Service Agreement Repair
                    if (workTicket.DtlCoveredOnContract == "Y") { switchSvcAgmtRepair.IsToggled = true; } else { switchSvcAgmtRepair.IsToggled = false; }

                    // Parts Covered on Service Agreement
                    if (workTicket.IsPreventativeMaintenance && workTicket.ServiceAgreement.ArePreventativeMaintenancePartsCovered) {
                        switchPartsCoveredonSvcAgmt.IsToggled = true;
                    } else if (workTicket.IsPreventativeMaintenance == false && workTicket.IsServiceAgreementRepair
                          && workTicket.ServiceAgreement.ArePartsCovered) {
                        switchPartsCoveredonSvcAgmt.IsToggled = true;
                    } else {
                        switchPartsCoveredonSvcAgmt.IsToggled = false;
                    }

        }

        async void Handle_RowTap(object sender, DevExpress.Mobile.DataGrid.RowTapEventArgs e)
        {
            int rowindex = grid.GetSourceRowIndex(e.RowHandle);
            App_RepairPart part = ((ObservableCollection<App_RepairPart>)grid.ItemsSource)[rowindex];

            await Navigation.PushAsync(new PartsEditPage(_vm.WorkTicket, part, PartsEditPage.PageMode.Edit, _scheduledAppointment));
        }
    }
}
