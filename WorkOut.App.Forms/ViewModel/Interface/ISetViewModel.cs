using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkOut.App.Forms.Model;

namespace WorkOut.App.Forms.ViewModel.Interface
{
    public interface ISetViewModel
    {
        int WorkOutId { get; set; }

        int SetId { get; set; }

        WorkOutAssignment.WorkOutTypes SetType { get; set; }

        string SetName { get; set; }

        int TotalRepetitions { get; set; }

        int CompletedRepetitions { get; set; }

        int Weight { get; set; }

        ICommand SaveSet { get; }
    }
}
