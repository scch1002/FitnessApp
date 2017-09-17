using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using WorkOut.App.Forms.ViewModel.Interface;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View
{
    public partial class SetView : ContentPage
    {
        private readonly ISetViewModel _set;

        public SetView(ISetViewModel set)
        {
            InitializeComponent();

            _set = set;

            foreach (var rep in Enumerable.Range(0, set.TotalRepetitions + 1).Select(s => s.ToString()).ToList())
            {
                RepPicker.Items.Add(rep);
            }

            RepPicker.SelectedIndex = _set.CompletedRepetitions;
        }
    }
}
