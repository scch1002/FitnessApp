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
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.Repository
{
    public class SessionDefinitionRepository : ISessionDefinitionRepository
    {
        private IWorkOutAssignmentRepository _workOutAssignmentRepository;

        public SessionDefinitionRepository(IWorkOutAssignmentRepository workOutAssignmentRepository)
        {
            _workOutAssignmentRepository = workOutAssignmentRepository;
        }

        public ISessionDefinitionViewModel[] GetSessionDefinitions()
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                return connection.Query<SessionDefinitionRow>("SELECT * FROM SessionDefinition ORDER BY SessionOrder")
                    .Select(s =>
                    {
                        var newSessionDefinitionViewModel = App.Container.Resolve<ISessionDefinitionViewModel>();
                        newSessionDefinitionViewModel.SessionDefinitonId = s.SessionDefinitonId;
                        newSessionDefinitionViewModel.SessionName = s.SessionName;
                        newSessionDefinitionViewModel.SessionOrder = s.SessionOrder;
                        foreach(var workoutAssignment in _workOutAssignmentRepository.GetAssignments(newSessionDefinitionViewModel))
                        {
                            newSessionDefinitionViewModel.WorkOutDefinitions.Add(workoutAssignment);
                        }
                        return newSessionDefinitionViewModel;
                    }).ToArray();
            }
        }

        public void AddSessionDefinition(ISessionDefinitionViewModel sessionDefinition)
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

        public void UpdateSessionDefinition(ISessionDefinitionViewModel sessionDefinition)
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

        public void DeleteSessionDefinition(ISessionDefinitionViewModel sessionDefinition)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Delete<SessionDefinitionRow>(sessionDefinition.SessionDefinitonId);
            }
        }
    }
}
