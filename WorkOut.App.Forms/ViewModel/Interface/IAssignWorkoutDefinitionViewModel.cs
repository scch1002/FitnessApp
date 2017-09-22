using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkOut.App.Forms.Model;

namespace WorkOut.App.Forms.ViewModel.Interface
{
    public interface IAssignWorkoutDefinitionViewModel
    {
        WorkOutAssignment.WorkOutTypes WorkOutType { get; set; }

        IEnumerable<IWorkoutDefinitionViewModel> WorkoutDefinitions { get; }

        ISessionDefinitionViewModel SelectedSessionDefinition { get; set; }

        IWorkoutDefinitionViewModel SelectedWorkoutDefinition { get; set; }

        ICommand AssignWorkoutDefinition { get; }
    }
}
