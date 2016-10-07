using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Model;
using Xamarin.Forms;

namespace WorkOut.WebApp.Repositories
{
    public static class SessionRepository
    {
        public static Session[] GetSessions()
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                return connection.Query<SessionRow>("SELECT * FROM Session")
                    .Select(s => new Session
                    {
                        SessionId = s.SessionId,
                        SessionDefinitionId = s.SessionDefinitionId,
                        SessionName = s.SessionName,
                        SessionDate = s.SessionDate,
                    }).ToArray();
            }
        }

        public static Session GetSession(int sessionId)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var sessionRow = connection.Get<SessionRow>(sessionId);

                return new Session
                {
                    SessionId = sessionRow.SessionId,
                    SessionDefinitionId = sessionRow.SessionDefinitionId,
                    SessionDate = sessionRow.SessionDate,
                    SessionName = sessionRow.SessionName
                };
            }
        }

        public static void AddSession(Session session)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var sessionRow = new SessionRow
                {
                    SessionDefinitionId = session.SessionDefinitionId,
                    SessionName = session.SessionName,
                    SessionDate = session.SessionDate
                };

                connection.Insert(sessionRow);

                session.SessionId = sessionRow.SessionId;
            }
        }

        public static void UpdateSession(Session session)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Update(new SessionRow
                {
                    SessionId = session.SessionId,
                    SessionDefinitionId = session.SessionDefinitionId,
                    SessionName = session.SessionName,
                    SessionDate = session.SessionDate
                });
            }
        }

        public static void DeleteSession(Session session)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Delete<SessionRow>(session.SessionId);
            }
        }
    }
}