using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.WebApp.Repositories;

namespace WorkOut.App.Forms.ViewModel
{
    public class SessionOverViewViewModel : ViewModelBase
    {
        public SessionOverViewViewModel(ObservableCollection<Session> sessions)
        {
            Sessions = sessions;
        }

        public ObservableCollection<Session> Sessions { get; set; }
    }
}
