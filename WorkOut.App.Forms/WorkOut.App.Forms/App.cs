using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.ViewModel;
using WorkOut.WebApp.Repositories;
using Xamarin.Forms;

namespace WorkOut.App.Forms
{
    public class App : Application
    {
        public App()
        {
        }

        protected override void OnStart()
        {
            DatabaseHelper.CreateWorkOutDatabase();

            var sessions = new ObservableCollection<Session>(SessionRepository.GetSessions());

            var sessionsViewModel = new SessionOverViewViewModel(sessions);
            MainPage = new NavigationPage(new SessionOverView(sessionsViewModel)
            {
                BindingContext = sessionsViewModel
            });
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
