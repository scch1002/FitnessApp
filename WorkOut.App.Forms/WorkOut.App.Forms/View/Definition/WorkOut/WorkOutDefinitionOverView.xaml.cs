using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.ViewModel;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View.Definition.WorkOut
{
    public partial class WorkOutDefinitionOverView : ContentPage
    {
        private readonly WorkOutDefinitionOverViewViewModel _workOutDefinitionOverViewViewModel;

        public WorkOutDefinitionOverView(WorkOutDefinitionOverViewViewModel workOutDefinitionOverViewViewModel)
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

            var workOutDefinition = (WorkOutDefinition)e.Item;
            var workOutDefinitionView = new WorkOutDefinitionView(workOutDefinition);
            workOutDefinitionView.BindingContext = workOutDefinition;
            await Navigation.PushAsync(workOutDefinitionView);
        }

        public void OnAddWorkOutDefinitionClicked(object sender, EventArgs e)
        {
            var workoutDefinition = new WorkOutDefinition();
            var workOutDefinitionView = new AddWorkOutDefinitionView(_workOutDefinitionOverViewViewModel.WorkOutDefinitions, workoutDefinition);
            workOutDefinitionView.BindingContext = workoutDefinition;
            Navigation.PushAsync(workOutDefinitionView);
        }
    }
}
