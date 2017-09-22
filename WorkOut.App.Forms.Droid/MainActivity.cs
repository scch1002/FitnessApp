using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;

namespace WorkOut.App.Forms.Droid
{
    [Activity(Label = "Fitness Prototype", Icon = "@drawable/ic_launcher", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //var sqliteFilename = "WorkOutSQLite.db3";
            //string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //var path = Path.Combine(documentsPath, sqliteFilename);

            //File.Delete(path);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

