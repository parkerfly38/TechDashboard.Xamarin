using System;
using System.Collections.Generic;
using System.Text;

using TechDashboard.iOS;
using TechDashboard.Services;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_iOS))]
namespace TechDashboard.iOS
{
    public class Sms_iOS : ISms
    {
        public void SendSms()
        {
            // puke
        }
    }
}
