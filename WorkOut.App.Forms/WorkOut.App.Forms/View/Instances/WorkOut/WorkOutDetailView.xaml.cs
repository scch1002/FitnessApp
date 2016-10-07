using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using WorkOut.WebApp.Repositories;
using Xamarin.Forms;
using ModelWorkOut = WorkOut.App.Forms.Model.WorkOut;

namespace WorkOut.App.Forms.View
{
    public partial class WorkOutDetailView : ContentPage
    {
        private readonly ModelWorkOut _workOut;
        private readonly Session _session;

        public WorkOutDetailView(ModelWorkOut workOut, Session session)
        {
            InitializeComponent();
            BindingContext = workOut;
            _workOut = workOut;
            _session = session;
            WorkCompleteButton.IsEnabled = !_workOut.WorkOutComplete;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            await Navigation.PushAsync(new SetView((Set)e.Item, _workOut));
        }

        private void OnWorkOutCompleteClicked(object sender, EventArgs e)
        {
            if (UpdateWorkOutDefinitionStartingWeight(_workOut, _session))
            {
                DisplayAlert("Auto Incremeted Starting Weight", "The weight for this Workout has been incremented.", "Ok");
            }
            _workOut.WorkOutComplete = true;
            WorkOutRepository.UpdateWorkOut(_workOut, _session);
            Navigation.PopAsync();
        }

        private static bool UpdateWorkOutDefinitionStartingWeight(ModelWorkOut workOut, Session session)
        {
            if (AllSetsCompleted(workOut))
            {
                var workOutDefinition = WorkOutDefinitionRepository.GetWorkOutDefinition(workOut.WorkOutDefinitionId);
                if (workOutDefinition == null)
                {
                    return false;
                }
                if (workOutDefinition.AutoIncrementStartingWeight)
                {
                    workOutDefinition.WarmUpWeight += workOutDefinition.WarmUpWeightIncrement;
                    workOutDefinition.Weight += workOutDefinition.WeightIncrement;
                    WorkOutDefinitionRepository.UpdateWorkOutDefinition(workOutDefinition);
                    WorkOutRepository.UpdateWorkOut(workOut, session);
                    return true;
                }
            }
            return false;
        }

        private static bool AllSetsCompleted(ModelWorkOut workOut)
        {
            return workOut.WorkOutSets.All(a => a.CompletedRepetitions == a.TotalRepetitions);
        }
    }
}
