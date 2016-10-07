using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using WorkOut.App.Forms.Service;
using WorkOut.App.Forms.View;
using WorkOut.App.Forms.View.Instances.Session;
using WorkOut.App.Forms.ViewModel;
using WorkOut.WebApp.Repositories;
using Xamarin.Forms;
using ModelWorkOut = WorkOut.App.Forms.Model.WorkOut;

namespace WorkOut.App.Forms
{
    public partial class SessionOverView : ContentPage
    {
        private SessionOverViewViewModel _sessions;

        public SessionOverView(SessionOverViewViewModel sessionOverViewViewModel)
        {
            InitializeComponent();
            _sessions = sessionOverViewViewModel;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var session = (Session)e.Item;

            session.SessionWarmUpWorkOuts = new ObservableCollection<ModelWorkOut>(WorkOutRepository.GetWorkOuts(session, 1));

            session.SessionWorkOuts = new ObservableCollection<ModelWorkOut>(WorkOutRepository.GetWorkOuts(session, 0));

            await Navigation.PushAsync(new SessionDetailView(session));
        }

        private async void OnNextSessionClicked(object sender, EventArgs e)
        {
            var selectNextSessionView = new SelectNextSessionView(_sessions.Sessions);
            selectNextSessionView.BindingContext = new ObservableCollection<SessionDefinition>(SessionDefinitionRepository.GetSessionDefinitions());

            await Navigation.PushAsync(selectNextSessionView);
        }

        private async void OnEditScheduleClicked(object sender, EventArgs e)
        {
            var sessionDefinitionOverViewViewModel = new SessionDefinitionOverViewViewModel();
            var sessionDefinitionOverView = new SessionDefinitionOverView(sessionDefinitionOverViewViewModel);
            sessionDefinitionOverView.BindingContext = sessionDefinitionOverViewViewModel;

            await Navigation.PushAsync(sessionDefinitionOverView);
        }

        public void OnDeleteSessionClicked(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            DisplayAlert("Session Deleted", "The session has been deleted.", "Ok");
            var session = (Session)menuItem.BindingContext;
            _sessions.Sessions.Remove(session);

            if (session.SessionWorkOuts != null)
            {
                foreach (var workOut in session.SessionWorkOuts)
                {
                    if (workOut.WorkOutSets != null)
                    {
                        foreach(var set in workOut.WorkOutSets)
                        {
                            SetRepository.DeleteSet(set);
                        }
                    }

                    WorkOutRepository.DeleteWorkOut(workOut);
                }
            }

            SessionRepository.DeleteSession(session);
        }
    }
}
