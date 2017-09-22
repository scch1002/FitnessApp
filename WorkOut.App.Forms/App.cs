using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WorkOut.App.Forms.DataModel;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;
using WorkOut.App.Forms.ViewModel;
using Xamarin.Forms;

namespace WorkOut.App.Forms
{
    public class App : Application
    {
        public static IAppContainer Container { get; set; } = new AppContainer();

        protected override void OnStart()
        {
            DatabaseHelper.CreateWorkOutDatabase();

            Container.Resolve<IUserInterfaceState>().Application = this;

            Container.Resolve<IUserInterfaceState>()
                .ChangeUserInterfaceState(UserInterfaceStates.Main);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
