using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.ViewModel
{
    public class WorkoutDefinitionViewModel : ViewModelBase, IWorkoutDefinitionViewModel
    {
        private readonly IWorkOutDefinitionRepository _workOutDefinitionRepository;
        private readonly IUserInterfaceState _userInterfaceState;

        public WorkoutDefinitionViewModel(IWorkOutDefinitionRepository workOutDefinitionRepository, IUserInterfaceState userInterfaceState)
        {
            _workOutDefinitionRepository = workOutDefinitionRepository;
            _userInterfaceState = userInterfaceState;
            UpdateWorkoutDefinition = new RelayCommand(UpdateWorkoutDefinitionExecute);
        }

        public int WorkOutId { get; set; }
        
        public string WorkOutName { get; set; }

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

        public ICommand UpdateWorkoutDefinition { get; }

        private void UpdateWorkoutDefinitionExecute()
        {
            _workOutDefinitionRepository.UpdateWorkOutDefinition(this);

            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.WorkoutDefinitionLibraryView);
        }
    }
}
