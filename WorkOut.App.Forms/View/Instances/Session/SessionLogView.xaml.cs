using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using WorkOut.App.Forms.View;
using WorkOut.App.Forms.ViewModel.Interface;
using Xamarin.Forms;

namespace WorkOut.App.Forms
{
    public partial class SessionLogView : ContentPage
    {
        private ISessionLogViewModel _sessionLogViewModel;

        public SessionLogView(ISessionLogViewModel sessionLogViewModel)
        {
            InitializeComponent();
            _sessionLogViewModel = sessionLogViewModel;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            _sessionLogViewModel.SelectedSession = (ISessionViewModel)e.Item;

            _sessionLogViewModel.EditSelectedSession.Execute(null);     
        }

        public void OnDeleteSessionClicked(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            DisplayAlert("Session Deleted", "The session has been deleted.", "Ok");

            _sessionLogViewModel.SelectedSession = (ISessionViewModel)menuItem.BindingContext;

            _sessionLogViewModel.RemoveSelectedSession.Execute(null);
        }
    }
}
