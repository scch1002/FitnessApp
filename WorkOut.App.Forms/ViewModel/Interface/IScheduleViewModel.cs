using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkOut.App.Forms.ViewModel.Interface
{
    public interface IScheduleViewModel
    {
        ObservableCollection<ISessionDefinitionViewModel> Sessions { get; set; }

        ISessionDefinitionViewModel SelectedSessionDefinition { get; set; }

        ICommand EditSessionDefinition { get; }

        ICommand RemoveSelectedSessionDefinition { get; }

        ICommand ViewWorkOutDefinitionLibrary { get; }
    }
}
