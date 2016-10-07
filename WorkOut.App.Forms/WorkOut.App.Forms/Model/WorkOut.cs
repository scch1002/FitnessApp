using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOut.App.Forms.Model
{
    public class WorkOut : ViewModelBase
    {
        public int WorkOutId { get; set; }

        public int WorkOutDefinitionId { get; set; }

        public int WorkOutType { get; set; }

        public TimeSpan RestTimeBetweenSets { get; set; }

        public string WorkOutName { get; set; }

        private bool _workOutComplete;
        public bool WorkOutComplete {
            get { return _workOutComplete; }
            set
            {
                _workOutComplete = value;
                RaisePropertyChanged("WorkOutComplete");
            }
        }

        public ObservableCollection<Set> WorkOutWarmUpSets { get; set; }

        public ObservableCollection<Set> WorkOutSets { get; set; }
    }
}
