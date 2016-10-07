using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using WorkOut.App.Forms.View.Definition.WorkOut;
using WorkOut.App.Forms.ViewModel;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View
{
    public partial class SessionDefinitionOverView : ContentPage
    {
        private readonly SessionDefinitionOverViewViewModel _sessionDefinitionOverViewViewModel;

        public SessionDefinitionOverView(SessionDefinitionOverViewViewModel sessionDefinitionOverViewViewModel)
        {
            InitializeComponent();
            _sessionDefinitionOverViewViewModel = sessionDefinitionOverViewViewModel;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var sessionDefinition = (SessionDefinition)e.Item;

            var workOutWarmUpDefinitions = WorkOutDefinitionRepository.GetWorkOutDefinitions(sessionDefinition.SessionDefinitonId, 1);

            var workOutDefinitions = WorkOutDefinitionRepository.GetWorkOutDefinitions(sessionDefinition.SessionDefinitonId, 0);

            sessionDefinition.SessionWarmUpWorkOuts = new ObservableCollection<WorkOutDefinition>(workOutWarmUpDefinitions);

            sessionDefinition.SessionWorkOuts = new ObservableCollection<WorkOutDefinition>(workOutDefinitions);

            var sessionDefinitionDetailView = new SessionDefinitionDetailView(sessionDefinition);
            sessionDefinitionDetailView.BindingContext = sessionDefinition;

            await Navigation.PushAsync(sessionDefinitionDetailView);
        }

        private async void OnAddSessionClicked(object sender, EventArgs e)
        {
            var sessionDefinition = new SessionDefinition();
            var addSessionDefinitionView = new AddSessionDefinitionView(_sessionDefinitionOverViewViewModel.Sessions, sessionDefinition);
            addSessionDefinitionView.BindingContext = sessionDefinition;
            await Navigation.PushAsync(addSessionDefinitionView);
        }

        private void OnEditWorkOutDefinitions(object sender, EventArgs e)
        {
            var workOutDefinitionOverViewViewModel = new WorkOutDefinitionOverViewViewModel();
            var workOutDefinitionOverView = new WorkOutDefinitionOverView(workOutDefinitionOverViewViewModel);
            workOutDefinitionOverView.BindingContext = workOutDefinitionOverViewViewModel;

            Navigation.PushAsync(workOutDefinitionOverView);
        }

        public void OnDeleteSessionClicked(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            DisplayAlert("Session Deleted", "The session has been deleted.", "Ok");
            var sessionDefinition = (SessionDefinition)menuItem.BindingContext;
            _sessionDefinitionOverViewViewModel.Sessions.Remove(sessionDefinition);
            
            if (sessionDefinition.SessionWorkOuts != null)
            {
                foreach(var workOutDefinition in sessionDefinition.SessionWorkOuts)
                {
                    WorkOutDefinitionRepository.DeleteWorkOutDefinition(workOutDefinition);
                }
            }            

            SessionDefinitionRepository.DeleteSessionDefinition(sessionDefinition);

            RenumberSessionDefinitionsAfterDelete(sessionDefinition.SessionOrder);
        }

        private void OnMoveSessionUpClicked(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            var sessionDefinition = (SessionDefinition)menuItem.BindingContext;
            var destination = _sessionDefinitionOverViewViewModel.Sessions.FirstOrDefault(f => f.SessionOrder == sessionDefinition.SessionOrder - 1);
            if (destination != null)
            {
                var indexOfDestination = _sessionDefinitionOverViewViewModel.Sessions.IndexOf(destination);
                var indexOfSource = _sessionDefinitionOverViewViewModel.Sessions.IndexOf(sessionDefinition);
                _sessionDefinitionOverViewViewModel.Sessions[indexOfSource] = destination;
                _sessionDefinitionOverViewViewModel.Sessions[indexOfDestination] = sessionDefinition;
                SwapSessionOrderPositions(sessionDefinition, destination);
            }
        }

        private void OnMoveSessionDownClicked(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            var sessionDefinition = (SessionDefinition)menuItem.BindingContext;
            var destination = _sessionDefinitionOverViewViewModel.Sessions.FirstOrDefault(f => f.SessionOrder == sessionDefinition.SessionOrder + 1);
            if (destination != null)
            {
                var indexOfDestination = _sessionDefinitionOverViewViewModel.Sessions.IndexOf(destination);
                var indexOfSource = _sessionDefinitionOverViewViewModel.Sessions.IndexOf(sessionDefinition);
                _sessionDefinitionOverViewViewModel.Sessions[indexOfSource] = destination;
                _sessionDefinitionOverViewViewModel.Sessions[indexOfDestination] = sessionDefinition;
                SwapSessionOrderPositions(sessionDefinition, destination);
            }
        }

        private void RenumberSessionDefinitionsAfterDelete(int sessionDefinitionOrderNumber)
        {
            foreach(var sessionDefinition in _sessionDefinitionOverViewViewModel
                .Sessions
                .Where(w => w.SessionOrder > sessionDefinitionOrderNumber))
            {
                sessionDefinition.SessionOrder--;
                SessionDefinitionRepository.UpdateSessionDefinition(sessionDefinition);
            }            
        }

        private void SwapSessionOrderPositions(SessionDefinition source, SessionDefinition destination)
        {
            var sessionOrder = source.SessionOrder;
            source.SessionOrder = destination.SessionOrder;
            destination.SessionOrder = sessionOrder;
            SessionDefinitionRepository.UpdateSessionDefinition(source);
            SessionDefinitionRepository.UpdateSessionDefinition(destination);
        }
    }
}
