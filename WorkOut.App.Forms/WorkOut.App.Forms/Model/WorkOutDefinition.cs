using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOut.App.Forms.Model
{
    public class WorkOutDefinition : ViewModelBase
    {
        public int WorkOutId { get; set; }

        public WorkOutAssignment WorkOutType { get; set; }

        public string WorkOutName { get; set; }

        private bool _autoIncrementStartingWeight;
        public bool AutoIncrementStartingWeight
        {
            get { return _autoIncrementStartingWeight; }
            set
            {
                _autoIncrementStartingWeight = value;
                RaisePropertyChanged("AutoIncrementStartingWeight");
            }
        }

        private int _numberOfWarmUpSets;
        public int NumberOfWarmUpSets
        {
            get { return _numberOfWarmUpSets; }
            set
            {
                _numberOfWarmUpSets = value;
                RaisePropertyChanged("NumberOfWarmUpSets");
            }
        }

        private int _warmUpRepetitions;
        public int WarmUpRepetitions
        {
            get { return _warmUpRepetitions; }
            set
            {
                _warmUpRepetitions = value;
                RaisePropertyChanged("WarmUpRepetitions");
            }
        }

        private int _warmUpWeight;
        public int WarmUpWeight
        {
            get { return _warmUpWeight; }
            set
            {
                _warmUpWeight = value;
                RaisePropertyChanged("WarmUpWeight");
            }
        }

        private int _warmUpWeightIncrement;
        public int WarmUpWeightIncrement
        {
            get { return _warmUpWeightIncrement; }
            set
            {
                _warmUpWeightIncrement = value;
                RaisePropertyChanged("WarmUpWeightIncrement");
            }
        }

        private int _numberOfSets;
        public int NumberOfSets
        {
            get { return _numberOfSets; }
            set
            {
                _numberOfSets = value;
                RaisePropertyChanged("NumberOfSets");
            }
        }

        private int _repetitions;
        public int Repetitions
        {
            get { return _repetitions; }
            set
            {
                _repetitions = value;
                RaisePropertyChanged("Repetitions");
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

        private int _weightIncrement;
        public int WeightIncrement
        {
            get { return _weightIncrement; }
            set
            {
                _weightIncrement = value;
                RaisePropertyChanged("WeightIncrement");
            }
        }

        private TimeSpan _restTimeBetweenSets;
        public TimeSpan RestTimeBetweenSets
        {
            get { return _restTimeBetweenSets; }
            set
            {
                _restTimeBetweenSets = value;
                RaisePropertyChanged("RestTimeBetweenSets");
            }
        }
    }
}
