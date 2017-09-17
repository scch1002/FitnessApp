using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WorkOut.App.Forms.DataModel
{
    public static class DatabaseHelper
    {
        public static void CreateWorkOutDatabase()
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.CreateTable<SessionRow>();
                connection.CreateTable<WorkOutRow>();
                connection.CreateTable<SetRow>();
                connection.CreateTable<SessionDefinitionRow>();
                connection.CreateTable<WorkOutDefinitionRow>();
                connection.CreateTable<WorkOutAssignmentRow>();
            }
        }

        public static bool TableExists(SQLiteConnection connection, string tableName)
        {
            return connection
                .Query<int>("SELECT 1 FROM EXISTS (SELECT 1 FROM sqlite_master WHERE type = 'table' AND name = '?')", new[] { tableName } )
                .Any();
        }

        public static bool RowExists(SQLiteConnection connection, string tableName, string rowId)
        {
            return connection
                .Query<int>("SELECT 1 FROM EXISTS (SELECT 1 FROM ? WHERE _id = ?)", new[] { tableName, rowId })
                .Any();
        }
    }
}
