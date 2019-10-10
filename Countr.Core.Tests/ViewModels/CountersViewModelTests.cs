using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Countr.Core.Services;
using Countr.Core.ViewModels;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Collections.Generic;
using Countr.Core.Models;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.Navigation;

namespace Countr.Core.Tests.ViewModels
{
    [TestFixture]
    public class CountersViewModelTests
    {
        Mock<ICountersService> countersService;
        Mock<IMvxMessenger> messenger;
        CountersViewModel viewModel;
        Action<CountersChangedMessage> publishAction; //An action to store the subscription
        Mock<IMvxNavigationService> navigationService; //Creates a mock of the navigation service

        [SetUp]
        public void SetUp()
        {
            messenger = new Mock<IMvxMessenger>();
            countersService = new Mock<ICountersService>();  //creates a mock counters service
            navigationService = new Mock<IMvxNavigationService>(); //Creates a mock of the navigation service
            viewModel = new CountersViewModel(countersService.Object,  //uses the mock counters service to create the view model
                                               messenger.Object, 
                                               navigationService.Object); 

            //listing 8.34 page 277  Sets up the subscribe method on the messenger so the action is stored
            messenger = new Mock<IMvxMessenger>();
            messenger.Setup(m => m.SubscribeOnMainThread
                             (It.IsAny<Action<CountersChangedMessage>>(),

                              It.IsAny<MvxReference>(),

                              It.IsAny<string>()))
                      .Callback<Action<CountersChangedMessage>,
                       MvxReference,
                       string>((a, m, s) => publishAction = a);

            viewModel = new CountersViewModel(countersService.Object,
                                              messenger.Object,
                                              navigationService.Object);

        }

        [Test]
        public async Task LoadCounters_CreatesCoutners()
        {
            //Arrange
            var counters = new List<Counter>
            {
                //Create a list of counter instances with dummy data
                new Counter{Name = "Counter1", Count = 0},
                new Counter{Name = "Counter2", Count = 4},
            };
            countersService.Setup(c => c.GetAllCounters())
                                        .ReturnsAsync(counters); //Sets up the mock counters service to return the list
            //Act
            await viewModel.LoadCounters(); //Calls LoadCounters on the view model to create the counter view models

            // Assert
            //Asserts that the list of CountrViewModel instances contains viewmodes that match teh canned data
            Assert.AreEqual(2, viewModel.Counters.Count);
            Assert.AreEqual("Counter1", viewModel.Counters[0].Name);
            Assert.AreEqual(0, viewModel.Counters[0].Count);
            Assert.AreEqual("Counter2", viewModel.Counters[1].Name);
            Assert.AreEqual(4, viewModel.Counters[1].Count);
        }
        [Test]
        public void ReceivedMessage_LoadsCounters()
        {
            // Arrange
            countersService.Setup(s => s.GetAllCounters())
                               .ReturnsAsync(new List<Counter>()); //Sets up a mock return value from GetAllCounters
            // Act
            publishAction.Invoke(new CountersChangedMessage(this)); // Calls the subscription action to simulate a message being published
            // Assert
            countersService.Verify(s => s.GetAllCounters()); //Verifies that after the message is published, the counters are reloaded
        }

        [Test] //Unit testing the showw-add-new-counter command
        public async Task ShowAddNewCounterCommand_ShowsCounterViewModel()
        {
            // Act                                                                
            await viewModel.ShowAddNewCounterCommand.ExecuteAsync(); //Executes the command
                                                                     // Assert Asserts that the correct view model was navigated to                                                              
            navigationService.Verify(n => n.Navigate(typeof(CountrViewModel),
                                                     It.IsAny<Counter>(),
                                                     null));
        }

    }
}