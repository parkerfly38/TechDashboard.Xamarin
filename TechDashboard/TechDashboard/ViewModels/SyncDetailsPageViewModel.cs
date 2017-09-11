using System;
using System.Collections.Generic;
using TechDashboard.Models;

namespace TechDashboard.ViewModels
{
    public class SyncDetailsPageViewModel
    {
        private List<JT_TransactionImportDetail> _transactionImportDetails;
        public List<JT_TransactionImportDetail> TransactionImportDetails
        {
            get {
                return _transactionImportDetails;
            }
            set {
                _transactionImportDetails = value;
            }
        }

        public SyncDetailsPageViewModel()
        {
            _transactionImportDetails = App.Database.GetCurrentExport();
        }
    }
}
