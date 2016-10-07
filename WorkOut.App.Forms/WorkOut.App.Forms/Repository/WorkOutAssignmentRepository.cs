using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Model;
using Xamarin.Forms;

namespace WorkOut.App.Forms.Repository
{
    public class WorkOutAssignmentRepository
    {
        public static WorkOutAssignment[] GetAssignments(int sessionId)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                return connection.Query<WorkOutAssignmentRow>("SELECT * FROM WorkOutAssignment WHERE SessionDefinitionId = ?", sessionId)
                    .Select(s => new WorkOutAssignment
                    {
                        SessionDefinitionId = s.SessionDefinitionId,
                        WorkOutDefinitionId = s.WorkOutDefinitionId,
                        WorkOutType = s.WorkOutType
                    })
                    .ToArray();
            }
        }

        public static void AssignWorkOutDefinition(WorkOutAssignment workOutAssignment)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var workOutDefinitionMap = new WorkOutAssignmentRow
                {
                    SessionDefinitionId = workOutAssignment.SessionDefinitionId,
                    WorkOutDefinitionId = workOutAssignment.WorkOutDefinitionId,
                    WorkOutType = workOutAssignment.WorkOutType
                };

                connection.Insert(workOutDefinitionMap);
            }
        }

        public static void UnassignWorkOutDefinition(WorkOutAssignment workOutAssignment)
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var assignments = connection
                    .Query<WorkOutAssignmentRow>("SELECT * FROM WorkOutAssignment WHERE WorkOutDefinitionId = ? AND SessionDefinitionId = ? AND WorkOutType = ?",
                        workOutAssignment.WorkOutDefinitionId,
                        workOutAssignment.SessionDefinitionId,
                        workOutAssignment.WorkOutType);

                foreach (var assignment in assignments)
                {
                    connection.Delete(assignment);
                }
            }
        }
    }
}
