using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using WorkOut.App.Forms.View.Instances.WorkOut;
using Xamarin.Forms;
using ModelWorkOut = WorkOut.App.Forms.Model.WorkOut;

namespace WorkOut.App.Forms.View
{
    public partial class SetView : ContentPage
    {
        private readonly ModelWorkOut _workOut;
        private readonly Set _set;

        public SetView(Set set, ModelWorkOut workOut)
        {
            InitializeComponent();

            BindingContext = set;

            _set = set;
            _workOut = workOut;

            foreach (var rep in Enumerable.Range(0, set.TotalRepetitions + 1).Select(s => s.ToString()).ToList())
            {
                RepPicker.Items.Add(rep);
            }

            RepPicker.SelectedIndex = set.CompletedRepetitions;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            SetRepository.UpdateSet(_set, _workOut);

            await Navigation.PushAsync(new WorkOutTimer(_workOut.RestTimeBetweenSets));
        }
    }
}
