using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Model.Interface;

namespace WorkOut.App.Forms.ViewModel.Interface
{
    public interface ISessionDefinitionViewModel
    {
        ICommand RemoveSelectedWorkOutDefinition { get; }

        int SessionDefinitonId { get; set; }

        int SessionOrder { get; set; }

        string SessionName { get; set; }

        IEnumerable<IWorkoutAssignment> WarmUpWorkOutDefinitions { get; }

        IEnumerable<IWorkoutAssignment> MainWorkOutDefinitions { get; }

        ObservableCollection<IWorkoutAssignment> WorkOutDefinitions { get; set; }

        IWorkoutAssignment SelectedWorkOutDefinition { get; set; }
    }
}
