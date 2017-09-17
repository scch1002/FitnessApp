using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Repository;
using Xamarin.Forms;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.View
{
    public partial class SelectNextSessionView : ContentPage
    {
        private readonly ICreateNextSessionViewModel _createNextSessionViewModel;

        public SelectNextSessionView(ICreateNextSessionViewModel createNextSessionViewModel)
        {
            InitializeComponent();
            _createNextSessionViewModel = createNextSessionViewModel;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            _createNextSessionViewModel.SelectedSessionDefinition = (ISessionDefinitionViewModel)e.Item;

            _createNextSessionViewModel.CreateNextSession.Execute(null);
        }
    }
}
