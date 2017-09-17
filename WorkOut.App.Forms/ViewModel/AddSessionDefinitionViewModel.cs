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
    public class AddSessionDefinitionViewModel : IAddSessionDefinitionViewModel
    {
        private readonly ISessionDefinitionRepository _sessionDefinitionRepository;
        private readonly IScheduleViewModel _scheduleViewModel;
        private readonly IUserInterfaceState _userInterfaceState;

        public AddSessionDefinitionViewModel(ISessionDefinitionRepository sessionDefinitionRepository, IScheduleViewModel scheduleViewModel, IUserInterfaceState userInterfaceState)
        {
            NewSessionDefinitionViewModel = App.Container.Resolve<ISessionDefinitionViewModel>();
            _sessionDefinitionRepository = sessionDefinitionRepository;
            _scheduleViewModel = scheduleViewModel;
            _userInterfaceState = userInterfaceState;
            AddSessionDefinition = new RelayCommand(AddSessionDefinitionExecute);
        }

        public ISessionDefinitionViewModel NewSessionDefinitionViewModel { get; set; }

        public ICommand AddSessionDefinition { get; }

        private void AddSessionDefinitionExecute()
        {
            _sessionDefinitionRepository.AddSessionDefinition(NewSessionDefinitionViewModel);
            _scheduleViewModel.Sessions.Add(NewSessionDefinitionViewModel);
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.ScheduleView);
        }
    }
}
