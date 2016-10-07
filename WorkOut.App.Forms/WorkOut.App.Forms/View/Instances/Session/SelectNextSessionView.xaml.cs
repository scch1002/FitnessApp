using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Repository;
using WorkOut.App.Forms.Service;
using WorkOut.WebApp.Repositories;
using Xamarin.Forms;
using WorkOut.App.Forms.Model;

namespace WorkOut.App.Forms.View.Instances.Session
{
    public partial class SelectNextSessionView : ContentPage
    {
        private readonly ObservableCollection<Model.Session> _sessions;

        public SelectNextSessionView(ObservableCollection<Model.Session> sessions)
        {
            InitializeComponent();
            _sessions = sessions;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var sessionDefinition = (SessionDefinition)e.Item;

            var session = ScheduleService.CreateNextSession(sessionDefinition);
            if (session == null)
            {
                DisplayAlert("There are no session defintions", "There are no session definitions", "Ok");
                return;
            }
            SessionRepository.AddSession(session);

            foreach (var workout in session.SessionWarmUpWorkOuts)
            {
                WorkOutRepository.AddWorkOut(workout, session);

                foreach (var set in workout.WorkOutWarmUpSets)
                {
                    SetRepository.AddSet(set, workout);
                }

                foreach (var set in workout.WorkOutSets)
                {
                    SetRepository.AddSet(set, workout);
                }
            }

            foreach (var workout in session.SessionWorkOuts)
            {
                WorkOutRepository.AddWorkOut(workout, session);

                foreach (var set in workout.WorkOutWarmUpSets)
                {
                    SetRepository.AddSet(set, workout);
                }

                foreach (var set in workout.WorkOutSets)
                {
                    SetRepository.AddSet(set, workout);
                }
            }

            _sessions.Add(session);

            await Navigation.PopAsync();
        }
    }
}
