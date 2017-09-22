using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkOut.App.Forms.ViewModel.Interface
{
    public interface ISessionViewModel
    {
        ICommand ViewSelectedWorkout { get; }

        int SessionId { get; set; }

        int SessionDefinitionId { get; set; }

        string SessionName { get; set; }

        DateTime SessionDate { get; set; }

        IEnumerable<IWorkoutViewModel> WarmupSessionWorkOuts { get; }

        IEnumerable<IWorkoutViewModel> MainSessionWorkOuts { get; }

        ObservableCollection<IWorkoutViewModel> SessionWorkOuts { get; set; }

        IWorkoutViewModel SelectedWorkout { get; set; }
    }
}
