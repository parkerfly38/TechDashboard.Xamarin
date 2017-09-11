using System;
using System.Collections.Generic;
using TechDashboard.Models;
using TechDashboard.Views;
using Xamarin.Forms;

namespace TechDashboard
{
    public partial class PartEditExtdDescPage : ContentPage
    {
        PartsEditExtdDescPageViewModel _vm;
        App_WorkTicket _workTicket;
        App_RepairPart _part;
        PartsEditPage.PageMode _pageMode;
        App_ScheduledAppointment _scheduledAppointment;

        public PartEditExtdDescPage(App_WorkTicket workTicket, App_RepairPart part, PartsEditPage.PageMode pageMode,
            App_ScheduledAppointment scheduledAppointment)
        {
            _workTicket = workTicket;
            _part = part;
            _pageMode = pageMode;
            _scheduledAppointment = scheduledAppointment;

            _vm = new PartsEditExtdDescPageViewModel(part, workTicket);

            InitializeComponent();
            labelTitle.FontFamily = Device.OnPlatform("OpenSans-Bold","sans-serif-black", null);
            labelItemCode.Text = _part.PartItemCode;
            labelItemCodeDesc.Text = _part.PartItemCodeDescription;
            if (_part.ItemCodeDesc != null || _part.ItemCodeDesc.Trim().Length == 0)
            {
                CI_Item itm = App.Database.GetItemFromDB(_part.PartItemCode);
                if (itm.ExtendedDescriptionKey != null && itm.ExtendedDescriptionKey > 0)
                {
                    CI_ExtendedDescription itmExtdDsc = App.Database.GetExtendedDescription(itm.ExtendedDescriptionKey);
                }
            }
            else
            {
                entryExtdDesc.Text = _part.PartItemCodeDescription;
            }
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            _vm.UpdateExtdDesc(entryExtdDesc.Text);
            Navigation.PopAsync();
        }

        void Cancel_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
