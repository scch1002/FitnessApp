using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkOut.App.Forms.Model;

namespace WorkOut.App.Forms.ViewModel.Interface
{
    public interface IWorkoutDefinitionViewModel
    {
        int WorkOutId { get; set; }

        string WorkOutName { get; set; }

        int NumberOfWarmUpSets { get; set; }

        int WarmUpRepetitions { get; set; }

        int WarmUpWeight { get; set; }

        int NumberOfSets { get; set; }

        int Repetitions { get; set; }

        int Weight { get; set; }

        ICommand UpdateWorkoutDefinition { get; }
    }
}
