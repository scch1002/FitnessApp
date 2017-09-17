using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOut.App.Forms.ViewModel;
using WorkOut.App.Forms.ViewModel.Interface;
using Moq;
using WorkOut.App.Forms.Repository.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using WorkOut.App.Forms.Model;
using System.Collections;
using System.Collections.Generic;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace WorkOut.App.Forms.Tests.ViewModel
{
    [TestClass]
    public class CreateNextSessionViewModelTests
    {
        private IFixture _fixture = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoConfiguredMoqCustomization());
        }

        [TestMethod]
        public void Create_next_session_from_selected_session_definition()
        {
            ISessionViewModel session = null;         
            var sessionLogViewModel = _fixture.Freeze<Mock<ISessionLogViewModel>>();
            var sessionRepository = _fixture.Freeze<Mock<ISessionRepository>>();
       
            sessionRepository.Setup(s => s.AddSession(It.IsAny<ISessionViewModel>()))
                .Callback<ISessionViewModel>(c =>
                {
                    session = c;
                });

            var sut = _fixture.Create<CreateNextSessionViewModel>();

            sut.CreateNextSession.Execute(null);

            Assert.IsNotNull(session);
            Assert.AreEqual(sut.SelectedSessionDefinition.SessionDefinitonId, session.SessionDefinitionId);
            Assert.IsNotNull(session.SessionDate);
            Assert.AreEqual(DateTime.Now.DayOfYear, session.SessionDate.DayOfYear);
            Assert.AreSame(session, sessionLogViewModel.Object.Sessions.First(f => f.SessionId == session.SessionId));
            Assert.AreEqual(sut.SelectedSessionDefinition.SessionName, session.SessionName);
        }

        [TestMethod]
        public void Create_workouts_from_assigned_workout_definitions()
        {
            ISessionViewModel session = null;
            var sessionRepository = _fixture.Freeze<Mock<ISessionRepository>>();
            var workoutRepository = _fixture.Freeze<Mock<IWorkOutRepository>>();

            sessionRepository.Setup(s => s.AddSession(It.IsAny<ISessionViewModel>()))
                .Callback<ISessionViewModel>(c =>
                {
                    session = c;
                });

            workoutRepository.Setup(s => s.AddWorkOut(It.IsAny<IWorkoutViewModel>()))
                .Callback<IWorkoutViewModel>(c =>
                {
                    Assert.IsTrue(session.SessionWorkOuts.Count(e => e == c) == 1);
                });

            var sut = _fixture.Create<CreateNextSessionViewModel>();

            sut.CreateNextSession.Execute(null);

            foreach (var workout in session.SessionWorkOuts)
            {
                var workoutAssignment = sut.SelectedSessionDefinition.WorkOutDefinitions.First(f => f.WorkOutDefinition.WorkOutId == workout.WorkOutDefinitionId);
                var workoutDefinition = workoutAssignment.WorkOutDefinition;
                Assert.AreEqual(workout.WorkOutDefinitionId, workoutDefinition.WorkOutId);
                Assert.AreEqual(workout.WorkOutName, workoutDefinition.WorkOutName);
                Assert.AreEqual(workout.WorkOutType, workoutAssignment.WorkoutType);
            }
        }

        [TestMethod]
        public void Create_sets_from_assigned_workout_definitions()
        {
            ISessionViewModel session = null;
            var sessionRepository = _fixture.Freeze<Mock<ISessionRepository>>();
            var workoutRepository = _fixture.Freeze<Mock<IWorkOutRepository>>();
            var setRepository = _fixture.Freeze<Mock<ISetRepository>>();

            sessionRepository.Setup(s => s.AddSession(It.IsAny<ISessionViewModel>()))
                .Callback<ISessionViewModel>(c =>
                {
                    session = c;
                });

            workoutRepository.Setup(s => s.AddWorkOut(It.IsAny<IWorkoutViewModel>()))
                .Callback<IWorkoutViewModel>(c =>
                {
                    Assert.IsTrue(session.SessionWorkOuts.Count(e => e == c) == 1);
                });

            setRepository.Setup(s => s.AddSet(It.IsAny<ISetViewModel>()))
                .Callback<ISetViewModel>(newValue =>
                {
                    Assert.IsTrue(session.SessionWorkOuts.SelectMany(s => s.WorkOutSets).Count(c => c == newValue) == 1);
                });

            var sut = _fixture.Create<CreateNextSessionViewModel>();

            sut.CreateNextSession.Execute(null);

            foreach (var workout in session.SessionWorkOuts)
            {
                var workoutAssignment = sut.SelectedSessionDefinition.WorkOutDefinitions.First(f => f.WorkOutDefinition.WorkOutId == workout.WorkOutDefinitionId);
                var workoutDefinition = workoutAssignment.WorkOutDefinition;

                Assert.AreEqual(workoutDefinition.NumberOfSets, workout.WorkOutSets.Count(c => c.SetType == WorkOutAssignment.WorkOutTypes.MainWorkout));
                Assert.AreEqual(workoutDefinition.NumberOfWarmUpSets, workout.WorkOutSets.Count(c => c.SetType == WorkOutAssignment.WorkOutTypes.WarmUpWorkout));

                foreach(var warmupSet in workout.WorkOutSets
                    .Where(w => w.SetType == WorkOutAssignment.WorkOutTypes.WarmUpWorkout))
                {                    
                    Assert.AreEqual(workoutDefinition.WarmUpRepetitions, warmupSet.TotalRepetitions);
                    Assert.AreEqual(workoutDefinition.WarmUpWeight, warmupSet.Weight);
                }

                foreach (var set in workout.WorkOutSets
                    .Where(w => w.SetType == WorkOutAssignment.WorkOutTypes.MainWorkout))
                {
                    Assert.AreEqual(workoutDefinition.Repetitions, set.TotalRepetitions);
                    Assert.AreEqual(workoutDefinition.Weight, set.Weight);
                }
            }
        }
    }
}
