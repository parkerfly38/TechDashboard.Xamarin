using TechDashboard.iOS;
using TechDashboard.Data;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_iOS))]
namespace TechDashboard.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS()
        {

        }

        public SQLite.SQLiteConnection GetConnection()
        {
            //var sqliteFilename = "Contact.db";
            //string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //string libraryPath = Path.Combine(documentsPath, "..", "Library");
            //var path = Path.Combine(libraryPath, sqliteFilename);
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine (documentsPath, "..", "Library");

			var path = Path.Combine(libraryPath, "Contacts2.db");
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}
