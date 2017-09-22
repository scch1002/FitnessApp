using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WorkOut.App.Forms.Repository.Interfaces;
using WorkOut.App.Forms.ViewModel;
using WorkOut.App.Forms.ViewModel.Interface;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace WorkOut.App.Forms.Tests.ViewModel
{
    [TestClass]
    public class SetViewModelTests
    {
        private IFixture _fixture = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _fixture = new Fixture()
                .Customize(new AutoConfiguredMoqCustomization());
        }

        [TestMethod]
        public void Save_set_to_set_repository()
        {
            var setRepository = _fixture.Freeze<Mock<ISetRepository>>();            

            var sut = _fixture.Create<SetViewModel>();

            sut.SaveSet.Execute(null);

            setRepository.Verify(v => v.UpdateSet(It.Is<ISetViewModel>(i => i == sut)));
        }
    }
}
