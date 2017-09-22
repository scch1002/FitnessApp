using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model.Interface;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.Model
{
    public class WorkOutAssignment : IWorkoutAssignment
    {
        public enum WorkOutTypes
        {
            MainWorkout,
            WarmUpWorkout
        }

        public int WorkOutDefinitionId { get; set; }

        public int SessionDefinitionId { get; set; }

        public WorkOutTypes WorkoutType { get; set; }

        public IWorkoutDefinitionViewModel WorkOutDefinition { get; set; }

        public ISessionDefinitionViewModel SessionDefinition { get; set; }
    }
}
