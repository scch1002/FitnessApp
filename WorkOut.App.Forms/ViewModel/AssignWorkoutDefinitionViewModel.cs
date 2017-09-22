using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.ViewModel.Interface;
using System.Windows.Input;
using WorkOut.App.Forms.Model;
using GalaSoft.MvvmLight.Command;
using WorkOut.App.Forms.Repository.Interfaces;

namespace WorkOut.App.Forms.ViewModel
{
    public class AssignWorkoutDefinitionViewModel : ViewModelBase, IAssignWorkoutDefinitionViewModel
    {
        private readonly IWorkoutDefinitionLibraryViewModel _workoutDefinitionLibraryViewModel;
        private readonly IWorkOutAssignmentRepository _workOutAssignmentRepository;
        private readonly IUserInterfaceState _userInterfaceState;

        public AssignWorkoutDefinitionViewModel(IWorkOutAssignmentRepository workOutAssignmentRepository, IScheduleViewModel scheduleViewModel, IWorkoutDefinitionLibraryViewModel workoutDefinitionLibraryViewModel, IUserInterfaceState userInterfaceState)
        {
            SelectedSessionDefinition = scheduleViewModel.SelectedSessionDefinition;
            _workoutDefinitionLibraryViewModel = workoutDefinitionLibraryViewModel;
            _workOutAssignmentRepository = workOutAssignmentRepository;
            _userInterfaceState = userInterfaceState;

            AssignWorkoutDefinition = new RelayCommand(AssignWorkoutDefinitionExecute);
        }

        public WorkOutAssignment.WorkOutTypes WorkOutType { get; set; }

        public IEnumerable<IWorkoutDefinitionViewModel> WorkoutDefinitions => _workoutDefinitionLibraryViewModel.WorkOutDefinitions;

        public ISessionDefinitionViewModel SelectedSessionDefinition { get; set; }

        public IWorkoutDefinitionViewModel SelectedWorkoutDefinition { get; set; }

        public ICommand AssignWorkoutDefinition { get; }

        private void AssignWorkoutDefinitionExecute()
        {
            var session = SelectedSessionDefinition;

            var assignment = new WorkOutAssignment
            {
                WorkOutDefinition = SelectedWorkoutDefinition,
                SessionDefinition = SelectedSessionDefinition,
                SessionDefinitionId = session.SessionDefinitonId,
                WorkOutDefinitionId = SelectedWorkoutDefinition.WorkOutId,
                WorkoutType = WorkOutType
            };

            _workOutAssignmentRepository.AssignWorkOutDefinition(assignment);

            session.WorkOutDefinitions.Add(assignment);

            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.SessionDefinitionView);
        }
    }
}
