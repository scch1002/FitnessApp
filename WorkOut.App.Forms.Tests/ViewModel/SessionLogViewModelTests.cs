using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel;
using System.Linq;
using Moq;
using WorkOut.App.Forms.ViewModel.Interface;

namespace WorkOut.App.Forms.Tests.ViewModel
{
    [TestClass]
    public class SessionLogViewModelTests
    {
        private IFixture _fixture = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoConfiguredMoqCustomization());
        }

        [TestMethod]
        public void Remove_session_from_log()
        {
            var sessionRepository = _fixture.Freeze<Mock<ISessionRepository>>();

            var sut = _fixture.Create<SessionLogViewModel>();

            sut.SelectedSession = sut.Sessions.First();

            var selectedSession = sut.SelectedSession;

            sut.RemoveSelectedSession.Execute(null);

            sessionRepository.Verify(v => v.DeleteSession(It.Is<ISessionViewModel>(i => i == selectedSession)));
            Assert.IsTrue(sut.Sessions.All(a => a != selectedSession));
            Assert.IsNull(sut.SelectedSession);
        }
    }
}
