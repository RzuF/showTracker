using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using showTracker.ViewModel.MainPage;

namespace showTracker.BusinessLayer.Tests.ViewModels
{
    [TestFixture]
    class MainViewModelTests
    {
        [Test]
        public void ShowsIsOk()
        {
            //Arrange
            var viewModel = new MainViewModel();

            //Act
            Assert.AreEqual(viewModel.Shows[0], viewModel.Shows[0].Self);
        }
    }
}
