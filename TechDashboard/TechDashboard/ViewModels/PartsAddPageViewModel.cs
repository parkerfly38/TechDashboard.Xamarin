using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using TechDashboard.Models;

namespace TechDashboard.ViewModels
{
    public class PartsAddPageViewModel
    {
        ObservableCollection<App_Item> _itemList;
        public ObservableCollection<App_Item> ItemList
        {
            get { return _itemList; }
        }

        App_WorkTicket _workTicket;
        public App_WorkTicket WorkTicket
        {
            get { return _workTicket; }
        }

        public PartsAddPageViewModel(App_WorkTicket workTicket)
        {
            _workTicket = workTicket;
            _itemList = new ObservableCollection<App_Item>(App.Database.GetItems());
        }

        public void FilterItemList(string filterText)
        {
			try {
				_itemList.Clear();
				/*foreach(App_Item newItem in App.Database.GetItems(filterText, filterText))
				{
					_itemList.Add(newItem);
				}*/
				string whseFilter = "";
				if(App.CurrentTechnician.DefaultWarehouse != null && App.CurrentTechnician.DefaultWarehouse.Trim().Length > 0) {
					whseFilter = App.CurrentTechnician.DefaultWarehouse;
				}
				foreach(App_Item newItem in App.Database.GetItems(filterText, filterText, whseFilter)) {
					_itemList.Add(newItem);
				}
			} catch(Exception exception) {
				App.sendException(exception, "TechDashboard.PartsAddPageViewModel.FilterItemList");
			}
        }
    }
}
