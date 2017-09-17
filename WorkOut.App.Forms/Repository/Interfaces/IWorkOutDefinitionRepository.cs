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
    public interface IWorkOutDefinitionRepository
    {
        IWorkoutDefinitionViewModel[] GetWorkOutDefinitions();

        IWorkoutDefinitionViewModel[] GetWorkOutDefinitions(IEnumerable<IWorkoutAssignment> workOutAssignments);

        void AddWorkOutDefinition(IWorkoutDefinitionViewModel workoutDefinition);

        void UpdateWorkOutDefinition(IWorkoutDefinitionViewModel workoutDefinition);

        void DeleteWorkOutDefinition(IWorkoutDefinitionViewModel workOutDefinition);
    }
}
