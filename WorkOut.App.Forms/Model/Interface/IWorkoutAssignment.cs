using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.Model.Interface
{
    public interface IWorkoutAssignment
    {
        int SessionDefinitionId { get; set; }

        IWorkoutDefinitionViewModel WorkOutDefinition { get; set; }

        WorkOutAssignment.WorkOutTypes WorkoutType { get; set; }
    }
}
