using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ModelWorkOut = WorkOut.App.Forms.Model.WorkOut;

namespace WorkOut.App.Forms.Model
{
    public class Session : ViewModelBase
    {
        public int SessionId { get; set; }

        public int SessionDefinitionId { get; set; }

        public string SessionName { get; set; }

        public DateTime SessionDate { get; set; }

        public ObservableCollection<ModelWorkOut> SessionWarmUpWorkOuts { get; set; }

        public ObservableCollection<ModelWorkOut> SessionWorkOuts { get; set; }
    }
}