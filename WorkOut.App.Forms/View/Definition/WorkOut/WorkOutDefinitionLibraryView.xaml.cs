using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.ViewModel.Interface;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View.WorkOut
{
    public partial class WorkOutDefinitionLibraryView : ContentPage
    {
        private readonly IWorkoutDefinitionLibraryViewModel _workOutDefinitionOverViewViewModel;

        public WorkOutDefinitionLibraryView(IWorkoutDefinitionLibraryViewModel workOutDefinitionOverViewViewModel)
        {
            InitializeComponent();
            _workOutDefinitionOverViewViewModel = workOutDefinitionOverViewViewModel;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            _workOutDefinitionOverViewViewModel.SelectedWorkoutDefinition = (IWorkoutDefinitionViewModel)e.Item;

            _workOutDefinitionOverViewViewModel.ViewWorkoutDefinition.Execute(null);
        }

        public void OnDeleteWorkoutDefinition(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            DisplayAlert("Workout Definition Removed", "The workout definition has been removed.", "Ok");
            _workOutDefinitionOverViewViewModel.SelectedWorkoutDefinition = (IWorkoutDefinitionViewModel)menuItem.BindingContext;

            _workOutDefinitionOverViewViewModel.RemoveWorkoutDefinition.Execute(null);
        }
    }
}
