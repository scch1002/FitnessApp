using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WorkOut.App.Forms.Model;
using Xamarin.Forms;
using WorkOut.App.Forms.Droid.PlateformDependent;
using Android.Media;

[assembly: Dependency(typeof(AndroidAlarmSound))]

namespace WorkOut.App.Forms.Droid.PlateformDependent
{
    public class AndroidAlarmSound : ITimerAlert
    {
        public void PlayAlarmSound()
        {
            MediaPlayer.Create(Android.App.Application.Context, Resource.Raw.buzztimer).Start();
        }
    }
}