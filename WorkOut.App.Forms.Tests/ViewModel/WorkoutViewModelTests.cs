using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using WorkOut.App.Forms.Repository.Interfaces;
using Moq;
using WorkOut.App.Forms.ViewModel;
using WorkOut.App.Forms.ViewModel.Interface;
using System.Collections.ObjectModel;

namespace WorkOut.App.Forms.Tests.ViewModel
{
    [TestClass]
    public class WorkoutViewModelTests
    {
        private IFixture _fixture = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoConfiguredMoqCustomization());
        }

        [TestMethod]
        public void Update_workout_view_model_set_WorkOutComplete_to_true()
        {
            var workoutRepository = _fixture.Freeze<Mock<IWorkOutRepository>>();

            var sut = _fixture.Create<WorkoutViewModel>();

            sut.SaveWorkout.Execute(null);

            Assert.IsTrue(sut.WorkOutComplete);
        }

        [TestMethod]
        public void Update_workout_view_model_call_repository_update()
        {
            var workoutRepository = _fixture.Freeze<Mock<IWorkOutRepository>>();

            var sut = _fixture.Create<WorkoutViewModel>();

            sut.SaveWorkout.Execute(null);

            workoutRepository.Verify(v => v.UpdateWorkOut(It.Is<IWorkoutViewModel>(i => i == sut)));
        }

        [TestMethod]
        public void AllSetsCompleted_property_correct()
        {
            var sut = _fixture.Create<WorkoutViewModel>();

            foreach (var set in sut.WorkOutSets)
            {
                set.CompletedRepetitions = set.TotalRepetitions + 1;
            }

            Assert.IsFalse(sut.AllSetsCompleted);

            foreach (var set in sut.WorkOutSets)
            {
                set.CompletedRepetitions = set.TotalRepetitions;
            }

            Assert.IsTrue(sut.AllSetsCompleted);            
        }

        [TestMethod]
        public void Set_type_property_has_correct_sets()
        {
            var sut = _fixture.Create<WorkoutViewModel>();

            foreach (var set in sut.WorkOutSets)
            {
                set.SetType = Model.WorkOutAssignment.WorkOutTypes.WarmUpWorkout;
            }

            Assert.AreEqual(sut.WorkOutSets.Count, sut.WarmupWorkOut.Count());

            foreach (var workout in sut.WarmupWorkOut)
            {
                Assert.IsTrue(sut.WorkOutSets.Count(c => c == workout) == 1);
            }

            foreach (var workout in sut.WorkOutSets)
            {
                workout.SetType = Model.WorkOutAssignment.WorkOutTypes.MainWorkout;
            }

            Assert.AreEqual(sut.WorkOutSets.Count, sut.MainWorkOut.Count());

            foreach (var workout in sut.MainWorkOut)
            {
                Assert.IsTrue(sut.WorkOutSets.Count(c => c == workout) == 1);
            }
        }
    }
}
