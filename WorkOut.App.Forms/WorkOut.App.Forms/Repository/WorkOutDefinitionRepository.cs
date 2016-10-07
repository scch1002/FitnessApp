using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Model;
using Xamarin.Forms;

namespace WorkOut.App.Forms.Repository
{
    public class WorkOutDefinitionRepository
    {
        public static WorkOutDefinition[] GetWorkOutDefinitions()
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                return connection.Query<WorkOutDefinitionRow>("SELECT * FROM WorkOutDefinition")
                    .Select(s => CreateWorkOutDefinition(s))
                    .ToArray();
            }
        }

        public static WorkOutDefinition[] GetWorkOutDefinitions(int sessionDefinitionId, int workOutType)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var workoutDefinitionsInSession = WorkOutAssignmentRepository
                    .GetAssignments(sessionDefinitionId)
                    .Where(w => w.WorkOutType == workOutType);

                var workOutDefinitions = new List<WorkOutDefinition>();
                foreach(var workOutDefinitionMap in workoutDefinitionsInSession)
                {
                    var workOutDefinitionRow = connection.Get<WorkOutDefinitionRow>(workOutDefinitionMap.WorkOutDefinitionId);
                    var workOutDefinition = CreateWorkOutDefinition(workOutDefinitionRow);
                    workOutDefinition.WorkOutType = workOutDefinitionMap;
                    workOutDefinitions.Add(workOutDefinition);
                    
                }

                return workOutDefinitions.ToArray();
            }
        }

        public static WorkOutDefinitionRow GetWorkOutDefinition(int workOutDefinitionid)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                return connection.Get<WorkOutDefinitionRow>(workOutDefinitionid);
            }
        }

        public static void AddWorkOutDefinition(WorkOutDefinition workoutDefinition)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var workOutDefinitionRow = new WorkOutDefinitionRow
                {
                    AutoIncrementStartingWeight = workoutDefinition.AutoIncrementStartingWeight,
                    WorkOutName = workoutDefinition.WorkOutName,
                    NumberOfWarmUpSets = workoutDefinition.NumberOfWarmUpSets,
                    WarmUpRepetitions = workoutDefinition.WarmUpRepetitions,
                    WarmUpWeight = workoutDefinition.WarmUpWeight,
                    WarmUpWeightIncrement = workoutDefinition.WarmUpWeightIncrement,
                    NumberOfSets = workoutDefinition.NumberOfSets,
                    Repetitions = workoutDefinition.Repetitions,
                    WeightIncrement = workoutDefinition.WeightIncrement,
                    Weight = workoutDefinition.Weight,
                    RestTimeBetweenSetsMinutes = workoutDefinition.RestTimeBetweenSets.Minutes,
                    RestTimeBetweenSetsSeconds = workoutDefinition.RestTimeBetweenSets.Seconds
                };

                connection.Insert(workOutDefinitionRow);

                workoutDefinition.WorkOutId = workOutDefinitionRow.WorkOutDefinitionId;
            }
        }

        public static void UpdateWorkOutDefinition(WorkOutDefinition workoutDefinition)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Update(new WorkOutDefinitionRow
                {
                    WorkOutDefinitionId = workoutDefinition.WorkOutId,
                    AutoIncrementStartingWeight = workoutDefinition.AutoIncrementStartingWeight,
                    WorkOutName = workoutDefinition.WorkOutName,
                    NumberOfWarmUpSets = workoutDefinition.NumberOfWarmUpSets,
                    WarmUpRepetitions = workoutDefinition.WarmUpRepetitions,
                    WarmUpWeight = workoutDefinition.WarmUpWeight,
                    WarmUpWeightIncrement = workoutDefinition.WarmUpWeightIncrement,
                    NumberOfSets = workoutDefinition.NumberOfSets,
                    Repetitions = workoutDefinition.Repetitions,
                    WeightIncrement = workoutDefinition.WeightIncrement,
                    Weight = workoutDefinition.Weight,
                    RestTimeBetweenSetsMinutes = workoutDefinition.RestTimeBetweenSets.Minutes,
                    RestTimeBetweenSetsSeconds = workoutDefinition.RestTimeBetweenSets.Seconds
                });
            }
        }

        public static void UpdateWorkOutDefinition(WorkOutDefinitionRow workOutDefinitionRow)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Update(workOutDefinitionRow);
            }
        }

        public static void DeleteWorkOutDefinition(WorkOutDefinition workOutDefinition)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var assignments = connection
                    .Query<WorkOutAssignmentRow>("SELECT * FROM WorkOutAssignment WHERE WorkOutDefinitionId = ?", 
                        workOutDefinition.WorkOutId);

                foreach(var assignment in assignments)
                {
                    connection.Delete<WorkOutDefinitionRow>(workOutDefinition.WorkOutId);
                }

                connection.Delete<WorkOutDefinitionRow>(workOutDefinition.WorkOutId);
            }
        }

        private static WorkOutDefinition CreateWorkOutDefinition(WorkOutDefinitionRow workOut)
        {
            return new WorkOutDefinition
            {
                WorkOutId = workOut.WorkOutDefinitionId,
                AutoIncrementStartingWeight = workOut.AutoIncrementStartingWeight,
                NumberOfWarmUpSets = workOut.NumberOfWarmUpSets,
                WarmUpRepetitions = workOut.WarmUpRepetitions,
                WarmUpWeight = workOut.WarmUpWeight,
                WarmUpWeightIncrement = workOut.WarmUpWeightIncrement,
                NumberOfSets = workOut.NumberOfSets,
                WorkOutName = workOut.WorkOutName,
                Repetitions = workOut.Repetitions,
                WeightIncrement = workOut.WeightIncrement,
                Weight = workOut.Weight,
                RestTimeBetweenSets = new TimeSpan(0, workOut.RestTimeBetweenSetsMinutes, workOut.RestTimeBetweenSetsSeconds)
            };
        }
    }
}
