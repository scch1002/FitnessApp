using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using WorkOut.App.Forms.ViewModel;
using System.Collections.ObjectModel;
using WorkOut.App.Forms.ViewModel.Interface;
using Moq;
using WorkOut.App.Forms.Repository.Interfaces;

namespace WorkOut.App.Forms.Tests.ViewModel
{
    [TestClass]
    public class SessionViewModelTests
    {
        private IFixture _fixture = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoConfiguredMoqCustomization());
        }

        [TestMethod]
        public void Session_type_property_has_correct_sessions()
        {
            var sut = _fixture.Create<SessionViewModel>();

            foreach (var workout in sut.SessionWorkOuts)
            {
                workout.WorkOutType = Model.WorkOutAssignment.WorkOutTypes.WarmUpWorkout;
            }

            Assert.AreEqual(sut.SessionWorkOuts.Count, sut.WarmupSessionWorkOuts.Count());

            foreach (var workout in sut.WarmupSessionWorkOuts)
            {
                Assert.IsTrue(sut.SessionWorkOuts.Count(c => c == workout) == 1);
            }

            foreach (var workout in sut.SessionWorkOuts)
            {
                workout.WorkOutType = Model.WorkOutAssignment.WorkOutTypes.MainWorkout;
            }

            Assert.AreEqual(sut.SessionWorkOuts.Count, sut.MainSessionWorkOuts.Count());

            foreach (var workout in sut.MainSessionWorkOuts)
            {
                Assert.IsTrue(sut.SessionWorkOuts.Count(c => c == workout) == 1);
            }
        }
    }
}
