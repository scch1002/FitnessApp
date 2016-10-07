using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using WorkOut.App.Forms.DataModel;
using SQLite;
using Xamarin.Forms;
using WorkOut.App.Forms.Model;

namespace WorkOut.App.Forms.Repository
{
    public static class SessionDefinitionRepository
    {
        public static SessionDefinition[] GetSessionDefinitions()
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                return connection.Query<SessionDefinitionRow>("SELECT * FROM SessionDefinition ORDER BY SessionOrder")
                    .Select(s => new SessionDefinition
                    {
                        SessionDefinitonId = s.SessionDefinitonId,
                        SessionName = s.SessionName,
                        SessionOrder = s.SessionOrder
                    }).ToArray();
            }
        }

        public static void AddSessionDefinition(SessionDefinition sessionDefinition)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var sessionDefinitionRow = new SessionDefinitionRow
                {
                    SessionName = sessionDefinition.SessionName,
                    SessionOrder = sessionDefinition.SessionOrder
                };

                connection.Insert(sessionDefinitionRow);

                sessionDefinition.SessionDefinitonId = sessionDefinitionRow.SessionDefinitonId;
            }
        }

        public static void UpdateSessionDefinition(SessionDefinition sessionDefinition)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Update(new SessionDefinitionRow
                {
                    SessionDefinitonId = sessionDefinition.SessionDefinitonId,
                    SessionName = sessionDefinition.SessionName,
                    SessionOrder = sessionDefinition.SessionOrder
                });
            }
        }

        public static void DeleteSessionDefinition(SessionDefinition sessionDefinition)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Delete<SessionDefinitionRow>(sessionDefinition.SessionDefinitonId);
            }
        }
    }
}
