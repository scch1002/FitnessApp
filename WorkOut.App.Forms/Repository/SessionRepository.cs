using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Repository.Interfaces;
using Xamarin.Forms;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IWorkOutRepository _workoutRepository;
        private readonly ISetRepository _setRepository;

        public SessionRepository(IWorkOutRepository workOutRepository, ISetRepository setRepository)
        {
            _workoutRepository = workOutRepository;
            _setRepository = setRepository;
        }

        public ISessionViewModel[] GetSessions()
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                return connection.Query<SessionRow>("SELECT * FROM Session")
                    .Select(CreateSessionViewModel).ToArray();
            }
        }

        public ISessionViewModel GetSession(int sessionId)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var sessionRow = connection.Get<SessionRow>(sessionId);

                return CreateSessionViewModel(sessionRow);
            }
        }

        public void AddSession(ISessionViewModel session)
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

                foreach (var workout in session.SessionWorkOuts)
                {
                    workout.SessionId = session.SessionId;
                    _workoutRepository.AddWorkOut(workout);

                    foreach (var set in workout.WorkOutSets)
                    {
                        set.WorkOutId = workout.WorkOutId;
                        _setRepository.AddSet(set);
                    }
                }
            }
        }

        public void UpdateSession(ISessionViewModel session)
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

        public void DeleteSession(ISessionViewModel session)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Delete<SessionRow>(session.SessionId);

                if (session.SessionWorkOuts != null)
                {
                    foreach (var workOut in session.SessionWorkOuts)
                    {
                        if (workOut.WorkOutSets != null)
                        {
                            foreach (var set in workOut.WorkOutSets)
                            {
                                _setRepository.DeleteSet(set);
                            }
                        }

                        _workoutRepository.DeleteWorkOut(workOut);
                    }
                }
            }
        }

        private ISessionViewModel CreateSessionViewModel(SessionRow sessionRow)
        {
            var session = App.Container.Resolve<ISessionViewModel>();
            session.SessionId = sessionRow.SessionId;
            session.SessionDefinitionId = sessionRow.SessionDefinitionId;
            session.SessionName = sessionRow.SessionName;
            session.SessionDate = sessionRow.SessionDate;
            foreach (var workOut in _workoutRepository.GetWorkOuts(session))
            {
                session.SessionWorkOuts.Add(workOut);
            }
            return session;
        }
    }
}