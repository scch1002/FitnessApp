using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Model.Interface;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.ViewModel
{
    public class CreateNextSessionViewModel : ICreateNextSessionViewModel
    {
        private readonly ISessionLogViewModel _sessionLogViewModel;
        private readonly ISessionRepository _sessionRepository;
        private readonly IScheduleViewModel _scheduleViewModel;
        private readonly IUserInterfaceState _userInterfaceState;

        public CreateNextSessionViewModel(ISessionLogViewModel sessionLogViewModel, IScheduleViewModel scheduleViewModel, ISessionRepository sessionRepository, IUserInterfaceState userInterfaceState)
        {
            _sessionLogViewModel = sessionLogViewModel;
            _sessionRepository = sessionRepository;
            _scheduleViewModel = scheduleViewModel;
            _userInterfaceState = userInterfaceState;
            CreateNextSession = new RelayCommand(CreateNextSessionExecute);
        }

        public IEnumerable<ISessionDefinitionViewModel> SessionDefinitions => _scheduleViewModel.Sessions;

        public ISessionDefinitionViewModel SelectedSessionDefinition { get; set; }

        public ICommand CreateNextSession { get; }

        private void CreateNextSessionExecute()
        {
            if (SelectedSessionDefinition == null)
            {
                return;
            }

            CreateSession();

            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.Main);
        }

        private void CreateSession()
        {
            var session = App.Container.Resolve<ISessionViewModel>();
            session.SessionDefinitionId = SelectedSessionDefinition.SessionDefinitonId;
            session.SessionName = SelectedSessionDefinition.SessionName;
            session.SessionDate = DateTime.Now;

            foreach (var workout in SelectedSessionDefinition.WorkOutDefinitions)
            {
                session.SessionWorkOuts.Add(CreateWorkOut(workout));
            }

            _sessionRepository.AddSession(session);
            _sessionLogViewModel.Sessions.Add(session);
        }

        private static IWorkoutViewModel CreateWorkOut(IWorkoutAssignment assignment)
        {
            var workoutDefintion = assignment.WorkOutDefinition;

            var sets = CreateWarmUpSetsFromWorkOutDefinition(workoutDefintion);

            sets.AddRange(CreateSetsFromWorkOutDefinition(workoutDefintion));

            var workoutViewModel = App.Container.Resolve<IWorkoutViewModel>();

            workoutViewModel.WorkOutName = workoutDefintion.WorkOutName;
            workoutViewModel.WorkOutDefinitionId = workoutDefintion.WorkOutId;
            workoutViewModel.WorkOutType = assignment.WorkoutType;
            foreach (var set in sets)
            {
                workoutViewModel.WorkOutSets.Add(set);
            }
            return workoutViewModel;
        }

        private static List<ISetViewModel> CreateWarmUpSetsFromWorkOutDefinition(IWorkoutDefinitionViewModel workOutDefinition)
        {
            var warmUpWorkOutSets = new List<ISetViewModel>();

            for (var count = 0; count < workOutDefinition.NumberOfWarmUpSets; count++)
            {
                var set = App.Container.Resolve<ISetViewModel>();

                set.SetName = "Set " + (count + 1);
                set.SetType = WorkOutAssignment.WorkOutTypes.WarmUpWorkout;
                set.Weight = workOutDefinition.WarmUpWeight;
                set.CompletedRepetitions = 0;
                set.TotalRepetitions = workOutDefinition.WarmUpRepetitions;

                warmUpWorkOutSets.Add(set);
            }

            return warmUpWorkOutSets;
        }

        private static List<ISetViewModel> CreateSetsFromWorkOutDefinition(IWorkoutDefinitionViewModel workOutDefinition)
        {
            var workOutSets = new List<ISetViewModel>();

            for (var count = 0; count < workOutDefinition.NumberOfSets; count++)
            {
                var set = App.Container.Resolve<ISetViewModel>();

                set.SetName = "Set " + (count + 1);
                set.SetType = WorkOutAssignment.WorkOutTypes.MainWorkout;
                set.Weight = workOutDefinition.Weight;
                set.CompletedRepetitions = 0;
                set.TotalRepetitions = workOutDefinition.Repetitions;

                workOutSets.Add(set);
            }

            return workOutSets;
        }
    }
}
