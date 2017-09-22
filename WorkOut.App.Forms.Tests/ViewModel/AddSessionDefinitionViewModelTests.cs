using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkOut.App.Forms.ViewModel;
using Moq;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel.Interface;
using System.Collections.ObjectModel;
using System.Linq;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture;

namespace WorkOut.App.Forms.Tests.ViewModel
{
    [TestClass]
    public class AddSessionDefinitionViewModelTests
    {
        private IFixture _fixture = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoConfiguredMoqCustomization());
        }

        [TestMethod]
        public void Add_session_to_session_definition_repository()
        {
            var sessionDefinitionRepository = _fixture.Freeze<Mock<ISessionDefinitionRepository>>();
            var scheduleViewModel = _fixture.Freeze<Mock<IScheduleViewModel>>();

            var sut = _fixture.Create<AddSessionDefinitionViewModel>();

            sut.AddSessionDefinition.Execute(null);

            sessionDefinitionRepository.Verify(s => s.AddSessionDefinition(It.Is<ISessionDefinitionViewModel>(i => i == sut.NewSessionDefinitionViewModel)));
            Assert.IsTrue(scheduleViewModel.Object.Sessions.Count(c => c == sut.NewSessionDefinitionViewModel) == 1);
        }
    }
}
