using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Model.Interface;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.Repository.Interfaces
{
    public interface IWorkOutAssignmentRepository
    {
        WorkOutAssignment[] GetAssignments(ISessionDefinitionViewModel sessionDefinitionViewModel);

        void AssignWorkOutDefinition(IWorkoutAssignment workOutAssignment);

        void UnassignWorkOutDefinition(IWorkoutAssignment workOutAssignment);
    }
}
