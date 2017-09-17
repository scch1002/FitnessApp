using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.ViewModel
{
    public class SessionViewModel : ViewModelBase, ISessionViewModel
    {
        private readonly IWorkOutRepository _workoutRepository;
        private readonly IUserInterfaceState _userInterfaceState;

        public SessionViewModel(IWorkOutRepository workoutRepository, IUserInterfaceState userInterfaceState)
        {
            _workoutRepository = workoutRepository;
            _userInterfaceState = userInterfaceState;
            SessionWorkOuts = new ObservableCollection<IWorkoutViewModel>();
            SessionWorkOuts.CollectionChanged += SessionWorkOuts_CollectionChanged;
            ViewSelectedWorkout = new RelayCommand(ViewSelectedWorkoutExecute);
        }

        public ICommand ViewSelectedWorkout { get; }

        public int SessionId { get; set; }

        public int SessionDefinitionId { get; set; }

        public string SessionName { get; set; }

        public DateTime SessionDate { get; set; }

        public IEnumerable<IWorkoutViewModel> WarmupSessionWorkOuts => SessionWorkOuts
            .Where(w => w.WorkOutType == Model.WorkOutAssignment.WorkOutTypes.WarmUpWorkout);

        public IEnumerable<IWorkoutViewModel> MainSessionWorkOuts => SessionWorkOuts
            .Where(w => w.WorkOutType == Model.WorkOutAssignment.WorkOutTypes.MainWorkout);

        public ObservableCollection<IWorkoutViewModel> SessionWorkOuts { get; set; }

        private IWorkoutViewModel _selectedWorkout;
        public IWorkoutViewModel SelectedWorkout
        {
            get { return _selectedWorkout; }
            set
            {
                _selectedWorkout = value;
                RaisePropertyChanged();
            }
        }

        private void SessionWorkOuts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("WarmupSessionWorkOuts");
            RaisePropertyChanged("MainSessionWorkOuts");
        }

        private void ViewSelectedWorkoutExecute()
        {
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.WorkoutView);
        }
    }
}
