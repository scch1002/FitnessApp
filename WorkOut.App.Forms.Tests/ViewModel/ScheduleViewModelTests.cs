using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel;
using WorkOut.App.Forms.ViewModel.Interface;
using System.Linq;
using Moq;

namespace WorkOut.App.Forms.Tests.ViewModel
{
    [TestClass]
    public class ScheduleViewModelTests
    {
        private IFixture _fixture = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoConfiguredMoqCustomization());
        }

        [TestMethod]
        public void Remove_selected_session_definition()
        {
            var sessionDefinitionRepository = _fixture.Freeze<Mock<ISessionDefinitionRepository>>();

            var sut = _fixture.Create<ScheduleViewModel>();

            var selectedSessionDefinition = sut.Sessions.First();

            sut.SelectedSessionDefinition = sut.Sessions.First();

            sut.RemoveSelectedSessionDefinition.Execute(null);

            sessionDefinitionRepository.Verify(v => v.DeleteSessionDefinition(It.Is<ISessionDefinitionViewModel>(i => i == selectedSessionDefinition)));
            Assert.IsTrue(sut.Sessions.All(a => a != selectedSessionDefinition));
            Assert.IsNull(sut.SelectedSessionDefinition);
        }
    }
}
