using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Model.Interface;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;
using Xamarin.Forms;

namespace WorkOut.App.Forms.Repository
{
    public class WorkOutAssignmentRepository : IWorkOutAssignmentRepository
    {
        private readonly IWorkOutDefinitionRepository _workoutDefinitionRepository;

        public WorkOutAssignmentRepository(IWorkOutDefinitionRepository workoutDefinitionRepository)
        {
            _workoutDefinitionRepository = workoutDefinitionRepository;
        }

        public WorkOutAssignment[] GetAssignments(ISessionDefinitionViewModel sessionDefinitionViewModel)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var workoutDefinitions = _workoutDefinitionRepository.GetWorkOutDefinitions();

                return connection.Query<WorkOutAssignmentRow>("SELECT * FROM WorkOutAssignment WHERE SessionDefinitionId = ?", sessionDefinitionViewModel.SessionDefinitonId)
                    .Select(s => new WorkOutAssignment
                    {
                        SessionDefinitionId = s.SessionDefinitionId,
                        SessionDefinition = sessionDefinitionViewModel,
                        WorkOutDefinitionId = s.WorkOutDefinitionId,
                        WorkoutType = (WorkOutAssignment.WorkOutTypes) s.WorkOutType,
                        WorkOutDefinition = workoutDefinitions.First(f => f.WorkOutId == s.WorkOutDefinitionId)
                    })
                    .ToArray();
            }
        }

        public void AssignWorkOutDefinition(IWorkoutAssignment workOutAssignment)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var workoutDefinitions = _workoutDefinitionRepository.GetWorkOutDefinitions();

                var workOutDefinitionMap = new WorkOutAssignmentRow
                {
                    SessionDefinitionId = workOutAssignment.SessionDefinitionId,
                    WorkOutDefinitionId = workOutAssignment.WorkOutDefinition.WorkOutId,
                    WorkOutType = (int)workOutAssignment.WorkoutType
                };

                connection.Insert(workOutDefinitionMap);

                workOutAssignment.WorkOutDefinition = workoutDefinitions.First(f => f.WorkOutId == workOutAssignment.WorkOutDefinition.WorkOutId);
            }
        }

        public void UnassignWorkOutDefinition(IWorkoutAssignment workOutAssignment)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var assignments = connection
                    .Query<WorkOutAssignmentRow>("SELECT * FROM WorkOutAssignment WHERE WorkOutDefinitionId = ? AND SessionDefinitionId = ? AND WorkOutType = ?",
                        workOutAssignment.WorkOutDefinition.WorkOutId,
                        workOutAssignment.SessionDefinitionId,
                        workOutAssignment.WorkoutType);

                foreach (var assignment in assignments)
                {
                    connection.Delete(assignment);
                }
            }
        }
    }
}
