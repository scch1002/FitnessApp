using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using WorkOut.WebApp.Repositories;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View
{
    public partial class AddSessionDefinitionView : ContentPage
    {
        private SessionDefinition _sessionDefinition;
        private readonly ObservableCollection<SessionDefinition> _sessionDefinitions;

        public AddSessionDefinitionView(ObservableCollection<SessionDefinition> sessionDefinitions, SessionDefinition sessionDefinition)
        {
            InitializeComponent();
            _sessionDefinition = sessionDefinition;
            _sessionDefinitions = sessionDefinitions;
        }

        private void OnAddClicked(object sender, EventArgs e)
        {
            _sessionDefinition.SessionOrder = _sessionDefinitions.Count;
            _sessionDefinitions.Add(_sessionDefinition);
            SessionDefinitionRepository.AddSessionDefinition(_sessionDefinition);
            Navigation.PopAsync();
        }
    }
}
