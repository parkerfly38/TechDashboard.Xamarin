using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using TechDashboard.Models;

namespace TechDashboard.ViewModels
{
    public class PartsEditPageViewModel
    {
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

        protected List<string> _warehouseList;
        public List<string> WarehouseList
        {
            get { return _warehouseList; }
        }

        // dch rkl 01/23/2017 add unit of measure list
        protected List<CI_UnitOfMeasure> _unitOfMeasureList;
        public List<CI_UnitOfMeasure> UnitOfMeasureList
        {
            get { return _unitOfMeasureList; }
        }

        protected string _qtyAvailable;
        public string QtyAvailable
        {
            get { return _qtyAvailable; }
            set {
                _qtyAvailable = value;
            }
        }

        // DCH 01/13/2017 Add the ExtendedDescriptionKey and ExtendedDescriptionText BEGIN
        protected int _extendedDescriptionKey;
        public int ExtendedDescriptionKey
        {
            get { return _extendedDescriptionKey; }
        }
        protected string _extendedDescriptionText;
        public string ExtendedDescriptionText
        {
            get { return _extendedDescriptionText; }
        }
        // DCH 01/13/2017 Add the ExtendedDescriptionKey and ExtendedDescriptionText END

        public PartsEditPageViewModel(App_WorkTicket workTicket, CI_Item partToEdit)
        {
            _workTicket = workTicket;
            //_partToEdit = partToEdit;

            //_warehouseList = GetTechnicianWarehouses();
            _warehouseList = new List<string>();
            List<IM_Warehouse> lsItmWhse = GetWarehouses();
            foreach (IM_Warehouse itemWhse in lsItmWhse) {
                _warehouseList.Add(string.Format("{0} - {1}", itemWhse.WarehouseCode, itemWhse.WarehouseDesc));
            }

            // dch rkl 01/23/2017 added Unit of Measure List
            _unitOfMeasureList = App.Database.GetCI_UnitOfMeasureFromDB();
            _unitOfMeasureList.Add(new CI_UnitOfMeasure() { UnitOfMeasure = "EACH" });
            _unitOfMeasureList.Sort((x, y) => x.UnitOfMeasure.CompareTo(y.UnitOfMeasure));

            // DCH 01/13/2017 Add the ExtendedDescriptionKey and ExtendedDescriptionText BEGIN
            _extendedDescriptionKey = partToEdit.ExtendedDescriptionKey;
            _extendedDescriptionText = "";
            if (partToEdit.ExtendedDescriptionKey > 0) {
                CI_ExtendedDescription extdDesc = App.Database.GetExtendedDescription(partToEdit.ExtendedDescriptionKey);
                if (extdDesc.ExtendedDescriptionText != null) { _extendedDescriptionText = extdDesc.ExtendedDescriptionText; }
            }
            // DCH 01/13/2017 Add the ExtendedDescriptionKey and ExtendedDescriptionText END
        }

        public PartsEditPageViewModel(App_WorkTicket workTicket, App_RepairPart partToEdit)
        {
            _workTicket = workTicket;
            _partToEdit = partToEdit;
            _warehouseList = new List<string>();
            List<IM_Warehouse> lsItmWhse = GetWarehouses();
            foreach (IM_Warehouse itemWhse in lsItmWhse) {
                _warehouseList.Add(string.Format("{0} - {1}", itemWhse.WarehouseCode, itemWhse.WarehouseDesc));
            }

            // dch rkl 01/23/2017 added Unit of Measure List
            _unitOfMeasureList = App.Database.GetCI_UnitOfMeasureFromDB();
            _unitOfMeasureList.Add(new CI_UnitOfMeasure() { UnitOfMeasure = "EACH" });
            _unitOfMeasureList.Sort((x, y) => x.UnitOfMeasure.CompareTo(y.UnitOfMeasure));
        }

        // dch rkl 12/01/2016 Get Warehouse List from IM_Warehouse instead of IM_ItemWarehouse
        protected List<IM_Warehouse> GetWarehouses()
        {
            // dch rkl 12/07/2016 catch exception
            //List<IM_Warehouse> warehouseList = App.Database.GetIMWarehouseFromDB();
            List<IM_Warehouse> warehouseList = new List<IM_Warehouse>();
            try {
                warehouseList = App.Database.GetIMWarehouseFromDB();
            } catch (Exception ex) {
                // dch rkl 12/07/2016 Log Error
                App.sendException(ex, "TechDashboard.PartsEditPageViewModel.GetWarehouses");
            }

            return warehouseList;
        }

        public void AddPartToPartsList()
        {
            // App.Database.AddItemToPartsList(_workTicket, _partToEdit);
            App.Database.SaveRepairPart(_partToEdit, _workTicket, App.CurrentTechnician);
        }

        public void UpdatePartOnPartsList()
        {
            App.Database.SaveRepairPart(_partToEdit, _workTicket, App.CurrentTechnician);
            //App.Database.UpdatePartOnPartsList(_partToEdit);
        }

        public void DeletePartFromPartsList()
        {
            App.Database.DeleteRepairPart(_partToEdit);
        }

        protected List<string> GetTechnicianWarehouses()
        {
            App_Technician technician = App.CurrentTechnician;
            List<string> warehouseList = App.Database.GetTechnicianWarehouses();

            if((PartToEdit.Warehouse != null) &&
               (!warehouseList.Contains(PartToEdit.Warehouse)))
            {
                warehouseList.Insert(0, PartToEdit.Warehouse);
            }
            if ((technician.DefaultWarehouse != null) &&
                (!warehouseList.Contains(technician.DefaultWarehouse)))
            {
                warehouseList.Insert(0, technician.DefaultWarehouse);
            }

            return warehouseList;
        }

        protected List<IM_ItemWarehouse> GetItemWarehouses(string itemCode)
        {
            List<IM_ItemWarehouse> warehouseList = App.Database.GetWarehousesForItem(itemCode);

            return warehouseList;
        }

        public List<Data.LotQavl> GetMfgSerialNumbersForPart()
        {
            // dch rkl 12/07/2016 catch exception
            List<Data.LotQavl> serialNumberList = new List<Data.LotQavl>();
            try
            {
                // dch rkl 11/27/2016 Include Qty Available
                //List<string> serialNumberList = App.Database.GetMfgSerialNumbersForItem(_partToEdit.PartItemCode, PartToEdit.Warehouse, _workTicket.SalesOrderNo, _workTicket.WTNumber, _workTicket.WTStep);
                serialNumberList = App.Database.GetMfgSerialNumbersForItem(_partToEdit.PartItemCode, PartToEdit.Warehouse, _workTicket.SalesOrderNo, _workTicket.WTNumber, _workTicket.WTStep);
            }
            catch (Exception ex)
            {
                // dch rkl 12/07/2016 Log Error
                App.sendException(ex, "TechDashboard.PartsEditPageViewModel.GetMfgSerialNumbersForPart");
            }

            return serialNumberList;
        }
    }
}
