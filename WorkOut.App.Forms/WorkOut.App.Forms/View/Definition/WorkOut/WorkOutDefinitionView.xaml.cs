using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View
{
    public partial class WorkOutDefinitionView : ContentPage
    {
        private readonly WorkOutDefinition _workOutDefinition;
     
        public WorkOutDefinitionView(WorkOutDefinition workOutDefinition)
        {
            InitializeComponent();
            _workOutDefinition = workOutDefinition;
            BindingContext = workOutDefinition;
            AutoIncrementWeight.SelectedIndex = _workOutDefinition.AutoIncrementStartingWeight ? 0 : 1;

            foreach (var rep in Enumerable.Range(0, 59).Select(s => s.ToString()).ToList())
            {
                MinutesPicker.Items.Add(rep);
                SecondsPicker.Items.Add(rep);
            }

            MinutesPicker.SelectedIndex = _workOutDefinition.RestTimeBetweenSets.Minutes;
            SecondsPicker.SelectedIndex = _workOutDefinition.RestTimeBetweenSets.Seconds;
        }

        private void OnUpdateClicked(object sender, EventArgs e)
        {
            _workOutDefinition.RestTimeBetweenSets = new TimeSpan(0, MinutesPicker.SelectedIndex, SecondsPicker.SelectedIndex);
            WorkOutDefinitionRepository.UpdateWorkOutDefinition(_workOutDefinition);
            Navigation.PopAsync();
        }

        private void AutoIncrementWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            _workOutDefinition.AutoIncrementStartingWeight = AutoIncrementWeight.SelectedIndex == 0;
        }
    }
}
