using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class WorkOutDefinitionRepository : IWorkOutDefinitionRepository
    {
        public IWorkoutDefinitionViewModel[] GetWorkOutDefinitions()
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                return connection.Query<WorkOutDefinitionRow>("SELECT * FROM WorkOutDefinition")
                    .Select(s => CreateWorkOutDefinition(s))
                    .ToArray();
            }
        }

        public IWorkoutDefinitionViewModel[] GetWorkOutDefinitions(IEnumerable<IWorkoutAssignment> workOutAssignments)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var workOutDefinitions = new List<IWorkoutDefinitionViewModel>();
                foreach(var workOutDefinitionMap in workOutAssignments)
                {
                    var workOutDefinitionRow = connection.Get<WorkOutDefinitionRow>(workOutDefinitionMap.WorkOutDefinition.WorkOutId);
                    var workOutDefinition = CreateWorkOutDefinition(workOutDefinitionRow);
                    workOutDefinitions.Add(workOutDefinition);                   
                }

                return workOutDefinitions.ToArray();
            }
        }

        public WorkOutDefinitionRow GetWorkOutDefinition(int workOutDefinitionid)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                return connection.Get<WorkOutDefinitionRow>(workOutDefinitionid);
            }
        }

        public void AddWorkOutDefinition(IWorkoutDefinitionViewModel workoutDefinition)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var workOutDefinitionRow = new WorkOutDefinitionRow
                {
                    WorkOutName = workoutDefinition.WorkOutName,
                    NumberOfWarmUpSets = workoutDefinition.NumberOfWarmUpSets,
                    WarmUpRepetitions = workoutDefinition.WarmUpRepetitions,
                    WarmUpWeight = workoutDefinition.WarmUpWeight,
                    NumberOfSets = workoutDefinition.NumberOfSets,
                    Repetitions = workoutDefinition.Repetitions,
                    Weight = workoutDefinition.Weight
                };

                connection.Insert(workOutDefinitionRow);

                workoutDefinition.WorkOutId = workOutDefinitionRow.WorkOutDefinitionId;
            }
        }

        public void UpdateWorkOutDefinition(IWorkoutDefinitionViewModel workoutDefinition)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Update(new WorkOutDefinitionRow
                {
                    WorkOutDefinitionId = workoutDefinition.WorkOutId,
                    WorkOutName = workoutDefinition.WorkOutName,
                    NumberOfWarmUpSets = workoutDefinition.NumberOfWarmUpSets,
                    WarmUpRepetitions = workoutDefinition.WarmUpRepetitions,
                    WarmUpWeight = workoutDefinition.WarmUpWeight,
                    NumberOfSets = workoutDefinition.NumberOfSets,
                    Repetitions = workoutDefinition.Repetitions,
                    Weight = workoutDefinition.Weight
                });
            }
        }

        public void UpdateWorkOutDefinition(WorkOutDefinitionRow workOutDefinitionRow)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Update(workOutDefinitionRow);
            }
        }

        public void DeleteWorkOutDefinition(IWorkoutDefinitionViewModel workOutDefinition)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var assignments = connection
                    .Query<WorkOutAssignmentRow>("SELECT * FROM WorkOutAssignment WHERE WorkOutDefinitionId = ?", 
                        workOutDefinition.WorkOutId);

                foreach(var assignment in assignments)
                {
                    connection.Delete<WorkOutAssignmentRow>(assignment.AssignmentId);
                }

                connection.Delete<WorkOutDefinitionRow>(workOutDefinition.WorkOutId);
            }
        }

        private IWorkoutDefinitionViewModel CreateWorkOutDefinition(WorkOutDefinitionRow workOut)
        {
            var workoutDefinition = App.Container.Resolve<IWorkoutDefinitionViewModel>();

            workoutDefinition.WorkOutId = workOut.WorkOutDefinitionId;
            workoutDefinition.NumberOfWarmUpSets = workOut.NumberOfWarmUpSets;
            workoutDefinition.WarmUpRepetitions = workOut.WarmUpRepetitions;
            workoutDefinition.WarmUpWeight = workOut.WarmUpWeight;
            workoutDefinition.NumberOfSets = workOut.NumberOfSets;
            workoutDefinition.WorkOutName = workOut.WorkOutName;
            workoutDefinition.Repetitions = workOut.Repetitions;
            workoutDefinition.Weight = workOut.Weight;

            return workoutDefinition;
        }
    }
}
