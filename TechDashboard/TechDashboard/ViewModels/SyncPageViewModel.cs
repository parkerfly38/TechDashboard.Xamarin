using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechDashboard.Data;
using TechDashboard.Models;

namespace TechDashboard.ViewModels
{
    public class SyncPageViewModel : INotifyPropertyChanged
    {
        private List<JT_TransactionImportDetail> _transactionImportDetails;

        public List<JT_TransactionImportDetail> transactionImportDetails
        {
            get { return _transactionImportDetails; }
            set { _transactionImportDetails = value; }
        }

        public int UpdateCount
        {
            get { return _transactionImportDetails.Count; }
        }

        public SyncPageViewModel()
        {
            _transactionImportDetails = App.Database.GetCurrentExport();
        }

        public void syncWithServer(ref int syncSuccess, ref int syncFailed)
        {
            // dch rkl 12/07/2016 catch exception
            try
            {
                // dch rkl 12/09/2016 return number failed and number successful
                syncSuccess = 0;
                syncFailed = 0;

                TechDashboard.Data.RestClient restClient = new Data.RestClient(App.Database.GetApplicationSettings().IsUsingHttps, App.Database.GetApplicationSettings().RestServiceUrl);

                foreach (JT_TransactionImportDetail transaction in _transactionImportDetails)
                {
                    // dch rkl 12/05/2016 If Lot/Serial Nbr Data, sync back to JobOps with multiple rows
                    //bool updateWorked = restClient.InsertTransactionImportDetailRecordSync(transaction);
                    bool updateWorked = true;
                    if (transaction.LotSerialNo == null || transaction.LotSerialNo.Trim().Length == 0)
                    {
                        // dch rkl 12/09/2016 This now returns a results object
                        //updateWorked = restClient.InsertTransactionImportDetailRecordSync(transaction);
                        updateWorked = restClient.InsertTransactionImportDetailRecordSync(transaction).Success;
                    }
                    else
                    {
                        // Split into LotSerNo/Qty strings
                        string[] lotSerQty = transaction.LotSerialNo.Split('|');
                        double qty = 0;

                        foreach (string lsq in lotSerQty)
                        {
                            // Split each LotSerNo/Qty string into LotSerNo and Qty
                            string[] sqty = lsq.Split('~');
                            if (sqty.GetUpperBound(0) > 0)
                            {
                                double.TryParse(sqty[1], out qty);
                                if (qty > 0)
                                {
                                    transaction.QuantityUsed = qty;
                                    transaction.LotSerialNo = sqty[0];
                                    // dch rkl 12/09/2016 This now returns a results object
                                    //bool updateWorkedLS = restClient.InsertTransactionImportDetailRecordSync(transaction);
                                    bool updateWorkedLS =
                                        restClient.InsertTransactionImportDetailRecordSync(transaction).Success;
                                    if (updateWorkedLS == false)
                                    {
                                        updateWorked = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (updateWorked)
                    {
                        App.Database.DeleteExportRow(transaction);

                        // dch rkl 12/09/2016 return number failed and number successful
                        syncSuccess++;
                    }
                    // dch rkl 12/09/2016 return number failed and number successful
                    else
                    {
                        syncFailed++;
                    }
                }

                _transactionImportDetails = App.Database.GetCurrentExport();
                PropertyChanged(this, new PropertyChangedEventArgs("UpdateCount"));
                PropertyChanged(this, new PropertyChangedEventArgs("transactionImportDetails"));

                JT_Technician technician = App.Database.GetCurrentTechnicianFromDb();

                var techUpdateWorked = restClient.UpdateTechnicianRecordSync(technician);

                PropertyChanged(this, new PropertyChangedEventArgs("UpdateCount"));
                PropertyChanged(this, new PropertyChangedEventArgs("transactionImportDetails"));

                App_Settings appSettings = App.Database.GetApplicationSettings();
                appSettings.LastSyncDate = DateTime.Now.ToString();
                App.Database.SaveAppSettings(appSettings);
            }
            catch (Exception ex)
            {
                // dch rkl 12/07/2016 Log Error
                //ErrorReporting errorReporting = new ErrorReporting();
                //errorReporting.sendException(ex, "TechDashboard.SyncPageViewModel.syncWithServer");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
