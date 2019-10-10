using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Countr.Core.ViewModels;
using Countr.Core.Models;
using System.Threading.Tasks;
using Moq;
using Countr.Core.Services;
using MvvmCross.Core.Navigation;


namespace Countr.Core.Tests.ViewModels
{
    [TestFixture]
    class CountrViewModelTests
    {
        CountrViewModel viewModel; //creates a new counter view model to use in all tests
        Mock<ICountersService> countersService; //Defines and creates a mock counters service that’s passed to the view-model constructor
        Mock<IMvxNavigationService> navigationService; //Sets up the mock navigation service

        [SetUp]
        public void SetUp()
        {
            countersService = new Mock<ICountersService>();
            navigationService = new Mock<IMvxNavigationService>();
            viewModel = new CountrViewModel(countersService.Object,navigationService.Object);
            viewModel.ShouldAlwaysRaiseInpcOnUserInterfaceThread(false); //Ensures all property-changed events are raised on teh current thread, listing 8.27
            
        }
        
        [Test]//Listing 8.18
        public void Name_ComesFromCounter()
        {
            // Arrange
            var counter = new Counter { Name = "A Counter" }; //Creates a new counter with a defined name
            // Act
            viewModel.Prepare(counter); //Prepares the view model with the counter
            // Assert
            Assert.AreEqual(counter.Name, viewModel.Name); //Asserts that the name on the view model matches the counter
        }
        
        [Test]//Testing the increment command page 270, listing 8.27
        public async Task IncrementCounter_IncrementsTheCounter()
        {
            // Act
            await viewModel.IncrementCommand.ExecuteAsync(); //Awaits the call to execute IncrementCommand
            // Assert
            countersService.Verify(s => s.IncrementCounter(It.IsAny<Counter>())); //Asserts that the counter has been incremented by the service
        }

        [Test]//Testing the increment command page 270, listing 8.27
        public async Task IncrementCounter_RaisesPropertyChanged()
        {
            // Arrange
            var propertyChangedRaised = false;
            viewModel.PropertyChanged +=
               (s, e) => propertyChangedRaised = (e.PropertyName == "Count"); //Listens for property changed notifications to the Count property
            // Act
            await viewModel.IncrementCommand.ExecuteAsync(); //Awaaits the call to execute IncrementCommand
            // Assert
            Assert.IsTrue(propertyChangedRaised); //Asserts that the property-changed notification has been raised
        }

        [Test] //Testing the save command
        public async Task SaveCommand_SavesTheCounter()
        {
            // Arrange
            var counter = new Counter { Name = "A Counter" };
            viewModel.Prepare(counter);
            // Act
            await viewModel.SaveCommand.ExecuteAsync(); //Executes the command
            // Assert
            countersService.Verify(c => c.AddNewCounter("A Counter")); //Verifies that the counter was saved
            navigationService.Verify(n => n.Close(viewModel)); //Verifies that the view model was closed
        }

        [Test] //Testing the cancel command
        public void CancelCommand_DoesntSaveTheCounter()
        {
            // Arrange
            var counter = new Counter { Name = "A Counter" };
            viewModel.Prepare(counter);
            // Act
            viewModel.CancelCommand.Execute();
            // Assert
            countersService.Verify(c => c.AddNewCounter(It.IsAny<string>()),//Verifies that AddNewCounter was never called
                                                            Times.Never()); //
            navigationService.Verify(n => n.Close(viewModel));
        }

    }
}
