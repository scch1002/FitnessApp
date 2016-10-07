using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Model;
using Xamarin.Forms;
using ModelWorkOut = WorkOut.App.Forms.Model.WorkOut;

namespace WorkOut.App.Forms.Repository
{
    public static class WorkOutRepository
    {
        public static ModelWorkOut[] GetWorkOuts(Session session, int workOutType)
        {
            var workOutDefinitions = WorkOutDefinitionRepository.GetWorkOutDefinitions(session.SessionDefinitionId, workOutType);

            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {               
                return connection
                    .Query<WorkOutRow>("SELECT * FROM WorkOut WHERE WorkOutType = ? AND SessionId = ?", workOutType, session.SessionId)
                    .Select(workOut => CreateWorkOutFromWorkOutRow(workOut, workOutDefinitions.FirstOrDefault(f => f.WorkOutId == workOut.WorkOutDefinitionId)))
                    .ToArray();
            }
        }

        public static void AddWorkOut(ModelWorkOut workout, Session session)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var workOutRow = new WorkOutRow
                {
                    SessionId = session.SessionId,
                    WorkOutDefinitionId = workout.WorkOutDefinitionId,
                    WorkOutName = workout.WorkOutName,
                    WorkOutType = workout.WorkOutType,
                    WorkOutComplete = workout.WorkOutComplete
                };

                connection.Insert(workOutRow);

                workout.WorkOutId = workOutRow.WorkOuId;
            }
        }

        public static void UpdateWorkOut(ModelWorkOut workout, Session session)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Update(new WorkOutRow
                {
                    WorkOuId = workout.WorkOutId,
                    SessionId = session.SessionId,
                    WorkOutDefinitionId = workout.WorkOutDefinitionId,
                    WorkOutName = workout.WorkOutName,
                    WorkOutType = workout.WorkOutType,
                    WorkOutComplete = workout.WorkOutComplete
                });
            }
        }

        public static void DeleteWorkOut(ModelWorkOut workOut)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                connection.Delete<WorkOutRow>(workOut.WorkOutId);
            }
        }

        private static ModelWorkOut CreateWorkOutFromWorkOutRow(WorkOutRow workOutRow, WorkOutDefinition workOutDefinition)
        {
            return new ModelWorkOut
            {
                WorkOutId = workOutRow.WorkOuId,
                WorkOutDefinitionId = workOutRow.WorkOutDefinitionId,
                WorkOutName = workOutRow.WorkOutName,
                WorkOutType = workOutRow.WorkOutType,
                RestTimeBetweenSets = workOutDefinition.RestTimeBetweenSets,
                WorkOutComplete = workOutRow.WorkOutComplete
            };
        }
    }
}
