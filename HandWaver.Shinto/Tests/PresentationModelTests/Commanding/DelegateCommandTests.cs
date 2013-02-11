using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shinto.PresentationModel.Commanding;

namespace PresentationModelTests.Commanding
{
    [TestClass]
    public class DelegateCommandTests
    {
        [TestMethod]
        public void DelegateCommandTestToggleCanExecute()
        {
            bool canExecute = true;
            bool executeChangedCalled = false;

            var command = new DelegateCommand(() =>
            {

            }, () =>
            {
                return canExecute;
            });

            command.CanExecuteChanged += (s, e) =>
            {
                executeChangedCalled = true;
            };

            command.CheckCanExecute();

            Assert.IsTrue(executeChangedCalled);

        }
    }
}
