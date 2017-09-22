using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOut.App.Forms.ViewModel;
using WorkOut.App.Forms.ViewModel.Interface;
using WorkOut.App.Forms.Repository.Interfaces;
using Moq;
using System.Collections.ObjectModel;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture;

namespace WorkOut.App.Forms.Tests.ViewModel
{
    [TestClass]
    public class AddWorkoutDefinitionViewModelTests
    {
        private IFixture _fixture = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoConfiguredMoqCustomization());
        }

        [TestMethod]
        public void Add_workout_to_workout_definition_repository()
        {
            var workoutDefinitionRepository = _fixture.Freeze<Mock<IWorkOutDefinitionRepository>>();
            var workoutDefinitionLibraryViewModel = _fixture.Freeze<Mock<IWorkoutDefinitionLibraryViewModel>>();

            var sut = _fixture.Create<AddWorkoutDefinitionViewModel>();

            sut.AddWorkoutDefinition.Execute(null);

            workoutDefinitionRepository.Verify(s => s.AddWorkOutDefinition(It.Is<IWorkoutDefinitionViewModel>(i => i == sut.NewWorkoutDefinitionViewModel)));
            Assert.IsTrue(workoutDefinitionLibraryViewModel.Object.WorkOutDefinitions.Count(c => c == sut.NewWorkoutDefinitionViewModel) == 1);
        }
    }
}
