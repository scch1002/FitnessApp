using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using WorkOut.App.Forms.Repository;
using WorkOut.WebApp.Repositories;
using WorkOut.App.Forms.Model;

namespace WorkOut.App.Forms.Model
{ 
    public class SessionDefinition : ViewModelBase
    {
        public int SessionDefinitonId { get; set; }

        public int SessionOrder { get; set; }

        private string _sessionName;
        public string SessionName
        {
            get { return _sessionName; }
            set
            {
                _sessionName = value;
                RaisePropertyChanged("SessionName");
            }
        }

        public ObservableCollection<WorkOutDefinition> SessionWarmUpWorkOuts { get; set; }

        public ObservableCollection<WorkOutDefinition> SessionWorkOuts { get; set; }
    }
}