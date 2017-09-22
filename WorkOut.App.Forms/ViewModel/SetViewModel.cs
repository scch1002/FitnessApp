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
    public class SetViewModel : ViewModelBase, ISetViewModel
    {
        private readonly ISetRepository _setRepository;
        private readonly IUserInterfaceState _userInterfaceState;

        public SetViewModel(ISetRepository setRepository, IUserInterfaceState userInterfaceState)
        {
            _setRepository = setRepository;
            _userInterfaceState = userInterfaceState;
            SaveSet = new RelayCommand(SaveSelectedSetExecute);
        }

        public ICommand SaveSet { get; }

        public int WorkOutId { get; set; }

        public int SetId { get; set; }

        public WorkOutAssignment.WorkOutTypes SetType { get; set; }

        private string _setName;
        public string SetName
        {
            get { return _setName; }
            set
            {
                _setName = value;
                RaisePropertyChanged();
            }
        }

        private int _totalRepetitions;
        public int TotalRepetitions
        {
            get { return _totalRepetitions; }
            set
            {
                _totalRepetitions = value;
                RaisePropertyChanged();
            }
        }

        private int _completedRepetitions;
        public int CompletedRepetitions
        {
            get { return _completedRepetitions; }
            set
            {
                _completedRepetitions = value;
                RaisePropertyChanged();
            }
        }

        private int _weight;
        public int Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                RaisePropertyChanged();
            }
        }

        private void SaveSelectedSetExecute()
        {
            _setRepository.UpdateSet(this);
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.WorkoutView);
        }
    }
}
