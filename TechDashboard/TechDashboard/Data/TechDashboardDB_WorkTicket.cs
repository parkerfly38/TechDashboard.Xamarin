﻿using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using System.Linq;
using TechDashboard.Models;
using Xamarin.Forms;

namespace TechDashboard.Data
{
    public partial class TechDashboardDatabase
    {
        #region App_WorkTicket

        public App_WorkTicket GetWorkTicket(App_ScheduledAppointment scheduledAppointment)
        {
            JT_WorkTicket workTicket = null;
            JT_WorkTicket workTicketStepZero = null;
            JT_WorkTicketClass workTicketClass = null;
            CI_Item repairItem = null;
            JT_EquipmentAsset equipmentAsset = null;
            App_ServiceAgreement serviceAgreement = null;
            JT_ClassificationCode problemCode = null;
            JT_ClassificationCode exceptionCode = null;


            lock (_locker)
            {
                if (scheduledAppointment != null)
                {
                    workTicket =
                        _database.Table<JT_WorkTicket>().Where(
                            wt => (wt.SalesOrderNo == scheduledAppointment.SalesOrderNumber) &&
                                  (wt.WTNumber == scheduledAppointment.WorkTicketNumber) &&
                                  (wt.WTStep == scheduledAppointment.WorkTicketStep)
                        ).FirstOrDefault();

                    // dch rkl 11/30/2016 If JT_WorkTicket.HdrWTClass is null, populate it from SO_SalesOrderHeader.JT158_WTClass
                    if (workTicket.HdrWtClass == null)
                    {
                        SO_SalesOrderHeader soHeader = _database.Table<SO_SalesOrderHeader>().Where(
                            soh => (soh.SalesOrderNo == workTicket.SalesOrderNo)).FirstOrDefault();
                        if (soHeader != null) { workTicket.HdrWtClass = soHeader.JT158_WTClass; }
                    }

                    workTicketStepZero =
                        _database.Table<JT_WorkTicket>().Where(
                            wt => (wt.SalesOrderNo == scheduledAppointment.SalesOrderNumber) &&
                                  (wt.WTNumber == scheduledAppointment.WorkTicketNumber) &&
                                  (wt.WTStep == "000")
                        ).FirstOrDefault();

                    workTicketClass = 
                        _database.Table<JT_WorkTicketClass>().Where(
                            wtc => (workTicket.HdrWtClass == wtc.WorkTicketClass)
                        ).FirstOrDefault();

                    repairItem = GetItemFromDB(workTicket.DtlRepairItemCode);

                    try {

                        if (workTicket.DtlMfgSerialNo != null) {
                            equipmentAsset =
                            _database.Table<JT_EquipmentAsset>().Where(
                                ea => (ea.ItemCode == repairItem.ItemCode) &&
                                (ea.MfgSerialNo == workTicket.DtlMfgSerialNo)
                            ).FirstOrDefault();
                        } else {
                            equipmentAsset = new JT_EquipmentAsset();
                        }
                    } catch (Exception exception) {
                        equipmentAsset = new JT_EquipmentAsset();
                    }

                    serviceAgreement = GetServiceAgreement(workTicketStepZero);

                    problemCode = _database.Table<JT_ClassificationCode>().Where(
                       pc => (pc.ClassificationCode == workTicket.DtlProblemCode)
                    ).FirstOrDefault();

                    exceptionCode = _database.Table<JT_ClassificationCode>().Where(
                       pc => (pc.ClassificationCode == workTicket.DtlCoverageExceptionCode)
                    ).FirstOrDefault();
                }
            }

            return new App_WorkTicket(workTicket, workTicketStepZero, workTicketClass, 
                                      new App_RepairItem(repairItem, equipmentAsset),
                                      serviceAgreement, problemCode, exceptionCode);
        }

        public App_WorkTicket GetCurrentWorkTicket()
        {
            App_WorkTicket currentWorkTicket = null;

            lock (_locker)
            {
                //App_CurrentSelectionData currentData = _database.Table<App_CurrentSelectionData>().FirstOrDefault();

                //return _database.Table<JT_WorkTicket>().Where(
                //        wt => (wt.SalesOrderNo == currentData.SalesOrderNo) &&
                //              (wt.WTNumber == currentData.WTNumber) &&
                //              (wt.WTStep == currentData.WTStep)
                //    ).FirstOrDefault();
                JT_TechnicianScheduleDetail scheduleDetail = RetrieveCurrentScheduleDetail();

                if (scheduleDetail != null)
                {
                    SO_SalesOrderHeader salesOrderHeader = GetSalesOrderHeader(scheduleDetail);
                    if (salesOrderHeader != null)
                    {
                        App_ScheduledAppointment scheduledAppointment = new App_ScheduledAppointment(scheduleDetail, salesOrderHeader);
                        currentWorkTicket = GetWorkTicket(scheduledAppointment);
                    }
                }
            }

            return currentWorkTicket;
        }

        #endregion
    }
}
