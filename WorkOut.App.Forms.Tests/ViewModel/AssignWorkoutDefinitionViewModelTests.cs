using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOut.App.Forms.ViewModel;
using Moq;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;
using WorkOut.App.Forms.Model;
using System.Collections.ObjectModel;
using System.Linq;
using WorkOut.App.Forms.Model.Interface;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace WorkOut.App.Forms.Tests.ViewModel
{
    [TestClass]
    public class AssignWorkoutDefinitionViewModelTests
    {
        private IFixture _fixture = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoConfiguredMoqCustomization());
        }

        [TestMethod]
        public void Assign_workout_definition_to_session_definition()
        {
            var workoutAssignmentRepository = _fixture.Freeze<Mock<IWorkOutAssignmentRepository>>();

            var sut = _fixture.Create<AssignWorkoutDefinitionViewModel>();

            sut.AssignWorkoutDefinition.Execute(null);

            workoutAssignmentRepository.Verify(v => v.AssignWorkOutDefinition(It.Is<IWorkoutAssignment>(i => 
            i.WorkOutDefinition == sut.SelectedWorkoutDefinition 
                && i.SessionDefinitionId == sut.SelectedSessionDefinition.SessionDefinitonId
                && i.WorkoutType == sut.WorkOutType
            )));
        }
    }
}
