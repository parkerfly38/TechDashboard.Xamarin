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
        #region App_WorkTicketText

        public App_WorkTicketText RetrieveTextFromCurrentAppWorkTicket()
        {
            JT_WorkTicketText note = null;

            lock (_locker)
            {
                JT_TechnicianScheduleDetail scheduleDetail = RetrieveCurrentScheduleDetail();

                if (scheduleDetail != null)
                {
                    note =
                        _database.Table<JT_WorkTicketText>().Where(
                            wtt => (wtt.SalesOrderNo == scheduleDetail.SalesOrderNo) &&
                                   (wtt.WTNumber == scheduleDetail.WTNumber) &&
                                   (wtt.WTStep == scheduleDetail.WTStep)
                        ).FirstOrDefault();
                }
            }

            return new App_WorkTicketText(note);
        }

        #endregion
    }
}
