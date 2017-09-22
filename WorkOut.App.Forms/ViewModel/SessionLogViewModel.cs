using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.ViewModel
{
    public class SessionLogViewModel : ViewModelBase, ISessionLogViewModel
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserInterfaceState _userInterfaceState;

        public SessionLogViewModel(ISessionRepository sessionRepository, IUserInterfaceState userInterfaceState)
        {
            Sessions = new ObservableCollection<ISessionViewModel>(sessionRepository.GetSessions());
            _sessionRepository = sessionRepository;
            _userInterfaceState = userInterfaceState;

            EditSchedule = new RelayCommand(EditScheduleExecute);
            SelectNextSession = new RelayCommand(SelectNextSessionExecute);
            EditSelectedSession = new RelayCommand(EditSelectedSessionExecute);
            RemoveSelectedSession = new RelayCommand(RemoveSelectedSessionExecute, CanRemoveSelectedSessionExecute);
        }

        public ICommand EditSchedule { get; }

        public ICommand SelectNextSession { get; }

        public ICommand EditSelectedSession { get; }

        public ICommand RemoveSelectedSession { get; }

        public ObservableCollection<ISessionViewModel> Sessions { get; set; }

        private ISessionViewModel _selectedSession;
        public ISessionViewModel SelectedSession
        {
            get { return _selectedSession; }
            set
            {
                _selectedSession = value;
                RaisePropertyChanged();
            }
        }

        private bool CanRemoveSelectedSessionExecute()
        {
            return SelectedSession != null;
        }

        private void RemoveSelectedSessionExecute()
        {
            Sessions.Remove(SelectedSession);
            _sessionRepository.DeleteSession(SelectedSession);

            SelectedSession = null;
        }

        private void EditScheduleExecute()
        {
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.ScheduleView);
        }

        private void SelectNextSessionExecute()
        {
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.SelectNextSession);
        }

        private void EditSelectedSessionExecute()
        {
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.SessionView);
        }
    }
}
