using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture;
using WorkOut.App.Forms.ViewModel.Interface;
using Moq;
using WorkOut.App.Forms.ViewModel;
using WorkOut.App.Forms.Repository.Interfaces;
using System.Linq;

namespace WorkOut.App.Forms.Tests.ViewModel
{
    [TestClass]
    public class WorkOutDefinitionLibraryViewModelTests
    {
        private IFixture _fixture = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoConfiguredMoqCustomization());
        }

        [TestMethod]
        public void Remove_selected_workout_definition()
        {
            var workoutDefinitionRepository = _fixture.Freeze<Mock<IWorkOutDefinitionRepository>>();

            var sut = _fixture.Create<WorkOutDefinitionLibraryViewModel>();

            sut.SelectedWorkoutDefinition = sut.WorkOutDefinitions.First();

            var workoutDefinition = sut.SelectedWorkoutDefinition;

            sut.RemoveWorkoutDefinition.Execute(null);

            workoutDefinitionRepository.Verify(v => v.DeleteWorkOutDefinition(It.Is<IWorkoutDefinitionViewModel>(i => i == workoutDefinition)));
            Assert.IsTrue(sut.WorkOutDefinitions.All(a => a != workoutDefinition));
            Assert.IsNull(sut.SelectedWorkoutDefinition);
        }
    }
}
