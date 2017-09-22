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
    public partial class SessionDetailView : ContentPage
    {
        private readonly ISessionViewModel _session;

        public SessionDetailView(ISessionLogViewModel sessionLogViewModel)
        {
            InitializeComponent();
            _session = sessionLogViewModel.SelectedSession;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            _session.SelectedWorkout = (IWorkoutViewModel)e.Item;

            _session.ViewSelectedWorkout.Execute(null);
        }
    }
}
