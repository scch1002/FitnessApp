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
    public interface IWorkoutDefinitionLibraryViewModel
    {
        ICommand AddWorkoutDefinition { get; }

        ICommand ViewWorkoutDefinition { get; }

        ICommand RemoveWorkoutDefinition { get; }

        ObservableCollection<IWorkoutDefinitionViewModel> WorkOutDefinitions { get; set; }

        IWorkoutDefinitionViewModel SelectedWorkoutDefinition { get; set; }
    }
}
