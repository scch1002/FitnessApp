using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkOut.App.Forms.Model;
using WorkOut.App.Forms.Model.Interface;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.ViewModel
{
    public class SessionDefinitionViewModel : ViewModelBase, ISessionDefinitionViewModel
    {
        private readonly IWorkOutDefinitionRepository _workOutDefinitionRepository;
        private readonly IWorkOutAssignmentRepository _workOutAssignmentRepository;

        public SessionDefinitionViewModel(IWorkOutDefinitionRepository workOutDefinitionRepository, IWorkOutAssignmentRepository workOutAssignmentRepository)
        {
            _workOutDefinitionRepository = workOutDefinitionRepository;
            _workOutAssignmentRepository = workOutAssignmentRepository;
            WorkOutDefinitions = new ObservableCollection<IWorkoutAssignment>();
            WorkOutDefinitions.CollectionChanged += WorkOutDefinitions_CollectionChanged;
            RemoveSelectedWorkOutDefinition = new RelayCommand(RemoveSelectedWorkOutDefinitionExecute, CanRemoveSelectedWorkOutDefinitionExecute);
        }

        public ICommand RemoveSelectedWorkOutDefinition { get; }

        public int SessionDefinitonId { get; set; }

        public int SessionOrder { get; set; }

        private string _sessionName;
        public string SessionName
        {
            get { return _sessionName; }
            set
            {
                _sessionName = value;
                RaisePropertyChanged("SessionName");
            }
        }

        public IEnumerable<IWorkoutAssignment> WarmUpWorkOutDefinitions => WorkOutDefinitions.Where(w => w.WorkoutType == WorkOutAssignment.WorkOutTypes.WarmUpWorkout);

        public IEnumerable<IWorkoutAssignment> MainWorkOutDefinitions => WorkOutDefinitions.Where(w => w.WorkoutType == WorkOutAssignment.WorkOutTypes.MainWorkout);

        public ObservableCollection<IWorkoutAssignment> WorkOutDefinitions { get; set; }

        private IWorkoutAssignment _selectedWorkOutDefinition;
        public IWorkoutAssignment SelectedWorkOutDefinition
        {
            get { return _selectedWorkOutDefinition; }
            set
            {
                _selectedWorkOutDefinition = value;
                RaisePropertyChanged();
            }
        }

        private bool CanRemoveSelectedWorkOutDefinitionExecute()
        {
            return SelectedWorkOutDefinition != null;
        }

        private void RemoveSelectedWorkOutDefinitionExecute()
        {
            WorkOutDefinitions.Remove(SelectedWorkOutDefinition);
            _workOutAssignmentRepository.UnassignWorkOutDefinition(SelectedWorkOutDefinition);
            SelectedWorkOutDefinition = null;
        }

        private void WorkOutDefinitions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("WarmUpWorkOutDefinitions");
            RaisePropertyChanged("MainWorkOutDefinitions");
        }
    }
}
