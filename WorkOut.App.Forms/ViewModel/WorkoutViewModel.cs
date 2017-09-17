using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.ViewModel
{
    public class WorkoutViewModel : ViewModelBase, IWorkoutViewModel
    {
        private readonly IWorkOutRepository _workoutRepository;
        private readonly IUserInterfaceState _userInterfaceState;

        public WorkoutViewModel(IWorkOutRepository workoutRepository, IUserInterfaceState userInterfaceState)
        {
            _workoutRepository = workoutRepository;
            _userInterfaceState = userInterfaceState;
            WorkOutSets = new ObservableCollection<ISetViewModel>();
            WorkOutSets.CollectionChanged += WorkOutSets_CollectionChanged;
            SaveWorkout = new RelayCommand(SaveWorkoutExecute);
            ViewSelectedSet = new RelayCommand(ViewSelectedSetExecute);
        }

        public ICommand SaveWorkout { get; }

        public int SessionId { get; set; }

        public int WorkOutId { get; set; }

        public int WorkOutDefinitionId { get; set; }

        public WorkOutAssignment.WorkOutTypes WorkOutType { get; set; }

        public TimeSpan RestTimeBetweenSets { get; set; }

        public string WorkOutName { get; set; }

        private bool _workOutComplete;
        public bool WorkOutComplete
        {
            get { return _workOutComplete; }
            set
            {
                _workOutComplete = value;
                RaisePropertyChanged("WorkOutComplete");
            }
        }

        public IEnumerable<ISetViewModel> WarmupWorkOut => WorkOutSets.Where(w => w.SetType == WorkOutAssignment.WorkOutTypes.WarmUpWorkout);

        public IEnumerable<ISetViewModel> MainWorkOut => WorkOutSets.Where(w => w.SetType == WorkOutAssignment.WorkOutTypes.MainWorkout);

        public ObservableCollection<ISetViewModel> WorkOutSets { get; set; }

        public bool AllSetsCompleted => WorkOutSets.All(a => a.CompletedRepetitions == a.TotalRepetitions);

        private ISetViewModel _setViewModel;
        public ISetViewModel SelectedSet
        {
            get { return _setViewModel; }
            set
            {
                _setViewModel = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ViewSelectedSet { get; }

        private void WorkOutSets_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("WarmupWorkOut");
            RaisePropertyChanged("MainWorkOut");
        }

        private void SaveWorkoutExecute()
        {
            WorkOutComplete = true;
            _workoutRepository.UpdateWorkOut(this);
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.SessionView);
        }

        private void ViewSelectedSetExecute()
        {
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.SetView);
        }
    }
}
