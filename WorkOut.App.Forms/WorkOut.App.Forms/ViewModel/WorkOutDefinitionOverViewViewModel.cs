using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Repository;

namespace WorkOut.App.Forms.ViewModel
{
    public class WorkOutDefinitionOverViewViewModel
    {
        public WorkOutDefinitionOverViewViewModel()
        {
            WorkOutDefinitions = new ObservableCollection<WorkOutDefinition>(WorkOutDefinitionRepository.GetWorkOutDefinitions());
        }

        public ObservableCollection<WorkOutDefinition> WorkOutDefinitions { get; set; }
    }
}
