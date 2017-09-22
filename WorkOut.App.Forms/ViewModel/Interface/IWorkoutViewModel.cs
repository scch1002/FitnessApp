using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkOut.App.Forms.Model;

namespace WorkOut.App.Forms.ViewModel.Interface
{
    public interface IWorkoutViewModel
    {
        int SessionId { get; set; }

        int WorkOutId { get; set; }

        int WorkOutDefinitionId { get; set; }

        ICommand ViewSelectedSet { get; } 

        ICommand SaveWorkout { get; }

        WorkOutAssignment.WorkOutTypes WorkOutType { get; set; }

        TimeSpan RestTimeBetweenSets { get; set; }

        string WorkOutName { get; set; }

        bool WorkOutComplete { get; set; }

        IEnumerable<ISetViewModel> WarmupWorkOut { get; }

        IEnumerable<ISetViewModel> MainWorkOut { get; }

        ObservableCollection<ISetViewModel> WorkOutSets { get; set; }

        bool AllSetsCompleted { get; }

        ISetViewModel SelectedSet { get; set; }
    }
}
