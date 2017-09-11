using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using TechDashboard.Droid;
using TechDashboard.Data;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Android))]
namespace TechDashboard.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android()
        {
            // empty
        }

        public SQLiteConnection GetConnection()
        {
            var path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "TechDashboard.db");
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}