using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.ViewModel.Interface;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View
{
    public partial class WorkOutDetailView : ContentPage
    {
        private readonly IWorkoutViewModel _workout;

        public WorkOutDetailView(IWorkoutViewModel workout)
        {
            InitializeComponent();
            _workout = workout;
            WorkCompleteButton.IsEnabled = !_workout.WorkOutComplete;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            _workout.SelectedSet = (ISetViewModel) e.Item;

            _workout.ViewSelectedSet.Execute(null);
        }

        private void OnWorkOutCompleteClicked(object sender, EventArgs e)
        {
            _workout.SaveWorkout.Execute(null);
        }
    }
}
