using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using TechDashboard.Models;

namespace TechDashboard.ViewModels
{
    public class PartsListPageViewModel
    {
        CI_Options _ciOptions;

        public string quantityFormatString { get; set; }

        public string umFormatString { get; set; }

        public string costFormatString { get; set; }

        public string priceFormatString { get; set; }

        protected App_WorkTicket _workTicket;
        public App_WorkTicket WorkTicket
        {
            get { return _workTicket; }
        }

        
        protected List<App_RepairPart> _partsList;
        public List<App_RepairPart> PartsList
        {
            get { return _partsList; }
        }

        protected ObservableCollection<App_RepairPart> _observablePartsList;
        public ObservableCollection<App_RepairPart> ObservablePartsList
        {
            get { return _observablePartsList; }
        }

        public PartsListPageViewModel(App_WorkTicket workTicket)
        {
            try {
                _ciOptions = App.Database.GetCIOptions();
                quantityFormatString = String.Concat("{0:F", _ciOptions.NumberOfDecimalPlacesInQty, "}");
                umFormatString = string.Concat("{0:F", _ciOptions.NumberOfDecimalPlacesInUM, "}");
                costFormatString = string.Concat("{0:F", _ciOptions.NumberOfDecimalPlacesInCost, "}");
                priceFormatString = string.Concat("{0:F", _ciOptions.NumberOfDecimalPlacesInPrice, "}");

                _workTicket = workTicket;
                _partsList = App.Database.RetrievePartsListFromWorkTicket(workTicket);
                _observablePartsList = new ObservableCollection<App_RepairPart>();
                SetPartsList();
            } catch (Exception exception) {
                App.sendException(exception, "PartsListPageViewModel.PartsListPageViewModel");
            }

        }

        protected void SetPartsList()
        {
            try {
                List<App_RepairPart> Initialparts = App.Database.RetrievePartsListFromWorkTicket(_workTicket);
                List<App_RepairPart> defaultparts = App.Database.GetDefaultPartsFromWorkTicket(_workTicket, Initialparts);

                var parts = defaultparts.Union(Initialparts);

                _observablePartsList.Clear();

                if (parts != null) {
                    foreach (App_RepairPart part in parts) {
                        _observablePartsList.Add(part);
                    }
                }
            } catch (Exception exception) {
                App.sendException(exception, "PartsListPageViewModel.SetPartsList()");
            }
        }

        //public PartsListPageViewModel()
        //{
        //    _partsList = App.Database.RetrievePartsListFromCurrentWorkTicket();
        //}
    }
}
