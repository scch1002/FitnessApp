using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;
using Xamarin.Forms;

namespace WorkOut.App.Forms.Repository
{
    public class WorkOutRepository : IWorkOutRepository
    {
        private readonly ISessionDefinitionRepository _sessionDefinitionRepository;
        private readonly IWorkOutDefinitionRepository _workOutDefinitionRepository;
        private readonly IWorkOutAssignmentRepository _workoutAssignmentRepository;
        private readonly ISetRepository _setRepository;

        public WorkOutRepository(ISessionDefinitionRepository sessionDefinitionRepository, IWorkOutDefinitionRepository workOutDefinitionRepository, IWorkOutAssignmentRepository workoutAssignmentRepository, ISetRepository setRepository)
        {
            _sessionDefinitionRepository = sessionDefinitionRepository;
            _workOutDefinitionRepository = workOutDefinitionRepository;
            _workoutAssignmentRepository = workoutAssignmentRepository;
            _setRepository = setRepository;
        }

        public IWorkoutViewModel[] GetWorkOuts(ISessionViewModel session)
        {
            var sessionDefinition = _sessionDefinitionRepository
                .GetSessionDefinitions()
                .First(f => f.SessionDefinitonId == session.SessionDefinitionId);
            var assignments = _workoutAssignmentRepository.GetAssignments(sessionDefinition);
            var workOutDefinitions = _workOutDefinitionRepository.GetWorkOutDefinitions(assignments);

            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {               
                return connection
                    .Query<WorkOutRow>("SELECT * FROM WorkOut WHERE SessionId = ?", session.SessionId)
                    .Select(workOut => CreateWorkOutFromWorkOutRow(workOut, workOutDefinitions.FirstOrDefault(f => f.WorkOutId == workOut.WorkOutDefinitionId)))
                    .ToArray();
            }
        }

        public void AddWorkOut(IWorkoutViewModel workout)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var workOutRow = new WorkOutRow
                {
                    SessionId = workout.SessionId,
                    WorkOutDefinitionId = workout.WorkOutDefinitionId,
                    WorkOutName = workout.WorkOutName,
                    WorkOutType = (int)workout.WorkOutType,
                    WorkOutComplete = workout.WorkOutComplete
                };

                connection.Insert(workOutRow);

                workout.WorkOutId = workOutRow.WorkOuId;
            }
        }

        public void UpdateWorkOut(IWorkoutViewModel workout)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Update(new WorkOutRow
                {
                    WorkOuId = workout.WorkOutId,
                    SessionId = workout.SessionId,
                    WorkOutDefinitionId = workout.WorkOutDefinitionId,
                    WorkOutName = workout.WorkOutName,
                    WorkOutType = (int)workout.WorkOutType,
                    WorkOutComplete = workout.WorkOutComplete
                });
            }
        }

        public void DeleteWorkOut(IWorkoutViewModel workOut)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Delete<WorkOutRow>(workOut.WorkOutId);
            }
        }

        private IWorkoutViewModel CreateWorkOutFromWorkOutRow(WorkOutRow workOutRow, IWorkoutDefinitionViewModel workOutDefinition)
        {
            var workout = App.Container.Resolve<IWorkoutViewModel>();

            workout.WorkOutId = workOutRow.WorkOuId;
            workout.SessionId = workOutRow.SessionId;
            workout.WorkOutDefinitionId = workOutRow.WorkOutDefinitionId;
            workout.WorkOutName = workOutRow.WorkOutName;
            workout.WorkOutType = (WorkOutAssignment.WorkOutTypes)workOutRow.WorkOutType;
            workout.WorkOutComplete = workOutRow.WorkOutComplete;

            foreach(var set in _setRepository.GetSets(workOutRow.WorkOuId))
            {
                workout.WorkOutSets.Add(set);
            }

            return workout;
        }
    }
}
