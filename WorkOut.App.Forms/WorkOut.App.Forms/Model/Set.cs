using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WorkOut.App.Forms.Model
{
    public class Set : ViewModelBase
    {
        public int SetId { get; set; }

        public int SetType { get; set; }

        private string _setName;
        public string SetName
        {
            get { return _setName; }
            set
            {
                _setName = value;
                RaisePropertyChanged("SetName");
            }
        }

        private int _totalRepetitions;
        public int TotalRepetitions
        {
            get { return _totalRepetitions; }
            set
            {
                _totalRepetitions = value;
                RaisePropertyChanged("TotalRepetitions");
            }
        }

        private int _completedRepetitions;
        public int CompletedRepetitions
        {
            get { return _completedRepetitions; }
            set
            {
                _completedRepetitions = value;
                RaisePropertyChanged("CompletedRepetitions");
            }
        }

        private int _weight;
        public int Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                RaisePropertyChanged("Weight");
            }
        }
    }
}