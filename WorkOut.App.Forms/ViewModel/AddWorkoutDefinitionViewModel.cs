using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.ViewModel
{
    public class AddWorkoutDefinitionViewModel : ViewModelBase, IAddWorkoutDefinitionViewModel
    {
        private readonly IWorkOutDefinitionRepository _workOutDefinitionRepository;
        private readonly IWorkoutDefinitionLibraryViewModel _workoutDefinitionLibraryViewModel;
        private readonly IUserInterfaceState _userInterfaceState;

        public AddWorkoutDefinitionViewModel(IWorkOutDefinitionRepository workOutDefinitionRepository, IWorkoutDefinitionLibraryViewModel workoutDefinitionLibraryViewModel, IUserInterfaceState userInterfaceState)
        {
            _workOutDefinitionRepository = workOutDefinitionRepository;
            _workoutDefinitionLibraryViewModel = workoutDefinitionLibraryViewModel;
            _userInterfaceState = userInterfaceState;
            NewWorkoutDefinitionViewModel = App.Container.Resolve<IWorkoutDefinitionViewModel>();
            AddWorkoutDefinition = new RelayCommand(AddWorkoutDefinitionExecute);
        }

        public IWorkoutDefinitionViewModel NewWorkoutDefinitionViewModel { get; set; }

        public ICommand AddWorkoutDefinition { get; }

        private void AddWorkoutDefinitionExecute()
        {
            _workOutDefinitionRepository.AddWorkOutDefinition(NewWorkoutDefinitionViewModel);
            _workoutDefinitionLibraryViewModel.WorkOutDefinitions.Add(NewWorkoutDefinitionViewModel);

            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.WorkoutDefinitionLibraryView);
        }
    }
}
