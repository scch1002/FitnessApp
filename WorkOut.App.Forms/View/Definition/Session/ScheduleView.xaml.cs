using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.ViewModel.Interface;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View
{
    public partial class ScheduleView : ContentPage
    {
        private readonly IScheduleViewModel _scheduleViewModel;

        public ScheduleView(IScheduleViewModel scheduleViewModel, IWorkoutDefinitionLibraryViewModel workoutDefinition)
        {
            InitializeComponent();
            _scheduleViewModel = scheduleViewModel;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            _scheduleViewModel.SelectedSessionDefinition = (ISessionDefinitionViewModel)e.Item;

            _scheduleViewModel.EditSessionDefinition.Execute(null);
        }

        public void OnDeleteSessionClicked(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            DisplayAlert("Session Deleted", "The session has been deleted.", "Ok");
            _scheduleViewModel.SelectedSessionDefinition = (ISessionDefinitionViewModel)menuItem.BindingContext;

            _scheduleViewModel.RemoveSelectedSessionDefinition.Execute(null);
        }
    }
}
