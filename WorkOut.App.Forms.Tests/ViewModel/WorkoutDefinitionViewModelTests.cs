using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Moq;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.Tests.ViewModel
{
    [TestClass]
    public class WorkoutDefinitionViewModelTests
    {
        private IFixture _fixture = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoConfiguredMoqCustomization());
        }

        [TestMethod]
        public void Update_workout_definition()
        {
            var workoutAssignmentRepository = _fixture.Freeze<Mock<IWorkOutDefinitionRepository>>();

            var sut = _fixture.Create<WorkoutDefinitionViewModel>();

            sut.UpdateWorkoutDefinition.Execute(null);

            workoutAssignmentRepository.Verify(v => v.UpdateWorkOutDefinition(It.Is<IWorkoutDefinitionViewModel>(i => i == sut)));
        }
    }
}
