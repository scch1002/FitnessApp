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
using ModelWorkOut = WorkOut.App.Forms.Model.WorkOut;

namespace WorkOut.App.Forms.View
{
    public partial class SessionDetailView : ContentPage
    {
        private readonly Session _session;

        public SessionDetailView(Session session)
        {
            InitializeComponent();
            _session = session;
            BindingContext = session;
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var workOut = (ModelWorkOut)e.Item;

            var workOutSets = SetRepository.GetSets(workOut.WorkOutId).ToList();

            workOut.WorkOutWarmUpSets = new ObservableCollection<Set>(workOutSets.Where(w => w.SetType == 1));
            workOut.WorkOutSets = new ObservableCollection<Set>(workOutSets.Where(w => w.SetType == 0));

            await Navigation.PushAsync(new WorkOutDetailView(workOut, _session));
        }
    }
}
