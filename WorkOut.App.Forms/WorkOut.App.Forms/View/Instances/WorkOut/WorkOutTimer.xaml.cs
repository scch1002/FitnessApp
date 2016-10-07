using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using Xamarin.Forms;

namespace WorkOut.App.Forms.View.Instances.WorkOut
{
    public partial class WorkOutTimer : ContentPage
    {
        private bool _stopTimer;
        private static TimeSpan _second = new TimeSpan(0, 0, 1);
        private TimeSpan _timeSpan;

        public WorkOutTimer(TimeSpan timespan)
        {
            InitializeComponent();

            _timeSpan = timespan;
            _stopTimer = false;

            Device.StartTimer(_second, UpdateTimer);
        }

        private bool UpdateTimer()
        {
            _timeSpan = _timeSpan.Subtract(_second);
            TimeLeftLabel.Text = _timeSpan.ToString();
            
            if(_stopTimer)
            {
                return false;
            }

            if (_timeSpan.Minutes == 0 && _timeSpan.Seconds == 0)
            {
                DependencyService.Get<ITimerAlert>().PlayAlarmSound();
                return false;
            }

            return true;
        }

        private void OnDoneClicked(object sender, EventArgs e)
        {
            _stopTimer = true;
            Navigation.PopAsync();
            Navigation.PopAsync();
        }
    }
}
