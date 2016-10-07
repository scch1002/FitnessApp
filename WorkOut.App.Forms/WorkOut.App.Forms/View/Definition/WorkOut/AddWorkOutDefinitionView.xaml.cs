using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View
{
    public partial class AddWorkOutDefinitionView : ContentPage
    {
        private readonly ObservableCollection<WorkOutDefinition> _workOutDefinitions;
        private readonly WorkOutDefinition _workOutDefinition;

        public AddWorkOutDefinitionView(ObservableCollection<WorkOutDefinition> workOutDefinitions, WorkOutDefinition workOutDefinition)
        {
            InitializeComponent();
            _workOutDefinition = workOutDefinition;
            _workOutDefinitions = workOutDefinitions;
            AutoIncrementWeight.SelectedIndex = 0;
            _workOutDefinition.AutoIncrementStartingWeight = true;

            foreach (var rep in Enumerable.Range(0, 59).Select(s => s.ToString()).ToList())
            {
                MinutesPicker.Items.Add(rep);
                SecondsPicker.Items.Add(rep);
            }
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            _workOutDefinition.RestTimeBetweenSets = new TimeSpan(0, MinutesPicker.SelectedIndex, SecondsPicker.SelectedIndex);
            
            WorkOutDefinitionRepository.AddWorkOutDefinition(_workOutDefinition);
            _workOutDefinitions.Add(_workOutDefinition);
            Navigation.PopAsync();
        }

        private void AutoIncrementWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            _workOutDefinition.AutoIncrementStartingWeight = AutoIncrementWeight.SelectedIndex == 0;
        }
    }
}
