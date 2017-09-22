using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOut.App.Forms.Repository;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.View;
using WorkOut.App.Forms.ViewModel;
using WorkOut.App.Forms.ViewModel.Interface;
using Xamarin.Forms;

namespace WorkOut.App.Forms
{
    public class AppContainer : IAppContainer
    {
        private readonly UnityContainer _container;

        public AppContainer()
        {
            _container = new UnityContainer();
            Register();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        private void Register()
        {
            _container.RegisterType<ISessionDefinitionRepository, SessionDefinitionRepository>();
            _container.RegisterType<ISessionRepository, SessionRepository>();
            _container.RegisterType<ISetRepository, SetRepository>();
            _container.RegisterType<IWorkOutAssignmentRepository, WorkOutAssignmentRepository>();
            _container.RegisterType<IWorkOutDefinitionRepository, WorkOutDefinitionRepository>();
            _container.RegisterType<IWorkOutRepository, WorkOutRepository>();

            _container.RegisterType<IScheduleViewModel, ScheduleViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<ISessionLogViewModel, SessionLogViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IWorkoutDefinitionLibraryViewModel, WorkOutDefinitionLibraryViewModel>(new ContainerControlledLifetimeManager());

            _container.RegisterType<IAssignWorkoutDefinitionViewModel, AssignWorkoutDefinitionViewModel>();
            _container.RegisterType<ISessionViewModel, SessionViewModel>();
            _container.RegisterType<ISessionDefinitionViewModel, SessionDefinitionViewModel>();
            _container.RegisterType<IWorkoutDefinitionViewModel, WorkoutDefinitionViewModel>();
            _container.RegisterType<IAddSessionDefinitionViewModel, AddSessionDefinitionViewModel>();
            _container.RegisterType<ICreateNextSessionViewModel, CreateNextSessionViewModel>();
            _container.RegisterType<IWorkoutViewModel, WorkoutViewModel>();
            _container.RegisterType<ISetViewModel, SetViewModel>();
            _container.RegisterType<IAddWorkoutDefinitionViewModel, AddWorkoutDefinitionViewModel>();

            _container.RegisterType<IUserInterfaceState, UserInterfaceState>(new ContainerControlledLifetimeManager());
        }
    }
}
