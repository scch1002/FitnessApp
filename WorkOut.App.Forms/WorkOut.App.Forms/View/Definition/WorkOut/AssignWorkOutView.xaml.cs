using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View.Definition.WorkOut
{
    public partial class AssignWorkOutView : ContentPage
    {
        private readonly SessionDefinition _sessionDefinition;
        private readonly bool _workOutType;

        public AssignWorkOutView(SessionDefinition sessionDefinition, bool workOutType)
        {
            InitializeComponent();
            _sessionDefinition = sessionDefinition;
            _workOutType = workOutType;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var workOutDefinition = (WorkOutDefinition)e.Item;

            WorkOutAssignmentRepository.AssignWorkOutDefinition(new WorkOutAssignment
            {
                WorkOutDefinitionId = workOutDefinition.WorkOutId,
                SessionDefinitionId = _sessionDefinition.SessionDefinitonId,
                WorkOutType = _workOutType ? 1 : 0
            });

            if (_workOutType)
            {
                _sessionDefinition.SessionWarmUpWorkOuts.Add(workOutDefinition);
            }
            else
            {
                _sessionDefinition.SessionWorkOuts.Add(workOutDefinition);
            }            

            await Navigation.PopAsync();
        }
    }
}
