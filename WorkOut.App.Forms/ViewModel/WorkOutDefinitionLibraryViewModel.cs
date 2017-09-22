using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class WorkOutDefinitionLibraryViewModel : ViewModelBase, IWorkoutDefinitionLibraryViewModel
    {
        private readonly IWorkOutDefinitionRepository _workOutDefinitionRepository;
        private readonly IUserInterfaceState _userInterfaceState;

        public WorkOutDefinitionLibraryViewModel(IWorkOutDefinitionRepository workoutDefinitionRepository, IUserInterfaceState userInterfaceState)
        {
            _workOutDefinitionRepository = workoutDefinitionRepository;
            _userInterfaceState = userInterfaceState;
            WorkOutDefinitions = new ObservableCollection<IWorkoutDefinitionViewModel>(workoutDefinitionRepository.GetWorkOutDefinitions());
            ViewWorkoutDefinition = new RelayCommand(ViewWorkoutDefinitionExecute, CanViewWorkoutDefinitionExecute);
            AddWorkoutDefinition = new RelayCommand(AddWorkOutDefinitionExecute);
            RemoveWorkoutDefinition = new RelayCommand(RemoveSelectedWorkoutDefinitionExecute, CanRemoveSelectedWorkoutDefinition);
        }

        public ObservableCollection<IWorkoutDefinitionViewModel> WorkOutDefinitions { get; set; }

        private IWorkoutDefinitionViewModel _selectedWorkoutDefinition;
        public IWorkoutDefinitionViewModel SelectedWorkoutDefinition
        {
            get { return _selectedWorkoutDefinition; }
            set
            {
                _selectedWorkoutDefinition = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddWorkoutDefinition { get; }

        public ICommand ViewWorkoutDefinition { get; }

        public ICommand RemoveWorkoutDefinition { get; }

        private void AddWorkOutDefinitionExecute()
        {
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.AddWorkoutDefinition);
        }

        private bool CanRemoveSelectedWorkoutDefinition()
        {
            return SelectedWorkoutDefinition != null;
        }

        private void RemoveSelectedWorkoutDefinitionExecute()
        {
            _workOutDefinitionRepository.DeleteWorkOutDefinition(SelectedWorkoutDefinition);

            WorkOutDefinitions.Remove(SelectedWorkoutDefinition);

            MessengerInstance.Send(new DeleteAssignmentMessage
            {
                WorkoutDefinitionId = SelectedWorkoutDefinition.WorkOutId
            });

            SelectedWorkoutDefinition = null;
        }

        private bool CanViewWorkoutDefinitionExecute()
        {
            return SelectedWorkoutDefinition != null;
        }

        private void ViewWorkoutDefinitionExecute()
        {
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.ViewWorkoutDefinition);
        }
    }
}
