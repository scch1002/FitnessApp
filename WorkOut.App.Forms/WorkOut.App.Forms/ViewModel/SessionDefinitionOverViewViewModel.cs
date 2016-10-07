using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;

namespace WorkOut.App.Forms.ViewModel
{
    public class SessionDefinitionOverViewViewModel : ViewModelBase
    {
        public SessionDefinitionOverViewViewModel()
        {
            Sessions = new ObservableCollection<SessionDefinition>(SessionDefinitionRepository.GetSessionDefinitions());
        }

        public ObservableCollection<SessionDefinition> Sessions { get; set; }
    }
}
