using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using WorkOut.App.Forms.ViewModel.Interface;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View
{
    public partial class SessionDefinitionView : ContentPage
    {
        private readonly ISessionDefinitionViewModel _sessionDefintion;
        private readonly IWorkoutDefinitionLibraryViewModel _workOutDefinitionLibraryViewModel;
        private readonly IUserInterfaceState _userInterfaceState;

        public SessionDefinitionView(ISessionDefinitionViewModel sessionDefinition, IWorkoutDefinitionLibraryViewModel workOutDefinitionLibraryViewModel, IUserInterfaceState userInterfaceState)
        {
            InitializeComponent();
            _sessionDefintion = sessionDefinition;
            _workOutDefinitionLibraryViewModel = workOutDefinitionLibraryViewModel;
            _userInterfaceState = userInterfaceState;
        }

        public void OnDeleteWarmUpAssignment(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            DisplayAlert("WorkOut Unassigned", "The workout has been assigned.", "Ok");
            _sessionDefintion.SelectedWorkOutDefinition = (WorkOutAssignment)menuItem.BindingContext;

            _sessionDefintion.RemoveSelectedWorkOutDefinition.Execute(null);
        }

        public void OnDeleteAssignment(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            DisplayAlert("WorkOut Unassigned", "The workout has been assigned.", "Ok");
            _sessionDefintion.SelectedWorkOutDefinition = (WorkOutAssignment)menuItem.BindingContext;

            _sessionDefintion.RemoveSelectedWorkOutDefinition.Execute(null);
        }

        private void OnAddWarmUpWorkOutDefinitionClicked(object sender, EventArgs e)
        {
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.AssignWorkoutDefinition, WorkOutAssignment.WorkOutTypes.WarmUpWorkout);
        }

        private void OnAddWorkOutDefinitionClicked(object sender, EventArgs e)
        {
            _userInterfaceState.ChangeUserInterfaceState(UserInterfaceStates.AssignWorkoutDefinition, WorkOutAssignment.WorkOutTypes.MainWorkout);
        }
    }
}
