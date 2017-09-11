using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TechDashboard.Data;
using TechDashboard.Models;

namespace TechDashboard
{
    public class PartsEditExtdDescPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        App_WorkTicket _workTicket;
        public App_WorkTicket WorkTicket
        {
            get { return _workTicket; }
        }

        App_RepairPart _partToEdit;
        public App_RepairPart PartToEdit
        {
            get { return _partToEdit; }
        }

        public PartsEditExtdDescPageViewModel(App_RepairPart partToEdit, App_WorkTicket workTicket)
        {
            try {
                _partToEdit = partToEdit;
                _workTicket = workTicket;
            } catch (Exception ex) {
                // dch rkl 12/07/2016 Log Error
                App.sendException(ex, "TechDashboard.PartsEditExtdDescPageViewModel(App_RepairPart partToEdit, App_WorkTicket workTicket)");
            }
        }

        public void UpdateExtdDesc(string extdDescr)
        {
            // Save the Description
            try {
                App.Database.SaveRepairPartDesc(_partToEdit, _workTicket, App.CurrentTechnician, extdDescr);
            } catch (Exception ex) {
                // dch rkl 12/07/2016 Log Error
                App.sendException(ex, "TechDashboard.PartsEditExtdDescPageViewModel.UpdatePartOnPartsList");
            }
        }
    }
}
