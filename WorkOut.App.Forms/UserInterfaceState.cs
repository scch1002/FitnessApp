using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using WorkOut.App.Forms.ViewModel;
using Microsoft.Practices.Unity;
using WorkOut.App.Forms.View;
using WorkOut.App.Forms.View.WorkOut;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms
{
    public class UserInterfaceState : IUserInterfaceState
    {
        private readonly Stack<UserInterfaceStates> _userInterfaceStack;
        private NavigationPage _navigation;
        
        public UserInterfaceState()
        {
            _userInterfaceStack = new Stack<UserInterfaceStates>();
        }

        public App Application { get; set; }

        public async void ChangeUserInterfaceState(UserInterfaceStates newState, object paramater = null)
        {
            if (newState == UserInterfaceStates.Main && _userInterfaceStack.Count == 1)
            {
                Application.MainPage = _navigation;
                return;
            }

            if (_userInterfaceStack.Contains(newState))
            {
                PopToState(newState);
                return;
            }

            _userInterfaceStack.Push(newState);

            switch (newState)
            {
                case UserInterfaceStates.AddWorkoutDefinition:
                    var addworkoutDefinitionLibrary = App.Container.Resolve<IWorkoutDefinitionLibraryViewModel>();
                    var addWorkoutDefinition = App.Container.Resolve<IAddWorkoutDefinitionViewModel>();
                    var addWorkoutDefinitionView = new AddWorkOutDefinitionView();
                    addWorkoutDefinitionView.BindingContext = addWorkoutDefinition;
                    await _navigation.PushAsync(addWorkoutDefinitionView);
                    break;
                case UserInterfaceStates.ViewWorkoutDefinition:
                    var workoutDefinitionLibrary = App.Container.Resolve<IWorkoutDefinitionLibraryViewModel>();
                    var selectedWorkoutDefinitionView = new WorkOutDefinitionView();
                    selectedWorkoutDefinitionView.BindingContext = workoutDefinitionLibrary.SelectedWorkoutDefinition;
                    await _navigation.PushAsync(selectedWorkoutDefinitionView);
                    break;
                case UserInterfaceStates.WorkoutView:
                    var selectedWorkout = App.Container.Resolve<ISessionLogViewModel>()
                        .SelectedSession
                        .SelectedWorkout;
                    var selectedWorkoutView = new WorkOutDetailView(selectedWorkout);
                    selectedWorkoutView.BindingContext = selectedWorkout;
                    await _navigation.PushAsync(selectedWorkoutView);
                    break;
                case UserInterfaceStates.SetView:
                    var setSessionLogViewModel = App.Container.Resolve<ISessionLogViewModel>();
                    var selectedSet = setSessionLogViewModel.SelectedSession.SelectedWorkout.SelectedSet;
                    var setView = new SetView(selectedSet);
                    setView.BindingContext = selectedSet;
                    await _navigation.PushAsync(setView);
                    break;
                case UserInterfaceStates.AssignWorkoutDefinition:
                    var assignWorkoutViewModel = App.Container.Resolve<IAssignWorkoutDefinitionViewModel>();
                    var assignWorkoutView = new AssignWorkOutView(assignWorkoutViewModel);                   
                    assignWorkoutViewModel.WorkOutType = (WorkOutAssignment.WorkOutTypes)paramater;
                    assignWorkoutView.BindingContext = assignWorkoutViewModel;
                    await _navigation.PushAsync(assignWorkoutView);
                    break;
                case UserInterfaceStates.WorkoutDefinitionLibraryView:
                    var workOutDefinitionLibraryViewModel = App.Container.Resolve<IWorkoutDefinitionLibraryViewModel>();
                    var workoutDefinitionView = new WorkOutDefinitionLibraryView(workOutDefinitionLibraryViewModel);
                    workoutDefinitionView.BindingContext = workOutDefinitionLibraryViewModel;
                    await _navigation.PushAsync(workoutDefinitionView);
                    break;
                case UserInterfaceStates.SelectNextSession:
                    var createNextSession = App.Container.Resolve<ICreateNextSessionViewModel>();
                    var selectNextSessionView = new SelectNextSessionView(createNextSession);
                    selectNextSessionView.BindingContext = createNextSession;
                    await _navigation.PushAsync(selectNextSessionView);
                    break;
                case UserInterfaceStates.SessionView:
                    var sessionLogViewModel = App.Container.Resolve<ISessionLogViewModel>();
                    var view = new SessionDetailView(sessionLogViewModel);
                    view.BindingContext = sessionLogViewModel.SelectedSession;
                    await _navigation.PushAsync(view);     
                    break;
                case UserInterfaceStates.ScheduleView:
                    var scheduleViewModel = App.Container.Resolve<IScheduleViewModel>();
                    var workoutDefinitionLibraryViewModel = App.Container.Resolve<IWorkoutDefinitionLibraryViewModel>();
                    var scheduleView = new ScheduleView(scheduleViewModel, workoutDefinitionLibraryViewModel);
                    scheduleView.BindingContext = scheduleViewModel;
                    await _navigation.PushAsync(scheduleView);
                    break;
                case UserInterfaceStates.ScheduleAdd:
                    var addSessionDefinitionViewModel = App.Container.Resolve<IAddSessionDefinitionViewModel>();
                    var addview = new AddSessionDefinitionView();
                    addview.BindingContext = addSessionDefinitionViewModel;
                    await _navigation.PushAsync(addview);
                    break;
                case UserInterfaceStates.SessionDefinitionView:
                    var s = App.Container.Resolve<IScheduleViewModel>();
                    var e = App.Container.Resolve<IWorkoutDefinitionLibraryViewModel>();
                    var userInterface = App.Container.Resolve<IUserInterfaceState>();
                    var definitionView = new SessionDefinitionView(s.SelectedSessionDefinition, e, userInterface);
                    definitionView.BindingContext = s.SelectedSessionDefinition;
                    await _navigation.PushAsync(definitionView);
                    break;
                default:
                    var sessionLog = App.Container.Resolve<ISessionLogViewModel>();
                    var viewMain = new SessionLogView(sessionLog);
                    viewMain.BindingContext = sessionLog;
                    _navigation = new NavigationPage(viewMain);
                    _navigation.Popped += _navigation_Popped;
                    _navigation.PoppedToRoot += _navigation_PoppedToRoot;
                    Application.MainPage = _navigation;
                    break;
            }
        }

        private void _navigation_PoppedToRoot(object sender, NavigationEventArgs e)
        {
            while(_userInterfaceStack.Peek() != UserInterfaceStates.Main)
            {
                _userInterfaceStack.Pop();
            }
        }

        private void _navigation_Popped(object sender, NavigationEventArgs e)
        {
            if (_userInterfaceStack.Peek() == UserInterfaceStates.Main)
            {
                return;
            }

            _userInterfaceStack.Pop();
        }

        private async void PopToState(UserInterfaceStates state)
        {
            while(_userInterfaceStack.Peek() != state)
            {
                await _navigation.PopAsync();
            }
        }
    }
}
