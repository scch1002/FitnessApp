using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.ViewModel.Interface;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View
{
    public partial class AssignWorkOutView : ContentPage
    {
        private readonly IAssignWorkoutDefinitionViewModel _assignWorkoutDefinitionViewModel;

        public AssignWorkOutView(IAssignWorkoutDefinitionViewModel assignWorkoutDefinitionViewModel)
        {
            InitializeComponent();
            _assignWorkoutDefinitionViewModel = assignWorkoutDefinitionViewModel;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            _assignWorkoutDefinitionViewModel.SelectedWorkoutDefinition = (IWorkoutDefinitionViewModel) e.Item;

            _assignWorkoutDefinitionViewModel.AssignWorkoutDefinition.Execute(null);
        }
    }
}
