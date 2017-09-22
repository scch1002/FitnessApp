using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkOut.App.Forms.ViewModel.Interface
{
    public interface ISessionLogViewModel
    {
        ICommand EditSchedule { get; }

        ICommand SelectNextSession { get; }

        ICommand EditSelectedSession { get; }

        ICommand RemoveSelectedSession { get; }

        ObservableCollection<ISessionViewModel> Sessions { get; set; }

        ISessionViewModel SelectedSession { get; set; }
    }
}
