using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using WorkOut.App.Forms.View.Definition.WorkOut;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View
{
    public partial class SessionDefinitionDetailView : ContentPage
    {
        private readonly SessionDefinition _sessionDefintion;

        public SessionDefinitionDetailView(SessionDefinition sessionDefinition)
        {
            InitializeComponent();
            _sessionDefintion = sessionDefinition;
        }

        public void OnDeleteWarmUpAssignment(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            DisplayAlert("WorkOut Unassigned", "The workout has been assigned.", "Ok");
            var workOutDefinition = (WorkOutDefinition)menuItem.BindingContext;
            _sessionDefintion.SessionWarmUpWorkOuts.Remove(workOutDefinition);

            WorkOutAssignmentRepository.UnassignWorkOutDefinition(new WorkOutAssignment
            {
                WorkOutDefinitionId = workOutDefinition.WorkOutId,
                SessionDefinitionId = _sessionDefintion.SessionDefinitonId,
                WorkOutType = 1
            });
        }

        public void OnDeleteAssignment(object sender, EventArgs e)
        {
            var menuItem = ((MenuItem)sender);
            DisplayAlert("WorkOut Unassigned", "The workout has been assigned.", "Ok");
            var workOutDefinition = (WorkOutDefinition)menuItem.BindingContext;
            _sessionDefintion.SessionWorkOuts.Remove(workOutDefinition);

            WorkOutAssignmentRepository.UnassignWorkOutDefinition(new WorkOutAssignment
            {
                WorkOutDefinitionId = workOutDefinition.WorkOutId,
                SessionDefinitionId = _sessionDefintion.SessionDefinitonId,
                WorkOutType = 0
            });
        }

        private void OnAddWarmUpWorkOutDefinitionClicked(object sender, EventArgs e)
        {
            var addWorkOutDefinitionView = new AssignWorkOutView(_sessionDefintion, true);
            addWorkOutDefinitionView.BindingContext = new ObservableCollection<WorkOutDefinition>(WorkOutDefinitionRepository.GetWorkOutDefinitions());
            Navigation.PushAsync(addWorkOutDefinitionView);
        }

        private void OnAddWorkOutDefinitionClicked(object sender, EventArgs e)
        {
            var addWorkOutDefinitionView = new AssignWorkOutView(_sessionDefintion, false);
            addWorkOutDefinitionView.BindingContext = new ObservableCollection<WorkOutDefinition>(WorkOutDefinitionRepository.GetWorkOutDefinitions());
            Navigation.PushAsync(addWorkOutDefinitionView);
        }
    }
}
