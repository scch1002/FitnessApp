using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel;
using Moq;
using System.Collections.ObjectModel;
using WorkOut.App.Forms.ViewModel.Interface;
using WorkOut.App.Forms.Model.Interface;

namespace WorkOut.App.Forms.Tests.ViewModel
{
    [TestClass]
    public class SessionDefinitionViewModelTests
    {
        private IFixture _fixture = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoConfiguredMoqCustomization());
        }

        [TestMethod]
        public void Workout_type_property_has_correct_workout_assignments()
        {
            var sut = _fixture.Create<SessionDefinitionViewModel>();

            foreach(var workoutAssignment in sut.WorkOutDefinitions)
            {
                workoutAssignment.WorkoutType = Model.WorkOutAssignment.WorkOutTypes.WarmUpWorkout;
            }

            Assert.AreEqual(sut.WorkOutDefinitions.Count, sut.WarmUpWorkOutDefinitions.Count());

            foreach(var assignment in sut.WarmUpWorkOutDefinitions)
            {
                Assert.IsTrue(sut.WorkOutDefinitions.Count(c => c == assignment) == 1);
            }

            foreach (var workoutAssignment in sut.WorkOutDefinitions)
            {
                workoutAssignment.WorkoutType = Model.WorkOutAssignment.WorkOutTypes.MainWorkout;
            }

            Assert.AreEqual(sut.WorkOutDefinitions.Count, sut.MainWorkOutDefinitions.Count());

            foreach (var assignment in sut.MainWorkOutDefinitions)
            {
                Assert.IsTrue(sut.WorkOutDefinitions.Count(c => c == assignment) == 1);
            }
        }

        [TestMethod]
        public void Remove_selected_workout_definition()
        {
            _fixture.Freeze<Mock<IWorkOutDefinitionRepository>>();
            var workOutAssignmentRepository = _fixture.Freeze<Mock<IWorkOutAssignmentRepository>>();

            var sut = _fixture.Create<SessionDefinitionViewModel>();

            sut.SelectedWorkOutDefinition = sut.WorkOutDefinitions.First();

            var selectedWorkoutDefinition = sut.SelectedWorkOutDefinition;

            sut.RemoveSelectedWorkOutDefinition.Execute(null);

            workOutAssignmentRepository.Verify(v => v.UnassignWorkOutDefinition(It.Is<IWorkoutAssignment>(i => i == selectedWorkoutDefinition)));
            Assert.IsTrue(sut.WorkOutDefinitions.All(a => a != selectedWorkoutDefinition));
            Assert.IsNull(sut.SelectedWorkOutDefinition);
        }
    }
}
