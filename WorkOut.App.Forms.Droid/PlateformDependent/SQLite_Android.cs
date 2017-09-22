using Xamarin.Forms;
using SQLite;
using WorkOut.App.Forms.DataModel;
using System.IO;
using WorkOut.App.Forms.Droid.PlateformDependent;

[assembly: Dependency(typeof(SQLite_Android))]

namespace WorkOut.App.Forms.Droid.PlateformDependent
{
    public class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "WorkOutSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); 
            var path = Path.Combine(documentsPath, sqliteFilename);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}