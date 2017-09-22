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
using WorkOut.App.Forms.Messages;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.ViewModel
{
    public class ScheduleViewModel : ViewModelBase, IScheduleViewModel
    {
        private readonly ISessionDefinitionRepository _sessionDefinitionRepository;
        private readonly IWorkOutAssignmentRepository _workOutAssignmentRepository;
        private readonly IUserInterfaceState _userInterfaceState;

        public ScheduleViewModel(
            ISessionDefinitionRepository sessionDefinitionRepository,
            IWorkOutAssignmentRepository workOutAssignmentRepository,
            IUserInterfaceState userInterfaceState)
        {
            _sessionDefinitionRepository = sessionDefinitionRepository;
            _workOutAssignmentRepository = workOutAssignmentRepository;
            _userInterfaceState = userInterfaceState;
            ViewNewSessionDefinitionForm = new RelayCommand(ViewNewSessionDefinitionFormExecute);
            EditSessionDefinition = new RelayCommand(EditSessionDefinitionExecute, CanEditSessionDefinitionExecute);
            ViewWorkOutDefinitionLibrary = new RelayCommand(ViewWorkOutDefinitionLibraryExecute);
            RemoveSelectedSessionDefinition = new RelayCommand(RemoveSelectedSessionDefinitionExecute);
            Sessions = new ObservableCollection<ISessionDefinitionViewModel>(_sessionDefinitionRepository.GetSessionDefinitions());
        }

        public ObservableCollection<ISessionDefinitionViewModel> Sessions { get; set; }

        private ISessionDefinitionViewModel _selectedSessionDefinition;
        public ISessionDefinitionViewModel SelectedSessionDefinition
        {
            get { return _selectedSessionDefinition; }
            set
            { 
                _selectedSessionDefinition = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ViewNewSessionDefinitionForm { get; }

        public ICommand AddNewSessionDefinition { get; }

        public ICommand EditSessionDefinition { get; }

        public ICommand RemoveSelectedSessionDefinition { get; }

        public ICommand ViewWorkOutDefinitionLibrary { get; }

        private bool CanEditSessionDefinitionExecute()
        {
            return SelectedSessionDefinition != null;
        }

        private void EditSessionDefinitionExecute()
        {
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.SessionDefinitionView);
        }

        private void RemoveSelectedSessionDefinitionExecute()
        {
            Sessions.Remove(SelectedSessionDefinition);

            if (SelectedSessionDefinition.WorkOutDefinitions.Any())
            {
                foreach (var workOutDefinition in SelectedSessionDefinition.WorkOutDefinitions)
                {
                    _workOutAssignmentRepository.UnassignWorkOutDefinition(workOutDefinition);
                }
            }

            _sessionDefinitionRepository.DeleteSessionDefinition(SelectedSessionDefinition);

            SelectedSessionDefinition = null;
        }

        private void ViewWorkOutDefinitionLibraryExecute()
        {
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.WorkoutDefinitionLibraryView);
        }

        private void ViewNewSessionDefinitionFormExecute()
        {
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.ScheduleAdd);
        }

        private void RemoveAssignmentUponDelete(DeleteAssignmentMessage message)
        {
            foreach(var assignment in Sessions.Where(s => s.WorkOutDefinitions.Any(w => w.SessionDefinitionId == message.SessionDefinitionId 
                && w.WorkOutDefinition.WorkOutId == message.WorkoutDefinitionId)))
            {

            }
        }
    }
}
